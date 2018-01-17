//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System.IO.Ports;
using System;
using System.IO;
using System.Threading;
using DialogEngine.Helpers;
using log4net;
using System.Reflection;
using System.Windows;

namespace DialogEngine
{

    public static class SerialComs
    {
        #region - Fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static SerialPort msSerialPort;

        public const int NumRadios = 6;  //includes dongle

        public static object CompPortErrorDialog { get; private set; }

        #endregion


        #region - Private functions -


        private static string readSerialInLine()
        {
            string _message = null;

            try
            {
                if (msSerialPort.BytesToRead > 18)
                {
                    _message = msSerialPort.ReadLine();

                    if (msSerialPort.BytesToRead > 1000)
                    {
                        // got behind for some reason
                        msSerialPort.DiscardInBuffer();

                        Console.WriteLine("serial buffer over run.");
                    }
                    if (SessionVariables.WriteSerialLog)
                    {
                        using (StreamWriter _serialLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.HexLogFileName, true))
                        {
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

        #endregion


        #region - Public methods -



        /// <summary>
        /// Initialize serial port
        /// </summary>
        public static void InitSerial()
        {
            if (SessionVariables.UseSerialPort)

            {
                try
                {
                    msSerialPort = new SerialPort();

                    Thread _readThread = new Thread(RegularylyReadSerial);

                    msSerialPort.PortName = SessionVariables.ComPortName;

                    msSerialPort.BaudRate = 460800;

                    msSerialPort.ReadTimeout = 500;

                    msSerialPort.Open();

                    msSerialPort.DiscardInBuffer();

                    _readThread.Start();
                }
                catch(Exception ex)
                {
                    mcLogger.Error("Serial port error " + ex.Message);

                    MessageBox.Show("COM port  doesn't exist. Please check configuration. ");
                }
            }

            else
            {
                Thread _dontReadThread = new Thread(SelectNextCharacters.OccasionallyChangeToRandNewCharacter);
                _dontReadThread.Start();
            }
            //worry about stopping cleanly later TODO
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

                if (_rowNum > -1 && _rowNum < NumRadios && _processCurrentMessage)
                {
                    _cycleCount++;
                    ParseMessage.ProcessMessage(_rowNum, _newRow);
                    SelectNextCharacters.FindBiggestRssiPair();
                }

                if (_cycleCount > 80)
                {
                    HeatMap.PrintHeatMap();
                    _cycleCount = 0;
                }
            }
        }

        #endregion


    }
}

