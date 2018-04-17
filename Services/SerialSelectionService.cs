
using DialogEngine.Dialogs;
using DialogEngine.Helpers;
using DialogEngine.Models.Shared;
using DialogEngine.Workflows.SerialSelectionWorkflow;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DialogEngine.Services
{
    public class SerialSelectionService : ICharacterSelection
    {
        #region - fields -

        private readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly int NumRadios = 6;  //includes dongle
        public const int StrongRssiBufDepth = 12;
        public readonly TimeSpan MaxLastSeenInterval = new TimeSpan(0, 0, 0, 3, 100);
        private readonly DispatcherTimer mcHeatMapUpdateTimer = new DispatcherTimer();
        private int mTempCh1;
        private int mTempch2;
        private SerialPort mSerialPort;
        private StateMachine mStateMachine;
        private States mCurrentState;
        private int mRowNum;
        private int [] mNewRow= new int[NumRadios +1];
        private bool mIsSerialMode;
        private Random mRandom = new Random();
        private int[,] mStrongRssiCharacterPairBuf = new int[2, StrongRssiBufDepth];

        public int BigRssi = 0;        
        public int CurrentCharacter1;
        public int CurrentCharacter2 = 1;
        public static int NextCharacter1 = 1;
        public static int NextCharacter2 = 2;
        public static int[,] HeatMap = new int[NumRadios,NumRadios];
        public static DateTime[] CharactersLastHeatMapUpdateTime = new DateTime[NumRadios];

        #endregion

        #region - constructor -

        public SerialSelectionService()
        {
            StateMachine = new StateMachine
            (
                action: () => { } // no action for start state
            );

            _configureStateMachine();
            StateMachine.PropertyChanged += _stateMachine_PropertyChanged;
            mcHeatMapUpdateTimer.Interval = TimeSpan.FromSeconds(3);
            mcHeatMapUpdateTimer.Tick += _heatMapUpdateTimer_Tick;
        }

        #endregion

        #region - event handlers -

        private void _stateMachine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("State"))
                mCurrentState = StateMachine.State;
        }


        private void _heatMapUpdateTimer_Tick(object sender, EventArgs e)
        {
            DialogData.Instance.HeatMapUpdate = HeatMap;
        }

        #endregion

        #region - private functions -

        private void _configureStateMachine()
        {
            StateMachine.Configure(States.Start)
                .Permit(Triggers.Initialize, States.Init);

            StateMachine.Configure(States.Init)
                .Permit(Triggers.ReadMessage, States.ReadMessage)
                .Permit(Triggers.SerialPortNameError, States.SerialPortNameError)
                .Permit(Triggers.USB_disconnectedError,States.USB_disconnectedError)
                .Permit(Triggers.Finish, States.Finish);

            StateMachine.Configure(States.Idle)
                .Permit(Triggers.ReadMessage, States.ReadMessage)
                .Permit(Triggers.Finish, States.Finish);

            StateMachine.Configure(States.SerialPortNameError)
                .Permit(Triggers.Initialize, States.Init)
                .Permit(Triggers.Start, States.Start);

            StateMachine.Configure(States.USB_disconnectedError)
                .OnEntry(t => _usbDisconectedError())
                .Permit(Triggers.Initialize, States.Init)
                .Permit(Triggers.Finish, States.Finish);

            StateMachine.Configure(States.ReadMessage)
                .Permit(Triggers.FindClosestPair, States.FindClosestPair)
                .Permit(Triggers.Initialize, States.Init)
                .Permit(Triggers.Idle, States.Idle)
                .Permit(Triggers.Finish, States.Finish);
                
            StateMachine.Configure(States.FindClosestPair)
                .Permit(Triggers.ReadMessage, States.ReadMessage)
                .Permit(Triggers.SelectNextCharacters, States.SelectNextCharacters)
                .Permit(Triggers.Finish, States.Finish);

            StateMachine.Configure(States.SelectNextCharacters)
                .Permit(Triggers.ReadMessage, States.ReadMessage)
                .Permit(Triggers.Finish, States.Finish);

            StateMachine.Configure(States.Finish)
                .Permit(Triggers.Start,States.Start);
                
        }


        private Triggers _initSerial()
        {
            try
            {
                NextCharacter1 = 0;
                NextCharacter2 = 0;

                mSerialPort = new SerialPort();
                mSerialPort.PortName = SessionHelper.ComPortName;
                mSerialPort.BaudRate = 460800;
                mSerialPort.ReadTimeout = 500;
                mSerialPort.Open();
                mSerialPort.DiscardInBuffer();

                mcHeatMapUpdateTimer.Start();

                return Triggers.ReadMessage;
            }
            catch(InvalidOperationException ex)  // Instance of SerialPort is already open
            {
                mcLogger.Error("_initSerial InvalidOperationException " + ex.Message);
                return Triggers.ReadMessage;
            }
            catch(ArgumentException ex) // invalid port name
            {
                mcLogger.Error("_initSerial ArgumentException " + ex.Message);
                return Triggers.SerialPortNameError;
            }
            catch(IOException ex)
            {
                mcLogger.Error("_initSerial IOException " + ex.Message);
                return Triggers.SerialPortNameError;
            }
        }

        private Triggers _idle()
        {
            return Triggers.ReadMessage;
        }



        private Triggers _finishSelection()
        {
            try
            {
                mSerialPort.Close();  // Close() method calls Dispose() se we don't need to call Dispose()
                mcHeatMapUpdateTimer.Stop();
            }
            catch (IOException ex)
            {
                mcLogger.Error("_finishSelection " + ex.Message);
            }

            return Triggers.Start;
        }

        private void _usbDisconectedError()
        {

        }

        private async Task<Triggers> _serialPortError()
        {
            bool result = false ;
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                result = (bool)await DialogHost.Show(new SerialComPortErrorDialog());

            });

            if (result)
            {
                return Triggers.Initialize;
            }
            else
            {
                return Triggers.Start;
            }
        }


        private Triggers _readMessage()
        {
            try
            {
                _resetData();
                string message = null;

                message = _readSerialInLine();

                if (message == null)
                {
                    return Triggers.Idle;
                }

                mRowNum = ParseMessage.Parse(message, ref mNewRow);

                if (mRowNum < 0 || mRowNum >= NumRadios)
                {
                    return Triggers.Idle;
                }
                else
                {
                    ParseMessage.ProcessMessage(mRowNum, mNewRow);
                    return Triggers.FindClosestPair;
                }
            }
            catch (TimeoutException ex)
            {
                mcLogger.Error("_readMessage TimeoutException " + ex.Message);
                return Triggers.Idle;
            }
            catch (InvalidOperationException ex) // port is closed
            {
                mcLogger.Error("_readMessage InvalidOperationException " + ex.Message);
                return Triggers.Initialize;
            }
            catch (Exception ex)
            {
                mcLogger.Error("_readMessage " + ex.Message);
                return Triggers.Idle;
            }
        }


        private void _resetData()
        {
            mTempCh1 = 0;
            mTempch2 = 0;
        }

        private  bool _calculateRssiStable(int _ch1, int _ch2)
        {
            try
            {
                bool _rssiStable = true;

                for (int _i = 0; _i < StrongRssiBufDepth - 1; _i++)
                {
                    // scoot data in buffer back by one to make room for next
                    mStrongRssiCharacterPairBuf[0, _i] = mStrongRssiCharacterPairBuf[0, _i + 1];
                    mStrongRssiCharacterPairBuf[1, _i] = mStrongRssiCharacterPairBuf[1, _i + 1];
                }

                mStrongRssiCharacterPairBuf[0, StrongRssiBufDepth - 1] = _ch1;
                mStrongRssiCharacterPairBuf[1, StrongRssiBufDepth - 1] = _ch2;

                for (int _i = 0; _i < StrongRssiBufDepth - 2; _i++)
                {
                    if ((mStrongRssiCharacterPairBuf[0, _i] != mStrongRssiCharacterPairBuf[0, _i + 1]
                        || mStrongRssiCharacterPairBuf[1, _i] != mStrongRssiCharacterPairBuf[1, _i + 1])
                        &&
                          (mStrongRssiCharacterPairBuf[0, _i] != mStrongRssiCharacterPairBuf[1, _i + 1]
                        || mStrongRssiCharacterPairBuf[1, _i] != mStrongRssiCharacterPairBuf[0, _i + 1]))
                    {
                        _rssiStable = false;
                        break;
                    }
                }

                return _rssiStable;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private  int _getCharacterMappedIndex(int _radioNum)
        {
            try
            {
                // if we find character return its index , or throw exception if there is no character with 
                //specified radio assigned.  First() - throws exception if no items found
                // FirstOrDefault() - returns first value or default value (for reference type it is null)
                int index = DialogData.Instance.CharacterCollection.Select((c, i) => new { Character = c, Index = i })
                                                                        .Where(x => x.Character.RadioNum == _radioNum)
                                                                        .Select(x => x.Index)
                                                                        .First();

                return index;
            }
            catch (Exception)
            {
                //MessageBox.Show("No character assigned to radio with number " + _radioNum + " .");
                return -1;
            }
        }


        private bool _assignNextCharacters(int _tempCh1, int _tempCh2)
        {
            try
            {
                int _nextCharacter1MappedIndex1, _nextCharacter1MappedIndex2;
                bool _nextCharactersAssigned = false;

                if (mRandom.NextDouble() > 0.5)
                {
                    _nextCharacter1MappedIndex1 = _getCharacterMappedIndex(_tempCh1);
                    _nextCharacter1MappedIndex2 = _getCharacterMappedIndex(_tempCh2);
                }
                else
                {
                    _nextCharacter1MappedIndex1 = _getCharacterMappedIndex(_tempCh2);
                    _nextCharacter1MappedIndex2 = _getCharacterMappedIndex(_tempCh1);
                }

                if (_nextCharacter1MappedIndex1 >= 0 && _nextCharacter1MappedIndex2 >= 0)
                {
                    NextCharacter1 = _nextCharacter1MappedIndex1;
                    NextCharacter2 = _nextCharacter1MappedIndex2;

                    if ((NextCharacter1 != CurrentCharacter1 || NextCharacter2 != CurrentCharacter2) &&
                         (NextCharacter2 != CurrentCharacter1 || NextCharacter1 != CurrentCharacter2))
                    {
                        _nextCharactersAssigned = true;
                    }
                }

                return _nextCharactersAssigned;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private Triggers _findBiggestRssiPair()
        {
            //  This method takes the RSSI values and combines them so that the RSSI for Ch2 looking at 
            //  Ch1 is added to the RSSI for Ch1 looking at Ch2

            try
            {
                bool _rssiStable = false;
                int i = 0, j = 0;

                var _currentTime = DateTime.Now;
                mTempCh1 = NextCharacter1;
                mTempch2 = NextCharacter2;

                if (mTempCh1 > NumRadios - 1 || mTempch2 > NumRadios - 1 ||
                    mTempCh1 < 0 || mTempch2 < 0 || mTempCh1 == mTempch2)
                {
                    mTempCh1 = 0;
                    mTempch2 = 1;
                }

                //only pick up new characters if bigRssi greater not =
                BigRssi = HeatMap[mTempCh1, mTempch2] + HeatMap[mTempch2, mTempCh1];

                for (i = 0; i < NumRadios; i++)
                {
                    // it shouldn't happen often that a character has dissapeared, if so zero him out
                    if (_currentTime - CharactersLastHeatMapUpdateTime[i] > MaxLastSeenInterval)
                    {
                        for (j = 0; j < NumRadios; j++)
                        {
                            HeatMap[i, j] = 0;
                        }
                    }
                    for (j = i + 1; j < NumRadios; j++)
                    {
                        // only need data above the matrix diagonal
                        if (HeatMap[i, j] + HeatMap[j, i] > BigRssi)
                        {
                            // look at both characters view of each other
                            BigRssi = HeatMap[i, j] + HeatMap[j, i];
                            mTempCh1 = i;
                            mTempch2 = j;
                        }
                    }
                }

                _rssiStable = _calculateRssiStable(mTempCh1, mTempch2);

                if (_rssiStable)
                {
                    return Triggers.SelectNextCharacters;
                }
                else
                {
                    return Triggers.ReadMessage;
                }
            }
            catch (Exception ex)
            {
                mcLogger.Error("_findBiggestRssiPair " + ex.Message);
                return Triggers.ReadMessage;
            }
        }


        private Triggers _selectNextCharacters()
        {
            try
            {
                bool _charactersAssigned = _assignNextCharacters(mTempCh1, mTempch2);

                if (_charactersAssigned)
                {
                    //todo notify others componets
                }
            }
            catch(Exception ex)
            {
                mcLogger.Error("_selectNextCharacters " + ex.Message);
            }

            return Triggers.ReadMessage;
        }


        private  string _readSerialInLine()
        {
            string _message = null;

            try
            {
                if (mSerialPort.IsOpen && mSerialPort.BytesToRead > 18)
                {
                    _message = mSerialPort.ReadLine();

                    if (mSerialPort.BytesToRead > 1000)
                    {
                        // got behind for some reason
                        mSerialPort.DiscardInBuffer();

                        mcLogger.Debug("serial buffer over run.");
                    }
                }
            }
            catch (TimeoutException ex)
            {
                throw ex;
            }
            catch (InvalidOperationException ex)  // port is not open
            {
                throw ex;
            }

            return _message;
        }


        #endregion

        #region - public functions -

        public  Task Start()
        {
            return Task.Run(async () =>
            {
                StateMachine.Fire(Triggers.Initialize);

                do
                {
                    switch (StateMachine.State)
                    {
                        case States.Init:
                            {
                                Triggers _nextTrigger = _initSerial();

                                StateMachine.Fire(_nextTrigger);
                                break;
                            }
                        case States.SerialPortNameError:
                            {
                                Triggers _nextTrigger = await _serialPortError();

                                StateMachine.Fire(_nextTrigger);
                                break;
                            }
                        case States.Idle:
                            {
                                Triggers _nextTrigger = _idle();

                                StateMachine.Fire(_nextTrigger);
                                break;
                            }
                        case States.ReadMessage:
                            {
                                Triggers _nextTrigger = _readMessage();

                                StateMachine.Fire(_nextTrigger);
                                break;
                            }
                        case States.FindClosestPair:
                            {
                                Triggers _nextTrigger = _findBiggestRssiPair();

                                StateMachine.Fire(_nextTrigger);
                                break;
                            }
                        case States.SelectNextCharacters:
                            {
                                Triggers _nextTrigger = _selectNextCharacters();

                                StateMachine.Fire(_nextTrigger);
                                break;
                            }
                        case States.Finish:
                            {
                                Triggers _nextTrigger = _finishSelection();

                                StateMachine.Fire(_nextTrigger);
                                break;
                            }
                    }
                } while (StateMachine.State != States.Start);

            });
        }

        public void Stop()
        {
            StateMachine.Fire(Triggers.Finish);
        }


        #endregion

        #region - properties -

        public StateMachine StateMachine
        {
            get { return mStateMachine; }
            set
            {
                mStateMachine = value;
            }
        }

        #endregion
    }
}
