//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Controls.ViewModels;
using DialogEngine.Controls.VoiceRecorder;
using DialogEngine.Core;
using DialogEngine.Dialogs;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Shared;
using DialogEngine.Workflows.WizardWorkflow;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DialogEngine.Models;

namespace DialogEngine.ViewModels
{
    public class WizardViewModel : ViewModelBase
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // name in .json file for recorded line
        private readonly string mCurrentLineName = "USR_CurrentLine";
        private int mCurrentStepIndex;
        private string mCurrentVideoFilePath;
        private string mDialogStr;
        private bool mIsPlayingLineInContext;
        private bool mIsEditMode;
        private bool mIsPhraseEditable;
        private States mCurrentState;
        private PhraseEntry mCurrentPhrase;
        private StateMachine mStateMachine;
        private MediaPlayerControlViewModel mMediaPlayerControlViewModel;
        private VoiceRecorderControlViewModel mVoiceRecorderControlViewModel;
        private Character mCharacter;
        private Wizard mCurrentWizard;
        private TutorialStep mCurrentTutorialStep;
        private CancellationTokenSource mCancellationTokenSource;

        #endregion

        #region - constructor -

        public WizardViewModel()
        {
            StateMachine = new StateMachine
            (
                action: () => { }
            );

            StateMachine.PropertyChanged += _stateMachine_PropertyChanged;

            MediaPlayerControlViewModel = new MediaPlayerControlViewModel();
            VoiceRecorderControlViewModel = new VoiceRecorderControlViewModel(NAudioEngine.Instance);

            _configureStateMachine();
            _bindCommands();
            _registerListeners();
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

        #region - event handlers -

        private void _stateMachine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("State"))
                CurrentState = StateMachine.State;
        }

        private void _voiceRecorderControlViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsPlaying":
                    {
                        if (mVoiceRecorderControlViewModel.IsPlaying)
                        {
                            if (StateMachine.CanFire(Triggers.VoiceRecorderPlaying))
                                StateMachine.Fire(Triggers.VoiceRecorderPlaying);
                        }
                        else
                        {
                            if (StateMachine.CanFire(Triggers.ReadyForUserAction))
                                StateMachine.Fire(Triggers.ReadyForUserAction);
                        }

                        break;
                    }

                case "IsRecording":
                    {
                        if (mVoiceRecorderControlViewModel.IsRecording)
                        {
                            if(StateMachine.CanFire(Triggers.VoiceRecorderRecording))
                                StateMachine.Fire(Triggers.VoiceRecorderRecording);
                        }
                        else
                        {
                            if(StateMachine.CanFire(Triggers.ReadyForUserAction))
                                StateMachine.Fire(Triggers.ReadyForUserAction);
                        }

                        break;
                    }
            }
        }

        private void _mediaPlayerControlViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsPlaying"))
            {
                if (mMediaPlayerControlViewModel.IsPlaying)
                {
                    StateMachine.Fire(Triggers.VideoPlayerPlaying);
                }
                else
                {
                    StateMachine.Fire(Triggers.ReadyForUserAction);
                }
            }
        }

        #endregion

        #region - private functions -

        #region - helper functions -

        private void _configureStateMachine()
        {
            StateMachine.Configure(States.Start)
                .Permit(Triggers.ShowFormDialog, States.ShowFormDialog);

            StateMachine.Configure(States.ShowFormDialog)
                .OnEntry(t => _view_Loaded())
                .Permit(Triggers.ReadyForUserAction, States.ReadyForUserAction)
                .Permit(Triggers.LeaveWizard, States.LeaveWizard);

            StateMachine.Configure(States.ReadyForUserAction)
                .Permit(Triggers.SaveAndNext, States.SaveAndNext)
                .Permit(Triggers.SkipStep, States.SkipStep)
                .Permit(Triggers.Cancel, States.Cancel)
                .Permit(Triggers.VoiceRecorderPlaying, States.VoiceRecorderPlaying)
                .Permit(Triggers.VoiceRecorderRecording, States.VoiceRecorderRecording)
                .Permit(Triggers.VoiceRecorderPlayingInContext, States.VoiceRecorderPlayingInContext)
                .Permit(Triggers.VideoPlayerPlaying, States.VideoPlayerPlaying);
                
            // State VoiceRecorderAction is added to be able to disable others controls if any of its substates is active
            StateMachine.Configure(States.VoiceRecorderAction)
                .Permit(Triggers.ReadyForUserAction,States.ReadyForUserAction);

            StateMachine.Configure(States.VoiceRecorderRecording)
                .SubstateOf(States.VoiceRecorderAction);

            StateMachine.Configure(States.VoiceRecorderPlaying)
                .SubstateOf(States.VoiceRecorderAction);

            StateMachine.Configure(States.VoiceRecorderPlayingInContext)
                .OnEntry(t => _clearListeners())
                .OnExit(t => _registerListeners())
                .SubstateOf(States.VoiceRecorderAction);

            StateMachine.Configure(States.VideoPlayerAction)
                .Permit(Triggers.ReadyForUserAction, States.ReadyForUserAction);

            StateMachine.Configure(States.SaveAndNext)
                .OnEntry(t => _saveAndNextStep())
                .Permit(Triggers.ReadyForUserAction, States.ReadyForUserAction)
                .Permit(Triggers.SkipStep,States.SkipStep)
                .Permit(Triggers.Finish, States.Finish);

            StateMachine.Configure(States.Finish)
                .OnEntry(t => _finish())
                .Permit(Triggers.LeaveWizard, States.LeaveWizard)
                .Permit(Triggers.ShowFormDialog, States.ShowFormDialog);

            StateMachine.Configure(States.SkipStep)
                .OnEntry(t => _skipStep())
                .Permit(Triggers.ReadyForUserAction,States.ReadyForUserAction);

            StateMachine.Configure(States.Cancel)
                .OnEntry(t => _cancel())
                .Permit(Triggers.LeaveWizard, States.LeaveWizard);

            StateMachine.Configure(States.LeaveWizard)
                .OnEntry(t => _leaveVizard())
                .Permit(Triggers.Start, States.Start);

            StateMachine.Configure(States.VideoPlayerPlaying)
                .SubstateOf(States.VideoPlayerAction);

        }

        private void _clearListeners()
        {
            mMediaPlayerControlViewModel.PropertyChanged -= _mediaPlayerControlViewModel_PropertyChanged;
            mVoiceRecorderControlViewModel.PropertyChanged -= _voiceRecorderControlViewModel_PropertyChanged;
        }

        private void _registerListeners()
        {
            mMediaPlayerControlViewModel.PropertyChanged += _mediaPlayerControlViewModel_PropertyChanged;
            mVoiceRecorderControlViewModel.PropertyChanged += _voiceRecorderControlViewModel_PropertyChanged;
        }

        private void _bindCommands()
        {
            DialogHostLoaded = new RelayCommand(x => { StateMachine.Fire(Triggers.ShowFormDialog); });
            SaveAndNext = new RelayCommand(x => { StateMachine.Fire(Triggers.SaveAndNext); });
            SkipStep = new RelayCommand(x => { StateMachine.Fire(Triggers.SkipStep); });
            PlayInContext = new RelayCommand(x => _playDialogLineInContext());
            Cancel = new RelayCommand(x => { StateMachine.Fire(Triggers.Cancel); });
        }

        private async void _playDialogLineInContext()
        {
            string _tutorialStepVideoFilePathCache = mCurrentVideoFilePath;

            if (IsPlayingLineInContext)
            {
                mCancellationTokenSource.Cancel();

                if (mVoiceRecorderControlViewModel.IsPlaying)
                {
                    mVoiceRecorderControlViewModel.PlayOrStop(mVoiceRecorderControlViewModel.CurrentFilePath);
                }
                else if (mMediaPlayerControlViewModel.IsPlaying)
                {
                    mMediaPlayerControlViewModel.StopMediaPlayer();
                }
            }
            else
            {
                await Task.Run(async () =>
                {
                    try
                    {
                        _clearListeners();

                        IsPlayingLineInContext = true;
                        List<List<string>> _dialogsList = CurrentTutorialStep.PlayUserRecordedAudioInContext;
                        mCancellationTokenSource = new CancellationTokenSource();
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
                                        mVoiceRecorderControlViewModel.PlayOrStop(mVoiceRecorderControlViewModel.CurrentFilePath);
                                    }));
                                }
                                else
                                {
                                    string path = Path.Combine(SessionHelper.WizardVideoDirectory, _dialogLine + ".avi");
                                    CurrentVideoFilePath = path;

                                    Dispatcher.Invoke((Action)(() =>
                                    {
                                        mMediaPlayerControlViewModel.StartMediaPlayer();
                                    }));
                                }

                                mCancellationTokenSource.Token.ThrowIfCancellationRequested();

                                do
                                {
                                    if (mVoiceRecorderControlViewModel.IsPlaying || mMediaPlayerControlViewModel.IsPlaying)
                                        await Task.Delay(500);   //task.delay will only logical blocks thread instead of 
                                                                 //thread.sleep which blocks thread
                                }
                                while (mVoiceRecorderControlViewModel.IsPlaying || mMediaPlayerControlViewModel.IsPlaying);

                                Thread.Sleep(500);
                            }

                            if (index < _dialogLength - 1)
                                Thread.Sleep(500);

                            index++;
                        }
                    }
                    catch (OperationCanceledException)
                    {
                    }
                    catch (Exception ex)
                    {
                        mcLogger.Error("_playDialogLineInContext" + ex.Message);
                    }

                });
            }

            IsPlayingLineInContext = false;
            CurrentVideoFilePath = _tutorialStepVideoFilePathCache;
        }

        private PhraseEntry _findPhraseInCharacterForTutorialStep(TutorialStep _tutorialStep)
        {
            foreach (PhraseEntry phrase in Character.Phrases)
            {
                if (!string.IsNullOrEmpty(phrase.FileName))
                {
                    // file name is formed as BO_Greeting.mp3
                    string _userRecordedFileName = phrase.FileName;
                    // DPWizGiveCredit tag name is right side of 'Wiz'
                    int _indexForSplitting = _tutorialStep.VideoFileName.IndexOf("Wiz");
                    string _tagName = _tutorialStep.VideoFileName.Substring(_indexForSplitting + 3);

                    if (_userRecordedFileName.Equals(_tagName))
                        return phrase;
                }
            }

            return null;
        }

        private string _tutorialStepFilePath()
        {
            int _indexForSplitting = CurrentTutorialStep.VideoFileName.IndexOf("Wiz");
            string _videoFileName = CurrentTutorialStep.VideoFileName.Substring(_indexForSplitting + 3);

            if (string.IsNullOrEmpty(_videoFileName))
            {
                return "";
            }
            else
            {
                string _mp3FilePath = Character.CharacterPrefix + "_" + _videoFileName;

                return _mp3FilePath;
            }
        }


        private void _goBackToDialog()
        {
            NavigationCommands.BrowseHome.Execute(null, Application.Current.MainWindow);
            Reset();
        }


        private void _setDataForTutorialStep(int _currentStepIndex)
        {
            try
            {
                CurrentTutorialStep = CurrentWizard.TutorialSteps[_currentStepIndex];
                CurrentVideoFilePath = Path.Combine(SessionHelper.WizardVideoDirectory, CurrentTutorialStep.VideoFileName + ".avi");

                if (CurrentTutorialStep.CollectUserInput)
                {
                    VoiceRecorderControlViewModel.CurrentFilePath = _tutorialStepFilePath();

                    PhraseEntry _currentPhrase = _findPhraseInCharacterForTutorialStep(CurrentTutorialStep);

                    if (_currentPhrase != null)
                    {
                        DialogStr = _currentPhrase.DialogStr;
                        VoiceRecorderControlViewModel.CurrentFilePath = Character.CharacterPrefix + "_" + _currentPhrase.FileName;
                        VoiceRecorderControlViewModel.IsLineRecorded = true;
                        mIsPhraseEditable = true;
                        mCurrentPhrase = _currentPhrase;
                    }
                    else
                    {
                        mIsPhraseEditable = false;
                        DialogStr = "";
                        mVoiceRecorderControlViewModel.ResetData();
                    }
                }
                else
                {
                    VoiceRecorderControlViewModel.ResetData();
                    DialogStr = "";
                }

            }
            catch (Exception ex)
            {
                mcLogger.Error("_setDataForTutorialStep " + ex.Message);
            }

        }

        #endregion

        #region - state machine functions -

        private async void _cancel()
        {
            try
            {
                StateMachine.Fire(Triggers.LeaveWizard);
            }
            catch (Exception ex)
            {
                mcLogger.Error("_cancel " + ex.Message);
            }
        }


        private void _leaveVizard()
        {
            try
            {
                StateMachine.Fire(Triggers.Start);
                _goBackToDialog();
            }
            catch (Exception ex)
            {
                mcLogger.Error("_leaveVizard" + ex.Message);
            }
        }


        private async void _view_Loaded()
        {
            WizardFormDialog dialog = Character == null? new WizardFormDialog()
                                                       : new WizardFormDialog(Character);

            var result = await DialogHost.Show(dialog, "RootDialogHost");

            if (result != null)
            {
                WizardParameter _wizardParameter = result as WizardParameter;

                Character = _wizardParameter.Character;
                CurrentWizard = DialogData.Instance.WizardsCollection[(result as WizardParameter).WizardTypeIndex];

                _setDataForTutorialStep(CurrentStepIndex);

                StateMachine.Fire(Triggers.ReadyForUserAction);
            }
            else
            {
                StateMachine.Fire(Triggers.LeaveWizard);
            }
        }


        private void _skipStep()
        {
            try
            {
                ++CurrentStepIndex;

                _setDataForTutorialStep(CurrentStepIndex);

                StateMachine.Fire(Triggers.ReadyForUserAction);
            }
            catch (Exception ex)
            {
                mcLogger.Error("_skipStep" + ex.Message);
            }
        }


        private async void _saveAndNextStep()
        {
            if(mCurrentStepIndex < mCurrentWizard.TutorialSteps.Count-1)
            {
                if (mCurrentTutorialStep.CollectUserInput)
                {
                    if (string.IsNullOrEmpty(DialogStr))
                    {
                        var result = await DialogHost
                            .Show(new YesNoDialog("Warning", 
                                                  "You didn't write text for this dialog line. Do you want to save step without it?", "Yes", "No"), 
                                                  "WizardPageDialogHost");

                        if(result == null)
                        {
                            StateMachine.Fire(Triggers.ReadyForUserAction);
                            return;
                        }
                    }

                    if(mIsPhraseEditable)
                    {
                        mCurrentPhrase.DialogStr = DialogStr;
                    }
                    else
                    {
                        string[] mFileNameArray = mVoiceRecorderControlViewModel.CurrentFilePath.Split('_');

                        PhraseEntry entry = new PhraseEntry
                        {
                            PhraseRating = CurrentTutorialStep.PhraseRating,
                            DialogStr = DialogStr,
                            PhraseWeights = CurrentTutorialStep.PhraseWeights,
                            FileName = mFileNameArray[1]
                        };

                        mCharacter.Phrases.Add(entry);
                    }

                    await DialogDataHelper.SerializeCharacterToFile(Character);
                }

                StateMachine.Fire(Triggers.SkipStep);
                return;
            }
            else
            {
                StateMachine.Fire(Triggers.Finish);
            }
        }


        private async void _finish()
        {
            var result = await DialogHost.
                Show(new YesNoDialog("Info",
                                     "Character successfully updated!", 
                                     "Run another wizard", 
                                     "Close wizard"), "WizardPageDialogHost");

            if (result != null)
            {
                Reset();
                StateMachine.Fire(Triggers.ShowFormDialog);
            }
            else
            {
                StateMachine.Fire(Triggers.LeaveWizard);
            }
        }

        #endregion

        #endregion

        #region - public functions -

        public void Reset()
        {
            mCharacter = null;
            CurrentStepIndex = 0;
            DialogStr = "";
        }

        #endregion

        #region - properties -

        public StateMachine StateMachine
        {
            get { return mStateMachine;  }
            set { mStateMachine = value; }
        }


        public States CurrentState
        {
            get { return StateMachine.State; }
            private set
            {
                mCurrentState = value;
                OnPropertyChanged("CurrentState");
            }
        }

        public TutorialStep CurrentTutorialStep
        {
          get { return mCurrentTutorialStep; }
          set
          {
                mCurrentTutorialStep = value;
                OnPropertyChanged("CurrentTutorialStep");
          }
        }


        public Wizard CurrentWizard
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
                bool _oldValue = mIsPlayingLineInContext;
                if (_oldValue == value)
                    return;

                mIsPlayingLineInContext = value;
                OnPropertyChanged("IsPlayingLineInContext");

                if (mIsPlayingLineInContext)
                {
                    StateMachine.Fire(Triggers.VoiceRecorderPlayingInContext);
                }
                else
                {
                    StateMachine.Fire(Triggers.ReadyForUserAction);
                }
            }
        }


        public string DialogStr
        {
            get { return mDialogStr; }
            set
            {
                mDialogStr = value;
                OnPropertyChanged("DialogStr");
            }
        }


        public MediaPlayerControlViewModel MediaPlayerControlViewModel
        {
            get { return mMediaPlayerControlViewModel; }
            set
            {
                mMediaPlayerControlViewModel = value;
                OnPropertyChanged("MediaPlayerControlViewModel");
            }
        }


        public VoiceRecorderControlViewModel VoiceRecorderControlViewModel
        {
            get { return mVoiceRecorderControlViewModel; }
            set
            {
                mVoiceRecorderControlViewModel = value;
                OnPropertyChanged("VoiceRecorderControlViewModel");
            }
        }

        #endregion
    }
}
