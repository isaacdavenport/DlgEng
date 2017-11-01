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

        private static DialogTracker msTheDialogs;
        private Views.Dialog.Dialog mView;
        private Random mRandom = new Random();
        private string mKeyboardInput;
        private ObservableCollection<string> mDialogCollection;

        #endregion


        #region - Public fields -

        #endregion

        #endregion


        #region - Constructor -

        public DialogViewModel(Views.Dialog.Dialog _view)
        {
            mView = _view;

            bindCommands();

        }

        #endregion


        #region - Properties -
        /// <summary>
        /// Dynamic collection of objects, objects can be added,removed or updated, and UI is automatically updated 
        /// </summary>
        public ObservableCollection<string> DialogCollection
        {
            get
            {
                if (mDialogCollection == null)
                {
                    mDialogCollection = new ObservableCollection<string>();
                }
                return mDialogCollection;
            }

            set
            {
                mDialogCollection = value;

                // send notification to view (model is changed)
                OnPropertyChanged("DialogCollection");

                
                (VisualTreeHelper.GetChild(mView.textOutput,0) as ScrollViewer)?.ScrollToBottom();
            }

        }


        public DialogTracker TheDialogs
        {
            set
            {
                msTheDialogs = value;
            }
        }

        #endregion


        #region - Commands -

        public RelayCommand InputButtonClick { get; set; }


        #endregion


        #region - Private methods -


        private void bindCommands()
        {
            this.InputButtonClick = new RelayCommand(_x => onInputButtonClick());
        }



        private void onInputButtonClick()
        {
            string _enteredText = mView.messageTextBox.Text;

            if (!string.IsNullOrEmpty(_enteredText))
            {
                AddDialogItem(_enteredText);

                mView.messageTextBox.Text = string.Empty;

            }

        }


        private void writeStartupInfo()
        {
            if (SessionVars.WriteSerialLog)
            {
                var _versionTimeStr = "Dialog Engine ver 0.67 " + DateTime.Now;

                AddDialogItem(_versionTimeStr);

                using (var _serialLog = new StreamWriter(SessionVars.LogsDirectory + SessionVars.HexLogFileName, true))
                {
                    _serialLog.WriteLine("");
                    _serialLog.WriteLine("");
                    _serialLog.WriteLine(_versionTimeStr);
                    _serialLog.Close();
                }

                using (var _serialLogDec = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DecimalLogFileName, true))
                {
                    _serialLogDec.WriteLine("");
                    _serialLogDec.WriteLine("");
                    _serialLogDec.WriteLine(_versionTimeStr);
                    _serialLogDec.Close();
                }

                using (var _serialLogDialog = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
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
            if (!SessionVars.AudioDialogsOn)
                return;


            foreach (var _character in msTheDialogs.CharacterList)
            {

                foreach (var _phrase in _character.Phrases)
                {


                    if (!File.Exists(SessionVars.AudioDirectory 
                        + _character.CharacterPrefix + "_" 
                        +_phrase.FileName + ".mp3")) //Char name and prefix are being left blank...
                    {

                        var _debugMessage = "missing " + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " + _phrase.DialogStr;

                        AddDialogItem(_debugMessage);



                        if (SessionVars.WriteSerialLog)
                            using (var _jsonLog = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                            {
                                _jsonLog.WriteLine("missing " + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " + _phrase.DialogStr);
                            }
                    }


                }


                AddDialogItem(string.Empty);
            }
            //TODO check that all dialog models have unique names
        }



        private void checkTagsUsed(DialogTracker _dialogTracker)
        {

            //spit out all dialog model names and associated number.
            AddDialogItem(string.Empty);

            AddDialogItem("Dialogs Index: ");

            AddDialogItem(string.Empty);

            AddDialogItem(string.Empty);


            foreach (var _dialog in msTheDialogs.ModelDialogs)
            {

                AddDialogItem(" " + _dialogTracker.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name);


                if (SessionVars.WriteSerialLog)
                {
                    using (var _jsonLog = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
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


                            if (SessionVars.WriteSerialLog)
                            {
                                using (var _jsonLog = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName,
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


                        if (SessionVars.WriteSerialLog)
                        {
                            using (var _jsonLog = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName,true))
                            {
                                _jsonLog.WriteLine(" " + _dialogtag + " not used in " + _dialog.Name);
                            }
                        }

                    }
                }
        }



        public void OnViewModelLoaded()
        {
            BackgroundWorker _workerLoader = new BackgroundWorker();

            _workerLoader.DoWork += (_sender, _e) =>
            {

                DialogTracker _dialogTracker = DialogTracker.Instance;

                writeStartupInfo();

                _dialogTracker.IntakeCharacters();

                InitModelDialogs.SetDefaults(DialogTracker.Instance);



                if (SessionVars.TagUsageCheck)
                {
                    checkTagsUsed(_dialogTracker);
                }



                if (SessionVars.DebugFlag)
                {
                    checkForMissingPhrases();

                    AddDialogItem("  press enter to continue");


                    if (!SessionVars.ForceCharactersAndDialogModel)
                    {
                        AddDialogItem("   you may enter two characters initials to make them talk");
                    }

                }


                //Select Debug Output
                if (SessionVars.ForceCharactersAndDialogModel)
                {
                    AddDialogItem("   enter three numbers to set the next: DialogModel, Char1, Char2");

                    AddDialogItem(string.Empty);
                }


                //vb: following part taken out of while(true) to check in input works and can be parsed

                if (SessionVars.ForceCharactersAndDialogModel)
                {
                    //string[] keyboardInput = Console.ReadLine().Split(' ');

                    //  vb: take user input fromtyext box instead of console.readline above

                    string[] _parsekeyboardInput = mKeyboardInput.Split();

                    //vb : for testing what is the parsed output
                    AddDialogItem(_parsekeyboardInput[0] + Environment.NewLine);

                    AddDialogItem(_parsekeyboardInput[1] + Environment.NewLine);

                    AddDialogItem(_parsekeyboardInput[2] + Environment.NewLine);

                    //if keyboard input has three numbers for debug mode to force dialog model and characters

                }

            };


            _workerLoader.RunWorkerAsync();


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

                if (SessionVars.ForceCharactersAndDialogModel)
                {
                    //string[] keyboardInput = Console.ReadLine().Split(' ');
                    string[] _parsekeyboardInput = mKeyboardInput.Split(); //vb:can add a hard code a console.readline()

                    //if keyboard input has three numbers for debug mode to force dialog model and characters
                    if (_parsekeyboardInput.Length == 3)
                    {
                        int _j = 0;

                        int[] _modelAndCharacters = new int[3];

                        foreach (string _asciiInt in _parsekeyboardInput)
                        {
                            _modelAndCharacters[_j] = Int32.Parse(_asciiInt);
                            _j++;
                        }


                        msTheDialogs.GenerateADialog(_modelAndCharacters);

                    }
                    else
                    {

                        AddDialogItem("Incorrect input, generating random dialog.");

                        msTheDialogs.GenerateADialog();
                    }
                }
                else
                {
                    if (!SessionVars.HeatMapOnlyMode)
                    {
                        msTheDialogs.GenerateADialog(); //normal operation

                        Thread.Sleep(1100); //vb:commented out for debugging as code stops here

                        Thread.Sleep(mRandom.Next(0, 2000)); //vb:commented out for debugging as code stops here
                    }
                    else
                    {

                        if (SessionVars.HeatMapFullMatrixDispMode)
                        {
                            FirmwareDebuggingTools.PrintHeatMap();
                        }


                        if (SessionVars.HeatMapSumsMode)
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
            MessageBoxResult _result = MessageBox.Show("While loop completed");
        }


        #endregion

        #region - Public methods -

        public void AddDialogItem(string _entry)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                DialogCollection.Add(_entry);

                OnPropertyChanged("DialogCollection");
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke((Action) (() =>
                {

                    DialogCollection.Add(_entry);

                    OnPropertyChanged("DialogCollection");

                }));
            }


        }

        #endregion
    }
}


