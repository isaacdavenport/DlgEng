//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Controls.ViewModels;
using DialogEngine.Core;
using DialogEngine.Dialogs;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Shared;
using DialogEngine.Models.Wizard;
using DialogEngine.Views;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DialogEngine.ViewModels
{
    public class WizardViewModel : ViewModelBase
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string mCurrentLineName = "USR_CurrentLine";
        private WizardView mView;
        private Character mCharacter;
        private WizardType mCurrentWizard;
        private TutorialStep mCurrentTutorialStep;
        private string mCurrentVideoFilePath;
        private int mCurrentStepIndex;
        private bool mIsPlayingLineInContext;
        private CancellationTokenSource mCancellationTokenSource;
        private string mDialogStr;
        #endregion

        #region - constructor -

        public WizardViewModel(WizardView view)
        {
            this.mView = view;

            _bindCommands();
        }

        public WizardViewModel(WizardView view,Character character)
        {
            this.mView = view;
            this.Character = character;

            _bindCommands();
        }

        #endregion

        #region - commands -

        public RelayCommand DialogHostLoaded { get; set; }
        public RelayCommand SaveAndNext { get; set; }
        public RelayCommand SkipStep { get; set; }
        public RelayCommand CreateNewCharacter { get; set; }
        public RelayCommand PlayInContext { get; set; }
        public RelayCommand Cancel { get; set; }

        #endregion

        #region - private functions -


        private void _bindCommands()
        {
            DialogHostLoaded = new RelayCommand(x => _view_Loaded());
            SaveAndNext = new RelayCommand(x => _nextStep());
            SkipStep = new RelayCommand(x => _skipStep());
            PlayInContext = new RelayCommand(x => _playDialogLineInContext());
            Cancel = new RelayCommand(x => _cancel());
        }

        private void _skipStep()
        {
            ++CurrentStepIndex;
            CurrentTutorialStep = mCurrentWizard.TutorialSteps[mCurrentStepIndex];
            CurrentVideoFilePath = Path.Combine(SessionVariables.WizardVideoDirectory, CurrentTutorialStep.VideoFileName + ".avi");
        }

        private async void _cancel()
        {
            var result =await  DialogHost.Show(new YesNoDialog("Cancel wizard","Do you want to save changes?"), "WizardPageDialogHost");

            if(result != null)
            {
                DialogData.Instance.CharacterCollection.Add(Character);
                _goBackToDialog();
            }
            else
            {
                _goBackToDialog();
            }
        }


        private void _goBackToDialog()
        {
            Frame _mainFrame = (Application.Current.MainWindow as MainWindow).mainFrame;

            if (_mainFrame.CanGoBack)
            {
                _mainFrame.GoBack();
                Reset();
            }
        }

        private async void _playDialogLineInContext()
        {

            VoiceRecorderControlViewModel _voiceRecorderVM = mView.voiceRecorder.DataContext as VoiceRecorderControlViewModel;
            MediaPlayerControlViewModel _mediaPlayerVM = mView.mediaPlayer.DataContext as MediaPlayerControlViewModel;
            string _tutorialStepVideoFilePathCache = mCurrentVideoFilePath;

            await Task.Run(() =>
            {

                if (mIsPlayingLineInContext)
                {
                    mCancellationTokenSource.Cancel();
                    mIsPlayingLineInContext = false;

                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        mView.voiceRecorder.PlayInContextBtn.Content = "Play in context";
                    }));
                }
                else
                {
                    try
                    {
                        mCancellationTokenSource = new CancellationTokenSource();
                        IsPlayingLineInContext = true;
                        List<List<string>> _dialogsList = CurrentTutorialStep.PlayUserRecordedAudioInContext;

                        Dispatcher.BeginInvoke((Action)(() =>
                        {
                            mView.voiceRecorder.PlayInContextBtn.Content = "Stop";
                        }));

                        int index = 0;
                        int _dialogLength = _dialogsList.Count;

                        foreach (List<string> dialog in _dialogsList)
                        {
                            mCancellationTokenSource.Token.ThrowIfCancellationRequested();

                            foreach (string _dialogLine in dialog)
                            {
                                mCancellationTokenSource.Token.ThrowIfCancellationRequested();

                                if (_dialogLine.Equals(mCurrentLineName))
                                {
                                    Dispatcher.Invoke((Action)(() =>
                                    {
                                        _voiceRecorderVM.Play(_voiceRecorderVM.CurrentFilePath);
                                    }));
                                }
                                else
                                {
                                    string path = Path.Combine(SessionVariables.WizardVideoDirectory, _dialogLine + ".avi");
                                    CurrentVideoFilePath = path;

                                    Dispatcher.Invoke((Action)(() =>
                                    {
                                        _mediaPlayerVM.StartMediaPlayer();
                                    }));
                                }

                                mCancellationTokenSource.Token.ThrowIfCancellationRequested();

                                do
                                {
                                    if(_voiceRecorderVM.IsPlaying || _mediaPlayerVM.IsPlaying)
                                        Task.Delay(500);   //task.delay will only logical blocks thread instead of thread.sleep which blocks thread
                                }
                                while (_voiceRecorderVM.IsPlaying || _mediaPlayerVM.IsPlaying);

                                Thread.Sleep(500);
                            }


                            if(index < _dialogLength-1)
                                Thread.Sleep(500);

                            index++;
                        }

                        IsPlayingLineInContext = false;
                        Dispatcher.BeginInvoke((Action)(() =>
                        {
                            mView.voiceRecorder.PlayInContextBtn.Content = "Play in context";
                        }));
                    }
                    catch (OperationCanceledException)
                    {
                        IsPlayingLineInContext = false;
                        Dispatcher.BeginInvoke((Action)(() =>
                        {
                            mView.voiceRecorder.PlayInContextBtn.Content = "Play in context";
                        }));
                    }
                    catch(Exception ex)
                    {
                        mcLogger.Error("_playDialogLineInContext" + ex.Message);
                    }
                }
            });

            CurrentVideoFilePath = _tutorialStepVideoFilePathCache;
        }


        private async void _view_Loaded()
        {
            // if we want to add new character
            NewCharacterDialogControl dialog;

            if (mCharacter == null)
                dialog = new NewCharacterDialogControl();
            else
                dialog = new NewCharacterDialogControl(mCharacter);

            var result = await DialogHost.Show(dialog, "WizardPageDialogHost");

            if (result != null)
            {
                mCharacter = (result as WizardParameter).Character;
                CurrentWizard = DialogData.Instance.WizardTypesCollection[(result as WizardParameter).WizardTypeIndex];
                CurrentTutorialStep = CurrentWizard.TutorialSteps[0];
                CurrentVideoFilePath = Path.Combine(SessionVariables.WizardVideoDirectory, CurrentTutorialStep.VideoFileName + ".avi");
            }
        }


        private async void _nextStep()
        {
            if(mCurrentStepIndex < mCurrentWizard.TutorialSteps.Count-1)
            {
                if (mCurrentTutorialStep.CollectUserInput)
                {
                    string _tagName = CurrentTutorialStep.PhraseWeights.Keys.ElementAt(0);
                    string _characterName = mCharacter.CharacterName.Replace(" ", string.Empty);

                    PhraseEntry entry = new PhraseEntry
                    {
                        PhraseRating = CurrentTutorialStep.PhraseRating,
                        DialogStr = DialogStr,
                        PhraseWeights = CurrentTutorialStep.PhraseWeights,
                        FileName = _characterName + "" + _tagName
                    };

                    mCharacter.Phrases.Add(entry);
                }

                ++CurrentStepIndex;
                CurrentTutorialStep = mCurrentWizard.TutorialSteps[mCurrentStepIndex];
                CurrentVideoFilePath = Path.Combine(SessionVariables.WizardVideoDirectory, CurrentTutorialStep.VideoFileName + ".avi");
            }
            else
            {
                DialogData.Instance.CharacterCollection.Add(mCharacter);

                var result = await DialogHost.
                    Show(new YesNoDialog("Success", "Character successfully created!","Add new character", "Close wizard"), "WizardPageDialogHost");

                if(result != null)
                {
                    Reset();
                    _view_Loaded();
                }
                else
                {
                    _goBackToDialog();
                }
            }
        }


        #endregion

        #region

        public void Reset()
        {
            mCharacter = null;
            CurrentStepIndex = 0;
        }

        #endregion

        #region - properties -

        public TutorialStep CurrentTutorialStep
        {
          get { return mCurrentTutorialStep; }
          set
          {
                mCurrentTutorialStep = value;
                OnPropertyChanged("CurrentTutorialStep");
          }
        }

        public WizardType CurrentWizard
        {
            get { return mCurrentWizard; }
            set
            {
                mCurrentWizard = value;
                OnPropertyChanged("CurrentWizard");
            }
        }


        public int CurrentStepIndex
        {
            get { return mCurrentStepIndex; }
            set
            {
                mCurrentStepIndex = value;
                OnPropertyChanged("CurrentStepIndex");
            }
        }


        public Character Character
        {
            get { return mCharacter; }
            set
            {
                mCharacter = value;
                OnPropertyChanged("Character");
            }
        }

        public string CurrentVideoFilePath
        {
            get { return mCurrentVideoFilePath; }
            set
            {
                mCurrentVideoFilePath = value;
                OnPropertyChanged("CurrentVideoFilePath");
            }
        }

        public bool IsPlayingLineInContext
        {
            get { return mIsPlayingLineInContext; }
            set
            {
                mIsPlayingLineInContext = value;
                OnPropertyChanged("IsPlayingLineInContext");
            }
        }


        public string DialogStr
        {
            get
            {
                return mDialogStr;
            }
            set
            {
                mDialogStr = value;
                OnPropertyChanged("DialogStr");
            }
        }

        #endregion

    }
}
