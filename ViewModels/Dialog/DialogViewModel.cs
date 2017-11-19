using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DialogEngine.Core;
using DialogEngine.Helpers;
using System.IO;
using System.Threading;
using System.Windows;
using Newtonsoft.Json;
using DialogEngine.Models.Dialog;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using log4net;

namespace DialogEngine.ViewModels.Dialog
{
    /// <summary>
    /// Implementation of <see cref="ViewModelBase"/>
    /// DataContext for Dialog.xaml/>
    /// </summary>
    public class DialogViewModel : ViewModelBase
    {
        #region - Fieds -

        #region -Private fields-
        private static readonly ILog mcLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private DialogTracker TheDialogs;
        private Views.Dialog.Dialog mView;
        private Random mRandom = new Random();
        private ObservableCollection<Object> mDialogLinesCollection;
        private ObservableCollection<CharacterInfo> mCharacter1Collection;
        private ObservableCollection<CharacterInfo> mCharacter2Collection;
        private ObservableCollection<string> mDialogModelCollection;
        private int mSelected1CharacterIndex=0;
        private int mSelected2CharacterIndex=1;
        private int mSelectedDialogModelIndex=0;

        #endregion


        #region - Public fields -

        #endregion

        #endregion

        #region - Constructor -

        public DialogViewModel(Views.Dialog.Dialog _view)
        {
            mView = _view;

            TheDialogs = DialogTracker.Instance;

            bindCommands();

            this.mCharacter1Collection = this._loadAllCharacterNames(SessionVariables.CharactersDirectory);

            this.mCharacter2Collection = new ObservableCollection<CharacterInfo>(this.mCharacter1Collection);

            this.mDialogModelCollection = this._loadAllDialogModels(SessionVariables.DialogsDirectory);

            _initDialogData();
        }

        #endregion

        #region - Properties -
        /// <summary>
        /// Dynamic collection of objects, objects can be added,removed or updated, and UI is automatically updated 
        /// </summary>
        public ObservableCollection<Object> DialogLinesCollection
        {
            get
            {
                if (mDialogLinesCollection == null)
                {
                    mDialogLinesCollection = new ObservableCollection<Object>();
                }
                return mDialogLinesCollection;
            }

            set
            {
                mDialogLinesCollection = value;

                // send notification to view (model is changed)
                OnPropertyChanged("DialogLinesCollection");

                

            }

        }

        public ObservableCollection<CharacterInfo> Character1Collection
        {
            get
            {
                if (this.mCharacter1Collection == null)
                {
                    this.mCharacter1Collection = new ObservableCollection<CharacterInfo>();
                }
                return this.mCharacter1Collection;
            }

            set
            {
                this.mCharacter1Collection = value;

                // send notification to view (model is changed)
                OnPropertyChanged("Character1Collection");

            }

        }

        public ObservableCollection<CharacterInfo> Character2Collection
        {
            get
            {
                if (this.mCharacter2Collection == null)
                {
                    this.mCharacter2Collection = new ObservableCollection<CharacterInfo>();
                }
                return this.mCharacter2Collection;
            }

            set
            {
                this.mCharacter2Collection = value;

                // send notification to view (model is changed)
                OnPropertyChanged("Character2Collection");
            }

        }

        public int Selected1CharacterIndex
        {
            get
            {

                return this.mSelected1CharacterIndex;
            }

            set
            {
                this.mSelected1CharacterIndex = value;

                // send notification to view (model is changed)
                OnPropertyChanged("Selected1CharacterIndex");
            }

        }

        public int Selected2CharacterIndex
        {
            get
            {

                return this.mSelected2CharacterIndex;
            }

            set
            {
                this.mSelected2CharacterIndex = value;

                // send notification to view (model is changed)
                OnPropertyChanged("Selected2CharacterIndex");
            }

        }

        public int SelectedDialogModelIndex
        {
            get
            {

                return this.mSelectedDialogModelIndex;
            }

            set
            {
                this.mSelectedDialogModelIndex = value;

                // send notification to view (model is changed)
                OnPropertyChanged("SelectedDialogModel");
            }

        }

        public ObservableCollection<string> DialogModelCollection
        {
            get
            {
                if (this.mDialogModelCollection == null)
                {
                    this.mDialogModelCollection = new ObservableCollection<string>();
                }
                return this.mDialogModelCollection;
            }

            set
            {
                this.mDialogModelCollection = value;

                // send notification to view (model is changed)
                OnPropertyChanged("DialogModelCollection");
            }

        }


        #endregion

        #region - Commands -

        public RelayCommand GenerateDialog { get; set; }


        #endregion

        #region - Private methods -


        private void bindCommands()
        {
            this.GenerateDialog = new RelayCommand(_x => _generateDialogClick());
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

                using (var _serialLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.HexLogFileName, true))
                {
                    _serialLog.WriteLine("");
                    _serialLog.WriteLine("");
                    _serialLog.WriteLine(_versionTimeStr);
                    _serialLog.Close();
                }

                using (var _serialLogDec = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DecimalLogFileName, true))
                {
                    _serialLogDec.WriteLine("");
                    _serialLogDec.WriteLine("");
                    _serialLogDec.WriteLine(_versionTimeStr);
                    _serialLogDec.Close();
                }

                using (var _serialLogDialog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
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
            {

                foreach (var _phrase in _character.Phrases)
                {


                    if (!File.Exists(SessionVariables.AudioDirectory 
                        + _character.CharacterPrefix + "_" 
                        +_phrase.FileName + ".mp3")) //Char name and prefix are being left blank...
                    {

                        var _debugMessage = "missing " + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " + _phrase.DialogStr;

                        //AddDialogItem(_debugMessage);



                        if (SessionVariables.WriteSerialLog)
                            using (var _jsonLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                            {
                                _jsonLog.WriteLine("missing " + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " + _phrase.DialogStr);
                            }
                    }


                }

            }
            //TODO check that all dialog models have unique names
        }

        private ObservableCollection<string> _loadAllDialogModels(string _path)
        {
            ObservableCollection<string> _dialogModels=new ObservableCollection<string>();

            DirectoryInfo _directoryInfo = new DirectoryInfo(_path);

            foreach (var _file in _directoryInfo.GetFiles("*.json"))
            {
                _dialogModels.Add(System.IO.Path.GetFileNameWithoutExtension(_file.FullName));
            }

            return _dialogModels;
        }


        private ObservableCollection<CharacterInfo> _loadAllCharacterNames(string _path)
        {
            DirectoryInfo _directoryInfo = new DirectoryInfo(_path);

            ObservableCollection<CharacterInfo> _charactersInfo=new ObservableCollection<CharacterInfo>();

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

                            var _deserializedCharacterJson = JsonConvert.DeserializeObject<Models.Dialog.CharacterInfo>(_inChar);

                            _deserializedCharacterJson.FileName = _file.Name;

                            _charactersInfo.Add(_deserializedCharacterJson);
                        }
                        catch (Exception ex)
                        {

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
                {
                    using (var _jsonLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                    {
                        _jsonLog.WriteLine(" " + _dialogTracker.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name);
                    }
                }


            }

            //test that all character tags are used by a dialog model.
            AddDialogItem("Check characters tags are used ");

            var _usedFlag = false;

            foreach (var _character in _dialogTracker.CharacterList)
            {

                foreach (var _phrase in _character.Phrases)
                {

                    foreach (var _phrasetag in _phrase.PhraseWeights.Keys)
                    {

                        _usedFlag = false;


                        foreach (var _dialog in _dialogTracker.ModelDialogs)
                        {

                            foreach (var _dialogtag in _dialog.PhraseTypeSequence)
                            {

                                if (_phrasetag == _dialogtag)
                                {
                                    _usedFlag = true;
                                    break;
                                }

                            }



                            if (_usedFlag)
                                break;
                        }


                        if (!_usedFlag)
                        {
                            AddDialogItem(" " + _phrasetag + " is not used.");


                            if (SessionVariables.WriteSerialLog)
                            {
                                using (var _jsonLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName,
                                    true))
                                {
                                    _jsonLog.WriteLine(" " + _phrasetag + " is not used.");
                                }
                            }


                        }
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
                            {
                                if (_dialogtag == _phraseTag)
                                {
                                    _usedFlag = true;
                                    break;
                                }

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
                        {
                            using (var _jsonLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName,true))
                            {
                                _jsonLog.WriteLine(" " + _dialogtag + " not used in " + _dialog.Name);
                            }
                        }

                    }
                }
        }

        private void _initDialogData()
        {
            BackgroundWorker _workerLoader = new BackgroundWorker();

            _workerLoader.DoWork += (_sender, _e) =>
            {


                writeStartupInfo();

                InitModelDialogs.SetDefaults(TheDialogs);

                TheDialogs.IntakeCharacters();


                if (SessionVariables.TagUsageCheck)
                {
                    checkTagsUsed(TheDialogs);
                }



                if (SessionVariables.DebugFlag)
                {
                    checkForMissingPhrases();
                }

            };

            _workerLoader.RunWorkerAsync();
        }

        public void StartDialog()
        {

            BackgroundWorker _worker = new BackgroundWorker();

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

                        int[] _modelAndCharacters = new int[3];

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
                        {
                            FirmwareDebuggingTools.PrintHeatMap();
                        }


                        if (SessionVariables.HeatMapSumsMode)
                        {
                            FirmwareDebuggingTools.PrintHeatMapSums();
                        }


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

        #region - Public methods -


        public void AddDialogItem(Object _entry)
        {

                if (Application.Current.Dispatcher.CheckAccess())
                {
                    try
                    {
                        DialogLinesCollection.Add(_entry);

                        OnPropertyChanged("DialogLinesCollection");

                        var scrollViewer = (VisualTreeHelper.GetChild(mView.textOutput, 0) as ScrollViewer);

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

                            var scrollViewer = (VisualTreeHelper.GetChild(mView.textOutput, 0) as ScrollViewer);

                            scrollViewer.ScrollToBottom();
                        }
                        catch (Exception _e)
                        {
                            mcLogger.Error(_e.Message);
                            
                        }
 

                    }));
                }
            }

        #endregion
    }
}


