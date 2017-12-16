//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DialogEngine.Core;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using log4net;
using Newtonsoft.Json;
using DialogEngine.Models.Logger;
using System.Threading.Tasks;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;

namespace DialogEngine.ViewModels.Dialog
{
    /// <summary>
    ///     Implementation of <see cref="ViewModelBase" />
    ///     DataContext for Dialog.xaml/>
    /// </summary>
    public class DialogViewModel : ViewModelBase
    {
        #region - Fieds -

        #region -Private fields-

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DialogTracker TheDialogs;
        private readonly Views.Dialog.DialogView mView;
        private readonly Random mRandom = new Random();
        private BackgroundWorker _dialogWorker;
        private ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private bool _isModelsDialogChanged = false;
        private CancellationTokenSource _cancellationTokensource;



        private ObservableCollection<object> mDialogLinesCollection;

        private string mSelectedCharacter1="";
        private string mSelectedCharacter2 = "";

        // Combobox item sources
        private ObservableCollection<CharacterInfo> mCharacter1Collection;
        private ObservableCollection<ModelDialogInfo> mDialogModelCollection;
        private ObservableCollection<InfoMessage> mInfoMessagesCollection;
        private ObservableCollection<WarningMessage> mWarningMessagesCollection;
        private ObservableCollection<ErrorMessage> mErrorMessagesCollection;

        #endregion

        #region - Public fields -

        #endregion

        #endregion

        #region - Constructor -

        public DialogViewModel(Views.Dialog.DialogView _view)
        {
            EventAggregator.Instance.GetEvent<ChangedCharactersStateEvent>().Subscribe(_onChangedCharacterState);
            EventAggregator.Instance.GetEvent<ChangedModelDialogStateEvent>().Subscribe(_onChangedModelDialogState);

            mView = _view;

            TheDialogs = DialogTracker.Instance;

            TheDialogs.AddItem = new DialogTracker.PrintMethod(AddItem);
            InitModelDialogs.AddItem = new InitModelDialogs.PrintMethod(AddItem);

            _bindCommands();

            _initDialogData();

        }

        #endregion

        #region - Properties -


        public string SelectedCharacter1
        {
            get
            {
                return mSelectedCharacter1;
            }

            set
            {
                mSelectedCharacter1 = value;

                OnPropertyChanged("SelectedCharacter1");
            }
        }


        public string SelectedCharacter2
        {
            get
            {
                return mSelectedCharacter2;
            }

            set
            {
                mSelectedCharacter2 = value;

                OnPropertyChanged("SelectedCharacter2");
            }
        }

        /// <summary>
        /// Dynamic collection of objects, objects can be added,removed or updated, and UI is automatically updated
        /// </summary>
        public ObservableCollection<object> DialogLinesCollection
        {
            get
            {
                if (mDialogLinesCollection == null)
                    mDialogLinesCollection = new ObservableCollection<object>();

                return mDialogLinesCollection;
            }

            set
            {
                mDialogLinesCollection = value;

                // send notification to view (model is changed)
                OnPropertyChanged("DialogLinesCollection");
            }
        }

        /// <summary>
        /// Character 1 combobox item source
        /// </summary>
        public ObservableCollection<CharacterInfo> Character1Collection
        {
            get
            {
                if (mCharacter1Collection == null)
                    mCharacter1Collection = new ObservableCollection<CharacterInfo>();

                return mCharacter1Collection;
            }

            set
            {
                mCharacter1Collection = value;

                // send notification to view (model is changed)
                OnPropertyChanged("Character1Collection");
            }
        }



        public ObservableCollection<ModelDialogInfo> DialogModelCollection
        {
            get
            {
                if (mDialogModelCollection == null)
                    mDialogModelCollection = new ObservableCollection<ModelDialogInfo>();
                return mDialogModelCollection;
            }

            set
            {
                mDialogModelCollection = value;

                _isModelsDialogChanged = true;
                // send notification to view (model is changed)
                OnPropertyChanged("DialogModelCollection");
            }
        }

        public ObservableCollection<ErrorMessage> ErrorMessagesCollection
        {
            get
            {
                if (mErrorMessagesCollection == null)
                {
                    mErrorMessagesCollection = new ObservableCollection<ErrorMessage>();
                }

                return mErrorMessagesCollection;
            }

            set
            {
                mErrorMessagesCollection = value;

                OnPropertyChanged("ErrorMessagesCollection");

            }
        }

        public ObservableCollection<WarningMessage> WarningMessagesCollection
        {
            get
            {
                if (mWarningMessagesCollection == null)
                {
                    mWarningMessagesCollection = new ObservableCollection<WarningMessage>();
                }

                return mWarningMessagesCollection;
            }

            set
            {
                mWarningMessagesCollection = value;

                OnPropertyChanged("WarningMessagesCollection");

            }
        }

        public ObservableCollection<InfoMessage> InfoMessagesCollection
        {
            get
            {
                if (mInfoMessagesCollection == null)
                {
                    mInfoMessagesCollection = new ObservableCollection<InfoMessage>();
                }

                return mInfoMessagesCollection;
            }

            set
            {
                mInfoMessagesCollection = value;

                OnPropertyChanged("InfoMessagesCollection");

            }
        }



        #endregion

        #region - Commands -

        public RelayCommand GenerateDialog { get; set; }

        public RelayCommand ClearAllMessages { get; set; }

        #endregion

        #region - Private methods -

        private void _onChangedCharacterState()
        {
        }

        private void _onChangedModelDialogState()
        {
            _isModelsDialogChanged = true;
        }

        private void _processAddItem(object _entry)
        {
            try
            {
                if (_entry is DialogItem || _entry is String)
                {
                    DialogLinesCollection.Add(_entry);

                    OnPropertyChanged("DialogLinesCollection");

                    var scrollViewer = VisualTreeHelper.GetChild(mView.textOutput, 0) as ScrollViewer;

                    scrollViewer.ScrollToBottom();
                }
                else if (_entry is InfoMessage)
                {
                    InfoMessagesCollection.Insert(0, (InfoMessage)_entry);

                    int _length = InfoMessagesCollection.Count;

                    if (_length > 300)
                    {
                        WarningMessagesCollection.RemoveAt(_length - 1);
                    }

                    OnPropertyChanged("InfoMessagesCollection");

                }
                else if (_entry is WarningMessage)
                {
                    WarningMessagesCollection.Insert(0, (WarningMessage)_entry);

                    int _length = WarningMessagesCollection.Count;

                    if (_length > 300)
                    {
                        WarningMessagesCollection.RemoveAt(_length - 1);
                    }

                    OnPropertyChanged("WarningMessagesCollection");

                }
                else
                {
                    ErrorMessagesCollection.Insert(0, (ErrorMessage)_entry);

                    int _length = ErrorMessagesCollection.Count;

                    if (_length > 300)
                    {
                        ErrorMessagesCollection.RemoveAt(_length - 1);
                    }

                    OnPropertyChanged("ErrorMessagesCollection");
                }
            }
            catch (Exception _e)
            {
                mcLogger.Error(_e.Message);
            }
        }

        private void _bindCommands()
        {
            GenerateDialog = new RelayCommand(_x => _startDialog());

            ClearAllMessages = new RelayCommand(x => _clearAllMessages((string)x));
        }



        private void _clearAllMessages(string type)
        {
            switch (type)
            {
                case "DialogLinesCollection":
                    {
                        DialogLinesCollection.Clear();

                        OnPropertyChanged(type);

                        break;
                    }

                case "InfoMessagesCollection":
                    {
                        InfoMessagesCollection.Clear();

                        OnPropertyChanged(type);

                        break;
                    }

                case "WarningMessagesCollection":
                    {
                        WarningMessagesCollection.Clear();

                        OnPropertyChanged(type);

                        break;
                    }

                case "ErrorMessagesCollection":
                    {
                        ErrorMessagesCollection.Clear();

                        OnPropertyChanged(type);

                        break;
                    }
            }
        }

        private void writeStartupInfo()
        {
            if (SessionVariables.WriteSerialLog)
            {
                var _versionTimeStr = "Dialog Engine ver 0.67 " + DateTime.Now;


                AddItem(new InfoMessage(_versionTimeStr));

                using (var _serialLog =
                    new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.HexLogFileName, true))
                {
                    _serialLog.WriteLine("");
                    _serialLog.WriteLine("");
                    _serialLog.WriteLine(_versionTimeStr);
                    _serialLog.Close();
                }

                using (var _serialLogDec =
                    new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DecimalLogFileName, true))
                {
                    _serialLogDec.WriteLine("");
                    _serialLogDec.WriteLine("");
                    _serialLogDec.WriteLine(_versionTimeStr);
                    _serialLogDec.Close();
                }

                using (var _serialLogDialog =
                    new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                {
                    _serialLogDialog.WriteLine("");
                    _serialLogDialog.WriteLine("");
                    _serialLogDialog.WriteLine(_versionTimeStr);
                    _serialLogDialog.Close();
                }
            }
        }


        private void checkForMissingPhrases()
        {
            if (!SessionVariables.AudioDialogsOn)
                return;


            foreach (var _character in TheDialogs.CharacterList)
            foreach (var _phrase in _character.Phrases)
                if (!File.Exists(SessionVariables.AudioDirectory
                                 + _character.CharacterPrefix + "_"
                                 + _phrase.FileName + ".mp3")) //Char name and prefix are being left blank...
                {
                    var _debugMessage = "Missing " + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " + _phrase.DialogStr;

                    AddItem(new WarningMessage(_debugMessage));


                    if (SessionVariables.WriteSerialLog)
                        using (var _jsonLog =
                            new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                        {
                            _jsonLog.WriteLine("missing " + _character.CharacterPrefix + "_" + _phrase.FileName +
                                               ".mp3 " + _phrase.DialogStr);
                        }
                }
            //TODO check that all dialog models have unique names
        }

        private async Task<ObservableCollection<ModelDialogInfo>> _loadAllDialogModels(string _path)
        {
            var _dialogModels = new ObservableCollection<ModelDialogInfo>();




            await Task.Run(() =>
            {
                var _directoryInfo = new DirectoryInfo(_path);


                foreach (var _file in _directoryInfo.GetFiles("*.json"))
                {
                    _dialogModels.Add(new ModelDialogInfo() { FileName = Path.GetFileNameWithoutExtension(_file.FullName) });
                }

            });

            return _dialogModels;
        }




        private async Task<ObservableCollection<CharacterInfo>> _loadAllCharacterNames(string _path)
        {

            var _charactersInfo = new ObservableCollection<CharacterInfo>();


            await Task.Run(() =>
            {
                var _directoryInfo = new DirectoryInfo(_path);


                foreach (var _file in _directoryInfo.GetFiles("*.json")) //file of type FileInfo for each .json in directory
                {
                    string _inChar;

                    try
                    {
                        var _fs = _file.OpenRead(); //open a read-only FileStream

                        using (var _reader = new StreamReader(_fs)
                        ) //creates new streamerader for fs stream. Could also construct with filename...
                        {
                            try
                            {
                                _inChar = _reader.ReadToEnd();

                                var _deserializedCharacterJson = JsonConvert.DeserializeObject<CharacterInfo>(_inChar);

                                _deserializedCharacterJson.FileName = _file.Name;

                                _charactersInfo.Add(_deserializedCharacterJson);
                            }
                            catch (Exception ex)
                            {
                                mcLogger.Error(ex.Message);
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                    }
                }
            });


            return _charactersInfo;
        }

        private void checkTagsUsed(DialogTracker _dialogTracker)
        {
            foreach (var _dialog in TheDialogs.ModelDialogs)
            {
                AddItem(new InfoMessage(" " + _dialogTracker.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name));


                if (SessionVariables.WriteSerialLog)
                    using (var _jsonLog =
                        new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                    {
                        _jsonLog.WriteLine(" " + _dialogTracker.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name);
                    }
            }

            //test that all character tags are used by a dialog model.
            AddItem(new InfoMessage("Check characters tags are used "));

            var _usedFlag = false;

            foreach (var _character in _dialogTracker.CharacterList)
            foreach (var _phrase in _character.Phrases)
            foreach (var _phrasetag in _phrase.PhraseWeights.Keys)
            {
                _usedFlag = false;


                foreach (var _dialog in _dialogTracker.ModelDialogs)
                {
                    foreach (var _dialogtag in _dialog.PhraseTypeSequence)
                        if (_phrasetag == _dialogtag)
                        {
                            _usedFlag = true;
                            break;
                        }


                    if (_usedFlag)
                        break;
                }


                if (!_usedFlag)
                {
                    AddItem(new InfoMessage(" " + _phrasetag + " is not used."));


                    if (SessionVariables.WriteSerialLog)
                        using (var _jsonLog = new StreamWriter(
                            SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName,true))
                        {
                            _jsonLog.WriteLine(" " + _phrasetag + " is not used.");
                        }
                }
            }


            AddItem(new InfoMessage("Check dialogs tags are used"));


            foreach (var _dialog in _dialogTracker.ModelDialogs)
            foreach (var _dialogtag in _dialog.PhraseTypeSequence) //each dialog model tag
            {
                _usedFlag = false;

                foreach (var _character in _dialogTracker.CharacterList)
                {
                    foreach (var _characterPhrase in _character.Phrases)
                    {
                        foreach (var _phraseTag in _characterPhrase.PhraseWeights.Keys) //each character phrase tag{
                            if (_dialogtag == _phraseTag)
                            {
                                _usedFlag = true;
                                break;
                            }


                        if (_usedFlag)
                            break;
                    }


                    if (_usedFlag)
                        break;
                }


                if (!_usedFlag)
                {
                    AddItem(new InfoMessage(" " + _dialogtag + " not used in " + _dialog.Name));


                    if (SessionVariables.WriteSerialLog)
                        using (var _jsonLog =
                            new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                        {
                            _jsonLog.WriteLine(" " + _dialogtag + " not used in " + _dialog.Name);
                        }
                }
            }
        }

        private async void _initDialogData()
        {


            var _workerLoader = new BackgroundWorker();

            _workerLoader.DoWork += (_sender, _e) =>
            {
                writeStartupInfo();

                InitModelDialogs.SetDefaults(TheDialogs);

                TheDialogs.IntakeCharacters();


                if (SessionVariables.TagUsageCheck)
                    checkTagsUsed(TheDialogs);


                if (SessionVariables.DebugFlag)
                    checkForMissingPhrases();
            };

            _workerLoader.RunWorkerAsync();


            Task<ObservableCollection<CharacterInfo>> _charactersTask =  _loadAllCharacterNames(SessionVariables.CharactersDirectory);
            Task<ObservableCollection<ModelDialogInfo>> _modelDialogTask = _loadAllDialogModels(SessionVariables.DialogsDirectory);

            Character1Collection = await _charactersTask;
            DialogModelCollection = await _modelDialogTask;

        }

        public  void _startDialog()
        {
            DialogLinesCollection.Clear();

            OnPropertyChanged("DialogLinesCollection");

            if(_cancellationTokensource == null)
            {
                _cancellationTokensource = new CancellationTokenSource();
            }
            else
            {
                _cancellationTokensource.Cancel();

                _cancellationTokensource = new CancellationTokenSource();
            }


            _dialogWorkerMethod(_cancellationTokensource.Token);

           
        }


        private async void _dialogWorkerMethod(CancellationToken _cancellationToken)
        {
            Task task =Task.Run(() =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                {

                    if (_isModelsDialogChanged == true)
                    {
                        InitModelDialogs.SetDefaults(TheDialogs, DialogModelCollection);

                        _isModelsDialogChanged = false;
                    }



                    if (SessionVariables.ForceCharactersAndDialogModel)
                    {

                        var _modelAndCharacters = new int[2];

                        //_modelAndCharacters[0] = 1;

                        _modelAndCharacters[0] = Int32.Parse(SelectedCharacter1);

                        _modelAndCharacters[1] = Int32.Parse(SelectedCharacter2);

                        TheDialogs.GenerateADialog(_cancellationToken,_modelAndCharacters);
                    }
                    else
                    {
                        if (!SessionVariables.HeatMapOnlyMode)
                        {
                            TheDialogs.GenerateADialog(_cancellationToken); //normal operation

                            Thread.Sleep(1100); //vb:commented out for debugging as code stops here

                            Thread.Sleep(mRandom.Next(0, 2000)); //vb:commented out for debugging as code stops here
                        }
                        else
                        {
                            if (SessionVariables.HeatMapFullMatrixDispMode)
                                FirmwareDebuggingTools.PrintHeatMap();


                            if (SessionVariables.HeatMapSumsMode)
                                FirmwareDebuggingTools.PrintHeatMapSums();


                            Thread.Sleep(400); //vb:commented out for debugging as code stops here
                        }
                    }
                }
            }, _cancellationToken);

            await task;
        }


        #endregion

        #region - Public methods -

        /// <summary>
        /// Add dialog item line
        /// </summary>
        /// <param name="_entry">Line to be added</param>
        public void AddItem(object _entry)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                _processAddItem(_entry);
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    _processAddItem(_entry);
                }));
            }

        }

        #endregion

    }
}