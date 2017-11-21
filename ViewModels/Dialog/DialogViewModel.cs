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
        private readonly Views.Dialog.Dialog mView;
        private readonly Random mRandom = new Random();

        // 
        private ObservableCollection<object> mDialogLinesCollection;

        // Combobox item sources
        private ObservableCollection<CharacterInfo> mCharacter1Collection;
        private ObservableCollection<CharacterInfo> mCharacter2Collection;
        private ObservableCollection<string> mDialogModelCollection;

        // Character n combobox item index
        private int mSelected1CharacterIndex;
        private int mSelected2CharacterIndex = 1;  // preventing to default to the same character name
        private int mSelectedDialogModelIndex;

        #endregion


        #region - Public fields -

        #endregion

        #region - Constructor -

        public DialogViewModel(Views.Dialog.Dialog _view)
        {
            mView = _view;

            TheDialogs = DialogTracker.Instance;

            bindCommands();

            mCharacter1Collection = _loadAllCharacterNames(SessionVariables.CharactersDirectory);

            mCharacter2Collection = new ObservableCollection<CharacterInfo>(mCharacter1Collection);

            mDialogModelCollection = _loadAllDialogModels(SessionVariables.DialogsDirectory);

            _initDialogData();
        }

        #endregion

        #region - Commands -

        public RelayCommand GenerateDialog { get; set; }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Add dialog item line
        /// </summary>
        /// <param name="_entry">Line to be added</param>
        public void AddDialogItem(object _entry)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                try
                {
                    DialogLinesCollection.Add(_entry);

                    OnPropertyChanged("DialogLinesCollection");

                    var scrollViewer = VisualTreeHelper.GetChild(mView.textOutput, 0) as ScrollViewer;

                    scrollViewer.ScrollToBottom();
                }
                catch (Exception e)
                {
                    mcLogger.Error(e.Message);
                }
            }

            else
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    try
                    {
                        DialogLinesCollection.Add(_entry);

                        OnPropertyChanged("DialogLinesCollection");

                        var scrollViewer = VisualTreeHelper.GetChild(mView.textOutput, 0) as ScrollViewer;

                        scrollViewer.ScrollToBottom();
                    }
                    catch (Exception _e)
                    {
                        mcLogger.Error(_e.Message);
                    }
                } ));
            }

        }

        #endregion



        #endregion

        #region - Properties -

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

        /// <summary>
        /// Character 2 combobox item source
        /// </summary>
        public ObservableCollection<CharacterInfo> Character2Collection
        {
            get
            {
                if (mCharacter2Collection == null)
                    mCharacter2Collection = new ObservableCollection<CharacterInfo>();

                return mCharacter2Collection;
            }

            set
            {
                mCharacter2Collection = value;

                // send notification to view (model is changed)
                OnPropertyChanged("Character2Collection");
            }
        }


        public int Selected1CharacterIndex
        {
            get => mSelected1CharacterIndex;

            set
            {
                mSelected1CharacterIndex = value;

                // send notification to view (model is changed)
                OnPropertyChanged("Selected1CharacterIndex");
            }
        }

        public int Selected2CharacterIndex
        {
            get => mSelected2CharacterIndex;

            set
            {
                mSelected2CharacterIndex = value;

                // send notification to view (model is changed)
                OnPropertyChanged("Selected2CharacterIndex");
            }
        }

        public int SelectedDialogModelIndex
        {
            get => mSelectedDialogModelIndex;

            set
            {
                mSelectedDialogModelIndex = value;

                // send notification to view (model is changed)
                OnPropertyChanged("SelectedDialogModel");
            }
        }

        public ObservableCollection<string> DialogModelCollection
        {
            get
            {
                if (mDialogModelCollection == null)
                    mDialogModelCollection = new ObservableCollection<string>();
                return mDialogModelCollection;
            }

            set
            {
                mDialogModelCollection = value;

                // send notification to view (model is changed)
                OnPropertyChanged("DialogModelCollection");
            }
        }

        #endregion

        #region - Private methods -

        private void bindCommands()
        {
            GenerateDialog = new RelayCommand(_x => _generateDialogClick());
        }


        private void _generateDialogClick()
        {
            StartDialog();
        }


        private void writeStartupInfo()
        {
            if (SessionVariables.WriteSerialLog)
            {
                var _versionTimeStr = "Dialog Engine ver 0.67 " + DateTime.Now;

                AddDialogItem(_versionTimeStr);

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
                    var _debugMessage = "missing " + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " +
                                        _phrase.DialogStr;

                    //AddDialogItem(_debugMessage);


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

        private ObservableCollection<string> _loadAllDialogModels(string _path)
        {
            var _dialogModels = new ObservableCollection<string>();

            var _directoryInfo = new DirectoryInfo(_path);

            foreach (var _file in _directoryInfo.GetFiles("*.json"))
                _dialogModels.Add(Path.GetFileNameWithoutExtension(_file.FullName));

            return _dialogModels;
        }


        private ObservableCollection<CharacterInfo> _loadAllCharacterNames(string _path)
        {
            var _directoryInfo = new DirectoryInfo(_path);

            var _charactersInfo = new ObservableCollection<CharacterInfo>();

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

            return _charactersInfo;
        }

        private void checkTagsUsed(DialogTracker _dialogTracker)
        {
            foreach (var _dialog in TheDialogs.ModelDialogs)
            {
                AddDialogItem(" " + _dialogTracker.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name);


                if (SessionVariables.WriteSerialLog)
                    using (var _jsonLog =
                        new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                    {
                        _jsonLog.WriteLine(" " + _dialogTracker.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name);
                    }
            }

            //test that all character tags are used by a dialog model.
            AddDialogItem("Check characters tags are used ");

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
                    AddDialogItem(" " + _phrasetag + " is not used.");


                    if (SessionVariables.WriteSerialLog)
                        using (var _jsonLog = new StreamWriter(
                            SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName,
                            true))
                        {
                            _jsonLog.WriteLine(" " + _phrasetag + " is not used.");
                        }
                }
            }


            AddDialogItem("Check dialogs tags are used");


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
                    AddDialogItem(" " + _dialogtag + " not used in " + _dialog.Name);


                    if (SessionVariables.WriteSerialLog)
                        using (var _jsonLog =
                            new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                        {
                            _jsonLog.WriteLine(" " + _dialogtag + " not used in " + _dialog.Name);
                        }
                }
            }
        }

        private void _initDialogData()
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
        }

        public void StartDialog()
        {
            var _worker = new BackgroundWorker();

            _worker.WorkerReportsProgress = false;
            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _worker.RunWorkerAsync(400);
        }

        private void worker_DoWork(object _sender, DoWorkEventArgs _e)
        {
            while (true)
            {
                if (SessionVariables.ForceCharactersAndDialogModel)
                {
                    var _modelAndCharacters = new int[3];

                    _modelAndCharacters[0] = SelectedDialogModelIndex;

                    _modelAndCharacters[1] = Selected1CharacterIndex;

                    _modelAndCharacters[2] = Selected2CharacterIndex;

                    TheDialogs.GenerateADialog(_modelAndCharacters);
                }
                else
                {
                    if (!SessionVariables.HeatMapOnlyMode)
                    {
                        TheDialogs.GenerateADialog(); //normal operation

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
        }


        private void worker_RunWorkerCompleted(object _sender, RunWorkerCompletedEventArgs _e)
        {
            //MessageBoxResult _result = MessageBox.Show("While loop completed");
        }

        #endregion
    }
}