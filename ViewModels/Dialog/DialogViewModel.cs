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

namespace DialogEngine.ViewModels.Dialog
{
    /// <summary>
    /// Implementation of <see cref="ViewModelBase"/>
    /// DataContext of Dialog.xaml/>
    /// </summary>
    public class DialogViewModel : ViewModelBase
    {
        #region - Fieds -

        #region -Private fields-

        private static DialogTracker mcTheDialogs;

        private Views.Dialog.Dialog mView;
        private Random mRandom = new Random();
        private string mKeyboardInput;
        private ObservableCollection<string> mDialogCollection;

        #endregion

        #region - Public fields -


        #endregion

        #endregion



        #region - Constructor -

        public DialogViewModel(Views.Dialog.Dialog view)
        {
            mView = view;

            bindCommands();

            //OnViewModelLoaded();
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
            }

        }

        #endregion



        #region - Commands -


        public RelayCommand InputButtonClick { get; set; }


        #endregion



        #region - Private methods -


        private void bindCommands()
        {
            this.InputButtonClick = new RelayCommand(x => onInputButtonClick());
        }



        private void onInputButtonClick()
        {
            string enteredText = mView.messageTextBox.Text;

            if (!string.IsNullOrEmpty(enteredText))
            {
                AddDialogItem(enteredText);

                mView.messageTextBox.Text = string.Empty;

            }

        }


        private void writeStartupInfo()
        {
            if (SessionVars.WriteSerialLog)
            {
                var versionTimeStr = "Dialog Engine ver 0.67 " + DateTime.Now;

                AddDialogItem(versionTimeStr);

                using (var serialLog = new StreamWriter(
                    SessionVars.LogsDirectory + SessionVars.HexLogFileName, true))
                {
                    serialLog.WriteLine("");
                    serialLog.WriteLine("");
                    serialLog.WriteLine(versionTimeStr);
                    serialLog.Close();
                }
                using (var serialLogDec = new StreamWriter(
                    SessionVars.LogsDirectory + SessionVars.DecimalLogFileName, true))
                {
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine(versionTimeStr);
                    serialLogDec.Close();
                }
                using (var serialLogDialog = new StreamWriter(
                    SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                {
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine(versionTimeStr);
                    serialLogDialog.Close();
                }
            }
        }


        private void checkForMissingPhrases()
        {
            if (!SessionVars.AudioDialogsOn)
            {
                return;
            }

            foreach (var character in mcTheDialogs.CharacterList)
            {
                foreach (var phrase in character.Phrases)
                {
                    if (!File.Exists(SessionVars.AudioDirectory + character.CharacterPrefix + "_" + phrase.FileName + ".mp3")) //Char name and prefix are being left blank...
                    {
                        string debugMessage = "missing " + character.CharacterPrefix + "_" + phrase.FileName + ".mp3 " + phrase.DialogStr;

                        AddDialogItem(debugMessage);

                        if (SessionVars.WriteSerialLog)
                        {
                            using (var JSONLog =
                                new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                            {
                                JSONLog.WriteLine("missing " + character.CharacterPrefix + "_" + phrase.FileName + ".mp3 " + phrase.DialogStr);
                            }
                        }

                    }
                }

                AddDialogItem(string.Empty);
            }
            //TODO check that all dialog models have unique names
        }



        private void checkTagsUsed(DialogTracker dialogTracker)
        {

            //spit out all dialog model names and associated number.
            AddDialogItem(string.Empty);

            AddDialogItem("Dialogs Index: ");

            AddDialogItem(string.Empty);

            AddDialogItem(string.Empty);

            foreach (var dialog in mcTheDialogs.ModelDialogs)
            {

                AddDialogItem(" " + dialogTracker.ModelDialogs.IndexOf(dialog) + " : " + dialog.Name);

                if (SessionVars.WriteSerialLog)
                {
                    using (var JSONLog =
                        new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                    {
                        JSONLog.WriteLine(" " + dialogTracker.ModelDialogs.IndexOf(dialog) + " : " + dialog.Name);
                    }
                }

            }

            //test that all character tags are used by a dialog model.
            AddDialogItem("Check characters tags are used ");

            var usedFlag = false;

            foreach (var character in dialogTracker.CharacterList)

                foreach (var phrase in character.Phrases)

                    foreach (var phrasetag in phrase.PhraseWeights.Keys)
                    {
                        usedFlag = false;
                        foreach (var dialog in dialogTracker.ModelDialogs)
                        {
                            foreach (var dialogtag in dialog.PhraseTypeSequence)
                                if (phrasetag == dialogtag)
                                {
                                    usedFlag = true;
                                    break;
                                }
                            if (usedFlag)
                                break;
                        }
                        if (!usedFlag)
                        {
                            AddDialogItem(" " + phrasetag + " is not used.");

                            if (SessionVars.WriteSerialLog)
                            {
                                using (var JSONLog = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName,
                                    true))
                                {
                                    JSONLog.WriteLine(" " + phrasetag + " is not used.");
                                }
                            }

                        }
                    }


            AddDialogItem("Check dialogs tags are used");

            foreach (var dialog in dialogTracker.ModelDialogs)
                foreach (var dialogtag in dialog.PhraseTypeSequence) //each dialog model tag
                {
                    usedFlag = false;
                    foreach (var character in dialogTracker.CharacterList)
                    {
                        foreach (var characterPhrase in character.Phrases)
                        {
                            foreach (var phraseTag in characterPhrase.PhraseWeights.Keys) //each character phrase tag
                                if (dialogtag == phraseTag)
                                {
                                    usedFlag = true;
                                    break;
                                }
                            if (usedFlag)
                                break;
                        }
                        if (usedFlag)
                            break;
                    }
                    if (!usedFlag)
                    {

                        AddDialogItem(" " + dialogtag + " not used in " + dialog.Name);

                        if (SessionVars.WriteSerialLog)
                        {
                            using (var JSONLog = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName,
                                true))
                            {
                                JSONLog.WriteLine(" " + dialogtag + " not used in " + dialog.Name);
                            }
                        }

                    }
                }
        }

        private void onViewModelLoaded()
        {
            DialogTracker dialogTracker = DialogTracker.Instance;

            writeStartupInfo();

            dialogTracker.IntakeCharacters();

            InitModelDialogs.SetDefaults(DialogTracker.Instance);

            if (SessionVars.TagUsageCheck)
            {
                checkTagsUsed(dialogTracker);
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

                string[] parsekeyboardInput = mKeyboardInput.Split();

                //vb : for testing what is the parsed output
                AddDialogItem(parsekeyboardInput[0] + Environment.NewLine);

                AddDialogItem(parsekeyboardInput[1] + Environment.NewLine);

                AddDialogItem(parsekeyboardInput[2] + Environment.NewLine);

                //if keyboard input has three numbers for debug mode to force dialog model and characters

            }

            BackgroundWorker worker = new BackgroundWorker();

            worker.WorkerReportsProgress = false;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync(400);

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {

                if (SessionVars.ForceCharactersAndDialogModel)
                {
                    //string[] keyboardInput = Console.ReadLine().Split(' ');
                    string[] parsekeyboardInput = mKeyboardInput.Split(); //vb:can add a hard code a console.readline()

                    //if keyboard input has three numbers for debug mode to force dialog model and characters
                    if (parsekeyboardInput.Length == 3)
                    {
                        int j = 0;

                        int[] modelAndCharacters = new int[3];

                        foreach (string asciiInt in parsekeyboardInput)
                        {
                            modelAndCharacters[j] = Int32.Parse(asciiInt);
                            j++;
                        }


                        mcTheDialogs.GenerateADialog(modelAndCharacters);

                    }
                    else
                    {

                        AddDialogItem("Incorrect input, generating random dialog.");

                        mcTheDialogs.GenerateADialog();
                    }
                }
                else
                {
                    if (!SessionVars.HeatMapOnlyMode)
                    {
                        mcTheDialogs.GenerateADialog(); //normal operation

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


        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("While loop completed");
        }


        #endregion

        #region - Public methods -

        public void AddDialogItem(string entry)
        {
            DialogCollection.Add(entry);

            OnPropertyChanged("DialogCollection");
        }

        #endregion
    }
}


