﻿//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System.IO.Ports;
using System;
using System.IO;
using System.Threading;

namespace DialogEngine
{
    public class ReceivedMessage
    {
        public string CharacterPrefix = "XX";
        public DateTime ReceivedTime = DateTime.MinValue;
        public int[] Rssi = new int[SerialComs.NUM_RADIOS];
        public int SequenceNum = -1;
    }

    public static class SerialComs
    {
        public const int NUM_RADIOS = 6;  //includes dongle
        static SerialPort _serialPort;

        public static void InitSerial() {
            if (!SessionVars.NoSerialPort) {
                _serialPort = new SerialPort();
                Thread readThread = new Thread(RegularylyReadSerial);
                _serialPort.PortName = SessionVars.ComPortName;
                _serialPort.BaudRate = 460800;
                _serialPort.ReadTimeout = 500;
                _serialPort.Open();
                _serialPort.DiscardInBuffer();
                readThread.Start();
            }
            else {
                Thread dontReadThread = new Thread(SelectNextCharacters.OccasionallyChangeToRandNewCharacter);
                dontReadThread.Start();
            }
            //worry about stopping cleanly later TODO
        }

        private static string ReadSerialInLine() {
            string message = null;
            try {
                if (_serialPort.BytesToRead > 18) {
                    message = _serialPort.ReadLine();
                    if (_serialPort.BytesToRead > 1000) {
                        // got behind for some reason
                        _serialPort.DiscardInBuffer();
                        Console.WriteLine("serial buffer over run.");
                    }
                    if (SessionVars.WriteSerialLog) {
                        using (StreamWriter serialLog = new StreamWriter(
                            SessionVars.LogsDirectory + SessionVars.SerialLogFileName, true)) {
                            serialLog.Write(DateTime.Now.ToString("mm.ss.fff") + "  ");
                            serialLog.Write(message);
                            serialLog.Close();
                        }
                    }
                }
            }
            catch (TimeoutException) {
                Console.WriteLine("serial timeout.");
            }
            return message;
        }

        public static void RegularylyReadSerial()
        {
            int[] newRow = new int[NUM_RADIOS + 1];
            int cycleCount = 0;

            while (true)
            {
                var processCurrentMessage = true;
                int rowNum = -1;
                var message = ReadSerialInLine();
                if (message != null)
                {
                    rowNum = ParseMessage.Parse(message, newRow);
                }
                else
                {
                    processCurrentMessage = false;  //we are in here a great deal
                }
                if (rowNum > -1 && rowNum < NUM_RADIOS && processCurrentMessage) {
                    cycleCount++;
                    ParseMessage.ProcessMessage(rowNum, newRow);
                    SelectNextCharacters.FindBiggestRssiPair();
                }
                if (cycleCount > 70)
                {
                    FirmwareDebuggingTools.ProcessDebugFlags();
                    cycleCount = 0;
                }
            }
        }
    }
}

