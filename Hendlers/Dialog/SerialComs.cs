//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System.IO.Ports;
using System;
using System.Threading;
using DialogEngine.Helpers;
using log4net;
using System.Reflection;
using System.Threading.Tasks;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using System.Windows;
using System.Linq;
using DialogEngine.ViewModels;
using DialogEngine.Models.Logger;
using DialogEngine.Dialogs;
using MaterialDesignThemes.Wpf;
using System.Windows.Threading;
using System.Configuration;
using DialogEngine.Services;

namespace DialogEngine
{

    public static class SerialComs
    {
        #region - Fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly DispatcherTimer mcHeatMapUpdateTimer = new DispatcherTimer();
        private static SerialPort msSerialPort;
        private static CancellationTokenSource msSerialTokenSource = new CancellationTokenSource();
        private static CancellationTokenSource msRandomTokenSource = new CancellationTokenSource();
        private static bool mIsSerialMode;
        public const int NumRadios = 6;  //includes dongle

        #endregion

        #region - Static constructor -

        static SerialComs()
        {
            EventAggregator.Instance.GetEvent<UseSerialPortChanged>().Subscribe(_useSerialPortChanged);
            mcHeatMapUpdateTimer.Interval = TimeSpan.FromSeconds(3);
            mcHeatMapUpdateTimer.Tick += _heatMapUpdateTimer_Tick;
        }

        #endregion

        #region - Event handlers -

        private static void _heatMapUpdateTimer_Tick(object sender, EventArgs e)
        {
            HeatMapUpdate.PrintHeatMap();
        }

        #endregion

        #region - Private functions -

        // method is executed when filed "UseSerialPort" is changed in configuration
        private async static void _useSerialPortChanged()
        {
            if (SessionVariables.UseSerialPort)
            {
                msRandomTokenSource.Cancel();
            }
            else
            {
                if(msSerialPort != null && msSerialPort.IsOpen)
                msSerialPort.Close();

                msSerialTokenSource.Cancel();
                mcHeatMapUpdateTimer.Stop();
            }

            await InitCharacterSelection();
        }

        private static  string _readSerialInLine()
        {
            string _message = null;

            try
            {
                if (msSerialPort.IsOpen && msSerialPort.BytesToRead > 18)
                {
                    _message = msSerialPort.ReadLine();

                    if (msSerialPort.BytesToRead > 1000)
                    {
                        // got behind for some reason
                        msSerialPort.DiscardInBuffer();

                        mcLogger.Debug("serial buffer over run.");
                    }
                }
            }
            catch (TimeoutException) {
                mcLogger.Debug("serial buffer over run.");
            }
            catch (InvalidOperationException ex)
            {
                mcLogger.Debug("readSerialInLine invalid operation " + ex.Message);
            }
            catch(Exception ex)   // this is happened when usb is plug
            {
                DialogDataService.AddMessage(new ErrorMessage("Device is disconnected."));

                // COM port is closed, so we will run random selection
                string _configPath = System.IO.Path.Combine(Environment.CurrentDirectory, "DialogEngine.exe");
                Configuration _config = ConfigurationManager.OpenExeConfiguration(_configPath);
                _config.AppSettings.Settings["UseSerialPort"].Value = false.ToString();
                _config.Save();
                ConfigurationManager.RefreshSection("appSettings");

                _useSerialPortChanged();
            }
            return _message;
        }


        private async static Task _initSerial()
        {
            try
            {
                SelectNextCharacters.NextCharacter1 = 0;
                SelectNextCharacters.NextCharacter2 = 0;

                msSerialPort = new SerialPort();
                msSerialPort.PortName = SessionVariables.ComPortName;
                msSerialPort.BaudRate = 460800;
                msSerialPort.ReadTimeout = 500;
                if (msSerialPort.IsOpen)
                {
                    msSerialPort.Close();
                }
                msSerialPort.Open();
                msSerialPort.DiscardInBuffer();

                mcHeatMapUpdateTimer.Start();

                await _regularylyReadSerialAsync(msSerialTokenSource.Token);
            }
            catch(Exception ex)
            {
                throw new Exception("InitSerial error "+ex.Message);
            }
        }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Initialize characters selection method
        /// </summary>
        public async static Task InitCharacterSelection() {
            msSerialTokenSource = new CancellationTokenSource();
            msRandomTokenSource = new CancellationTokenSource();

            try
            {
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
                        var result = await DialogHost.Show(new SerialComPortErrorDialog());

                        // if user clicked on "Save changes" we try to again initialize serial
                        if (result == null)
                        {
                            try
                            {
                                await _initSerial();
                            }
                            catch (Exception innerEx)
                            {
                                mcLogger.Error("Init serial COM port error after changing of COM port. " + innerEx.Message);

                                // if again error occured we initialize random selection, so we need to see log file check error
                                await SelectNextCharacters.OccasionallyChangeToRandNewCharacterAsync(msRandomTokenSource.Token);
                            }
                        }
                        else // if user closed dialog we initialize random selection
                        {
                            await SelectNextCharacters.OccasionallyChangeToRandNewCharacterAsync(msRandomTokenSource.Token);
                        }
                    }
                }
                else // user chose NoSerialPort so we initialize random selection
                {
                    await SelectNextCharacters.OccasionallyChangeToRandNewCharacterAsync(msRandomTokenSource.Token);
                }
            }
            catch (Exception e)
            {
                mcLogger.Error("InitCharacterSelection " + e.Message);
                DialogDataService.AddMessage(new ErrorMessage("Error in character selection method."));
            }
            //worry about stopping cleanly later TODO
        }


        private async static Task _regularylyReadSerialAsync(CancellationToken _cancellationToken)
        {
            await Task.Run(() =>
            {
                Thread.CurrentThread.Name = "SerialThread";

                int[] _newRow = new int[NumRadios + 1];
                int _cycleCount = 0;

                IsSerialMode = true;
                while (true)
                {
                    try
                    {
                        if (_cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }

                        var _processCurrentMessage = true;
                        int _rowNum = -1;

                        var _message = _readSerialInLine();

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
                    }
                    catch(Exception ex)
                    {
                        mcLogger.Error("_regularylyReadSerialAsync " + ex.Message);
                        DialogDataService.AddMessage(new ErrorMessage("Error in serial communication."));
                    }
                }
            });
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Indicates which selection mode is active
        /// true - serial mode
        /// false - random mode
        /// </summary>
        public static bool IsSerialMode
        {
            get { return mIsSerialMode; }
            set
            {
                mIsSerialMode = value;
                try
                {
                    string _selectionModeName = mIsSerialMode ? "Serial" : "Random";

                    if (Application.Current.Dispatcher.CheckAccess())
                    {
                        Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().SelectionModeLabel.Content = _selectionModeName;
                    }
                    else
                    {
                        Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().SelectionModeLabel.Content = _selectionModeName;
                        }));
                    }
                }
                catch (Exception ex)
                {
                    mcLogger.Error("IsSerialMode changed. " + ex.Message);
                }
            }
        }


        #endregion
    }
}

