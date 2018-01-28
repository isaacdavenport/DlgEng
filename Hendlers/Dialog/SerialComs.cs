//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System.IO.Ports;
using System;
using System.IO;
using System.Threading;
using DialogEngine.Helpers;
using log4net;
using System.Reflection;
using DialogEngine.Dialogs;
using System.Threading.Tasks;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;

namespace DialogEngine
{

    public static class SerialComs
    {
        #region - Fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static SerialPort msSerialPort;

        private static CancellationTokenSource msSerialTokenSource = new CancellationTokenSource();
        private static CancellationTokenSource msRandomTokenSource = new CancellationTokenSource();

        public const int NumRadios = 6;  //includes dongle

        #endregion

        static SerialComs()
        {
            EventAggregator.Instance.GetEvent<UseSerialPortChanged>().Subscribe(_useSerialPortChanged);
        }

        

        #region - Private functions -

        // 
        private async  static void _useSerialPortChanged()
        {
            if (SessionVariables.UseSerialPort)
            {
                msRandomTokenSource.Cancel();
            }
            else
            {
                msSerialTokenSource.Cancel();
            }

             InitCharacterSelection();
        }


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


        private async static Task _initSerial()
        {
            try
            {
                msSerialPort = new SerialPort();

                msSerialPort.PortName = SessionVariables.ComPortName;

                msSerialPort.BaudRate = 460800;

                msSerialPort.ReadTimeout = 500;

                msSerialPort.Open();

                msSerialPort.DiscardInBuffer();

                await _regularylyReadSerialAsync(msSerialTokenSource.Token);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion




        #region - Public methods -

        

        /// <summary>
        /// Initialize characters selection method
        /// </summary>
        public async  static Task  InitCharacterSelection()
        {
            msSerialTokenSource = new CancellationTokenSource();
            msRandomTokenSource = new CancellationTokenSource();

            if (SessionVariables.UseSerialPort)
            {


                try
                {
                     await _initSerial();
                }
                catch (Exception ex)
                {
                    mcLogger.Error("Serial port error " + ex.Message);

                    // if COM port name is not valid, we show dialog to user with valid COM ports 
                    SerialComPortErrorDialog dialog = new SerialComPortErrorDialog();

                    dialog.ShowDialog();

                    // if user clicked on "Save changes" we try to again initialize serial
                    if(dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    {
                        try
                        {
                           await _initSerial();
                        }
                        catch(Exception innerEx)
                        {
                          mcLogger.Error("Init serial COM port error after changing of COM port. " + innerEx.Message);
                          
                          // if again error occured we initialize random selection, so we need to see log file check error
                           await SelectNextCharacters.OccasionallyChangeToRandNewCharacterAsync(msRandomTokenSource.Token);
                        }
                    }
                    else  // if user closed dialog we initialize random selection
                    {
                         await SelectNextCharacters.OccasionallyChangeToRandNewCharacterAsync(msRandomTokenSource.Token);
                    }

                }
            }
            else // user choose NoSerialPort so we initialize random selection
            {
                 await SelectNextCharacters.OccasionallyChangeToRandNewCharacterAsync(msRandomTokenSource.Token);
            }

            //worry about stopping cleanly later TODO
        }


        private async static Task _regularylyReadSerialAsync(CancellationToken _cancllationToken)
        {
            await Task.Run(() =>
            {

                int[] _newRow = new int[NumRadios + 1];
                int _cycleCount = 0;

                while (true)
                {
                    if (_cancllationToken.IsCancellationRequested)
                    {
                        return;
                    }

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
                        HeatMapUpdate.PrintHeatMap();
                        _cycleCount = 0;
                    }
                }

            });
        }

        #endregion


    }
}

