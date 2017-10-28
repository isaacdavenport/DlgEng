//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System.IO.Ports;
using System;
using System.IO;
using System.Threading;
using DialogEngine.Helpers;

namespace DialogEngine
{

    public static class SerialComs
    {
        public const int NumRadios = 6;  //includes dongle
        static SerialPort mcSerialPort;

        public static void InitSerial()

        {
            if (!SessionVars.NoSerialPort)

            {
                mcSerialPort = new SerialPort();

                Thread _readThread = new Thread(RegularylyReadSerial);

                mcSerialPort.PortName = SessionVars.ComPortName;

                mcSerialPort.BaudRate = 460800;

                mcSerialPort.ReadTimeout = 500;

                mcSerialPort.Open();

                mcSerialPort.DiscardInBuffer();

                _readThread.Start();
            }

            else
            {
                Thread _dontReadThread = new Thread(SelectNextCharacters.OccasionallyChangeToRandNewCharacter);
                _dontReadThread.Start();
            }
            //worry about stopping cleanly later TODO
        }

        private static string readSerialInLine()
        {
            string _message = null;

            try
            {
                if (mcSerialPort.BytesToRead > 18)
                {
                    _message = mcSerialPort.ReadLine();

                    if (mcSerialPort.BytesToRead > 1000)
                    {
                        // got behind for some reason
                        mcSerialPort.DiscardInBuffer();
                        Console.WriteLine("serial buffer over run.");
                    }
                    if (SessionVars.WriteSerialLog)
                    {
                        using (StreamWriter _serialLog = new StreamWriter(
                            SessionVars.LogsDirectory + SessionVars.HexLogFileName, true)) {
                            _serialLog.Write(DateTime.Now.ToString("mm.ss.fff") + "  ");
                            _serialLog.Write(_message);
                            _serialLog.Close();
                        }
                    }
                }
            }
            catch (TimeoutException) {
                Console.WriteLine("serial timeout.");
            }
            return _message;
        }

        public static void RegularylyReadSerial()
        {
            int[] _newRow = new int[NumRadios + 1];
            int _cycleCount = 0;

            while (true)
            {
                var _processCurrentMessage = true;
                int _rowNum = -1;
                var _message = readSerialInLine();
                if (_message != null)
                {
                    _rowNum = ParseMessage.Parse(_message, _newRow);
                }
                else
                {
                    _processCurrentMessage = false;  //we are in here a great deal
                }
                if (_rowNum > -1 && _rowNum < NumRadios && _processCurrentMessage) {
                    _cycleCount++;
                    ParseMessage.ProcessMessage(_rowNum, _newRow);
                    SelectNextCharacters.FindBiggestRssiPair();
                }
                if (_cycleCount > 110)
                {
                    FirmwareDebuggingTools.ProcessDebugFlags();
                    _cycleCount = 0;
                }
            }
        }
    }
}

