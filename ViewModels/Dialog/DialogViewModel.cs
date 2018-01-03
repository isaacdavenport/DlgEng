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
using System.Windows.Data;
using DialogEngine.Converters;
using DialogEngine.Views.Dialog;

namespace DialogEngine.ViewModels.Dialog
{
    /// <summary>
    ///     Implementation of <see cref="ViewModelBase" />
    ///     DataContext for Dialog.xaml/>
    /// </summary>
    public class DialogViewModel : ViewModelBase
    {
        #region - Fieds -

        //default application logger
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // counter for characters in On state
        private static int mSelectedCharactersOn;

        private static DialogViewModel msInstance = null;

        private static readonly object mcPadlock = new object();

        private readonly DialogTracker mcTheDialogs;

        // reference on view
        private  Views.Dialog.DialogView mView;

        private readonly Random mRandom = new Random();

        // detect is dialog model changed, true value force application to reload dialog model
        private bool mIsModelsDialogChanged;

        private int mSelectedIndex1;
        private int mSelectedIndex2;

        // create token which we pass to background method, so we can force method to finish executing
        private CancellationTokenSource _cancellationTokensource;

        // collection of dialog lines
        private ObservableCollection<object> mDialogLinesCollection;

        // Combobox item sources
        private ObservableCollection<CharacterInfo> mCharacterCollection;
        private ObservableCollection<ModelDialogInfo> mDialogModelCollection;

        // Collections of debug messages
        private ObservableCollection<InfoMessage> mInfoMessagesCollection;
        private ObservableCollection<WarningMessage> mWarningMessagesCollection;
        private ObservableCollection<ErrorMessage> mErrorMessagesCollection;

        #endregion


        #region - Singleton - 

        /// <summary>
        /// Implementation of singleton pattern for DialogTracker class
        /// </summary>
        public static DialogViewModel Instance
        {
            get
            {
                lock (mcPadlock)
                {
                    if (msInstance == null)
                    {
                        msInstance = new DialogViewModel();
                    }
                    return msInstance;
                }
            }
        }

        #endregion

        #region - Constructor -

        /// <summary>
        /// Creates instance of DialogViewModel.cs
        /// </summary>
        public DialogViewModel()
        {
            EventAggregator.Instance.GetEvent<ChangedCharactersStateEvent>().Subscribe(_onChangedCharacterState);
            EventAggregator.Instance.GetEvent<ChangedModelDialogStateEvent>().Subscribe(_onChangedModelDialogState);


            mcTheDialogs = DialogTracker.Instance;

            _bindCommands();
        }

        #endregion

        #region - Properties -


        /// <summary>
        /// Dynamic collection of objects, objects can be added,removed or updated, and UI is automatically updated
        /// Source for ItemsControl where we display dialog lines
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

        public int SelectedIndex1 { get => mSelectedIndex1; set => mSelectedIndex1 = value; }


        public int SelectedIndex2 { get => mSelectedIndex2; set => mSelectedIndex2 = value; }

        public DialogView View
        {
            get
            {
                return mView;
            }

            set
            {
                this.mView = value;
            }
        }

        /// <summary>
        /// Counter for characters in "On" state
        /// </summary>
        public static int SelectedCharactersOn
        {
            get
            {
                return mSelectedCharactersOn;
            }

            set
            {
                mSelectedCharactersOn = value;
            }
        }

        /// <summary>
        /// Collection of <see cref="CharacterInfo"/>
        /// Source for characters combobox
        /// </summary>
        public ObservableCollection<CharacterInfo> CharacterCollection
        {
            get
            {
                if (mCharacterCollection == null)
                {
                    mCharacterCollection = new ObservableCollection<CharacterInfo>();

                }

                return mCharacterCollection;
            }

            set
            {
                    mCharacterCollection = value;

                    // send notification to view (model is changed)
                    OnPropertyChanged("CharacterCollection");
            }
        }


        /// <summary>
        /// Collection of <see cref="ModelDialogInfo"/>
        /// Source for model dialogs combobox
        /// </summary>
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

                // send notification to view (model is changed)
                OnPropertyChanged("DialogModelCollection");
            }
        }

        /// <summary>
        /// Collection of <see cref="ErrorMessage"/>
        /// Source for GridView in "ErrorMessage" TabItem
        /// </summary>
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

        /// <summary>
        /// Collection of <see cref="WarningMessage"/>
        /// Source for GridView in "WarningMessage" TabItem
        /// </summary>
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

        /// <summary>
        /// Collection of <see cref="InfoMessage"/>
        /// Source for GridView in "InfoMessage" TabItem
        /// </summary>
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

        /// <summary>
        /// Start dialog 
        /// </summary>
        public RelayCommand GenerateDialog { get; set; }

        /// <summary>
        /// Clear messages depends on message type
        /// </summary>
        public RelayCommand ClearAllMessages { get; set; }

        /// <summary>
        /// Update bindings for columns width with <see cref="StarWidthConverter"/>
        /// </summary>
        public RelayCommand RefreshTabItem { get; set; }


        #endregion

        #region - Private methods -

        // refresh SelectedCharactersOn when character state is changed
        private void _onChangedCharacterState()
        {
            int result = 0;
            SelectedCharactersOn = 0;

            SelectedIndex1 = -1;
            SelectedIndex2 = -1;

            int index = 0;
            
            foreach(CharacterInfo characterInfo in CharacterCollection)
            {
                if (characterInfo.State == Models.Enums.CharacterState.On)
                {
                    string fieldName = "mSelectedIndex" + (result + 1);

                    var field = this.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

                    field.SetValue(this, index);

                    result += 1;

                    if (result == 2)
                        break;
                }

                index++;
            }

            SelectedCharactersOn = result;

            OnPropertyChanged("SelectedCharactersOn");

        }


        // change indicator if state of dialog models changed
        private void _onChangedModelDialogState()
        {
            mIsModelsDialogChanged = true;
        }


        // choose collection where to add object depend on type of argument
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

            RefreshTabItem = new RelayCommand(x => _refreshTabItem());
        }


        // force TabItem to refresh binding for GridView columns width
        private void _refreshTabItem()
        {
            GridViewColumn column = mView.InfoGridViewColumn;

            BindingOperations.GetBindingExpression(column, GridViewColumn.WidthProperty).UpdateTarget();
        }


        // clear collection depend of argument type
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


            foreach (var _character in mcTheDialogs.CharacterList)
            {
                foreach (var _phrase in _character.Phrases)
                {
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
                                _jsonLog.WriteLine("missing " + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " + _phrase.DialogStr);
                            }
                    }
                }

            }

            //TODO check that all dialog models have unique names
        }


        // loads information about dialog models
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


        // loads information about characters
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

                        using (var _reader = new StreamReader(_fs)) //creates new streamerader for fs stream. Could also construct with filename...
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
                        mcLogger.Error(ex.Message);
                    }
                }
            });


            return _charactersInfo;
        }



        private void checkTagsUsed(DialogTracker _dialogTracker)
        {
            foreach (var _dialog in mcTheDialogs.ModelDialogs)
            {
                AddItem(new InfoMessage(" " + _dialogTracker.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name));


                if (SessionVariables.WriteSerialLog)
                {
                    using (var _jsonLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                    {
                        _jsonLog.WriteLine(" " + _dialogTracker.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name);
                    }
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
                            {
                                using (var _jsonLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                                {
                                    _jsonLog.WriteLine(" " + _phrasetag + " is not used.");
                                }
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
                            using (var _jsonLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                            {
                                _jsonLog.WriteLine(" " + _dialogtag + " not used in " + _dialog.Name);
                            }
                    }
                }
        }

      
        
        private  void _startDialog()
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

            // pass cancellationToken to method which can be used to force async method to finish executing
            _dialogWorkerMethod(_cancellationTokensource.Token);           
        }



        private async void _dialogWorkerMethod(CancellationToken _cancellationToken)
        {

            try
            {
                await Task.Run(() =>
                  {
                      while (!_cancellationToken.IsCancellationRequested)
                      {

                          if (mIsModelsDialogChanged == true)
                          {
                              InitModelDialogs.SetDefaults(mcTheDialogs, DialogModelCollection);

                              mIsModelsDialogChanged = false;
                          }


                          if (SelectedCharactersOn == 2)
                          {
                              var _SelectedCharacters = new int[2];

                              _SelectedCharacters[0] = SelectedIndex1;

                              _SelectedCharacters[1] = SelectedIndex2;

                              mcTheDialogs.GenerateADialog(_cancellationToken, _SelectedCharacters);
                          }
                          else if(SelectedCharactersOn == 1)
                          {
                              var _SelectedCharacters = new int[2];

                              _SelectedCharacters[0] = SelectedIndex1 == -1 ? SelectedIndex2 : SelectedIndex1;

                              _SelectedCharacters[1] = SelectNextCharacters.GetNextCharacter(_SelectedCharacters[0]);

                              mcTheDialogs.GenerateADialog(_cancellationToken, _SelectedCharacters);

                          }
                          else
                          {
                              if (!SessionVariables.HeatMapOnlyMode)
                              {

                                  mcTheDialogs.GenerateADialog(_cancellationToken); //normal operation

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

            }
            catch (TaskCanceledException _ex)
            {
                mcLogger.Error(_ex.Message);

            }

        }


        #endregion

        #region - Public methods -

        /// <summary>
        /// Add dialog item 
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

        /// <summary>
        /// Initialize  dialog data (characters and dialog models)
        /// </summary>
        public async void InitDialogData()
        {
            Task task = Task.Run(() =>
            {
                writeStartupInfo();

                if (SessionVariables.TagUsageCheck)
                    checkTagsUsed(mcTheDialogs);


                if (SessionVariables.DebugFlag)
                    checkForMissingPhrases();

            });


            InitModelDialogs.SetDefaults(mcTheDialogs);

            mcTheDialogs.IntakeCharacters();


            Task<ObservableCollection<CharacterInfo>> _charactersTask = _loadAllCharacterNames(SessionVariables.CharactersDirectory);
            Task<ObservableCollection<ModelDialogInfo>> _modelDialogTask = _loadAllDialogModels(SessionVariables.DialogsDirectory);


            CharacterCollection = await _charactersTask;
            DialogModelCollection = await _modelDialogTask;

            SerialComs.InitSerial();
        }

        #endregion

    }
}