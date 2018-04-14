//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Controls.ViewModels;
using DialogEngine.Controls.VoiceRecorder;
using DialogEngine.Core;
using DialogEngine.Dialogs;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Shared;
using DialogEngine.Models.Wizard;
using DialogEngine.ViewModels.WizardWorkflow;
using log4net;
using MaterialDesignThemes.Wpf;
using Stateless.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DialogEngine.ViewModels
{
    public class WizardViewModel : ViewModelBase
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string mCurrentLineName = "USR_CurrentLine";
        public  StateMachine mStateMachine;
        private States mCurrentState;
        private MediaPlayerControlViewModel mMediaPlayerControlViewModel;
        private VoiceRecorderControlViewModel mVoiceRecorderControlViewModel;
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


        public WizardViewModel()
        {
            StateMachine = new StateMachine
            (
                action: () => _view_Loaded()
            );

            StateMachine.PropertyChanged += _stateMachine_PropertyChanged;

            MediaPlayerControlViewModel = new MediaPlayerControlViewModel();
            VoiceRecorderControlViewModel = new VoiceRecorderControlViewModel(NAudioEngine.Instance);

            _configureStateMachine();
            _bindCommands();

            Console.Write(UmlDotGraph.Format(StateMachine.GetInfo()));
        }


        public WizardViewModel(Character character)
        {
            this.Character = character;

            StateMachine = new StateMachine
            (
                action: () => _view_Loaded()
            );
            MediaPlayerControlViewModel = new MediaPlayerControlViewModel();
            VoiceRecorderControlViewModel = new VoiceRecorderControlViewModel(NAudioEngine.Instance);

            _configureStateMachine();
            _bindCommands();

            Console.Write(UmlDotGraph.Format(StateMachine.GetInfo()));
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
                    if (mVoiceRecorderControlViewModel.IsPlaying)
                    {
                        StateMachine.Fire(Triggers.VoiceRecorderPlaying);
                    }
                    else
                    {
                        StateMachine.Fire(Triggers.ReadyForUserAction);
                    }
                    break;
                case "IsRecording":
                    if (mVoiceRecorderControlViewModel.IsRecording)
                    {
                        StateMachine.Fire(Triggers.VoiceRecorderRecording);
                    }
                    else
                    {
                        StateMachine.Fire(Triggers.ReadyForUserAction);
                    }
                    break;
                case "IsPlayingLineInContext":
                    if(mVoiceRecorderControlViewModel.IsPlayingLineInContext)
                    {
                        StateMachine.Fire(Triggers.VoiceRecorderPlayingInContext);
                    }
                    else
                    {
                        StateMachine.Fire(Triggers.ReadyForUserAction);
                    }
                    break;
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

        private void _configureStateMachine()
        {
            StateMachine.Configure(States.Start)
                .OnEntry(_registerListeners)
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
                .SubstateOf(States.VoiceRecorderAction)
                .Permit(Triggers.ReadyForUserAction,States.ReadyForUserAction);

            StateMachine.Configure(States.VoiceRecorderPlaying)
                .SubstateOf(States.VoiceRecorderAction)
                .Permit(Triggers.ReadyForUserAction, States.ReadyForUserAction);

            StateMachine.Configure(States.VoiceRecorderPlayingInContext)
                .OnEntry(t => _playDialogLineInContext())
                .SubstateOf(States.VoiceRecorderAction)
                .Permit(Triggers.ReadyForUserAction, States.ReadyForUserAction);

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
                .SubstateOf(States.VideoPlayerAction)
                .Permit(Triggers.ReadyForUserAction, States.ReadyForUserAction);
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
            PlayInContext = new RelayCommand(x => { StateMachine.Fire(Triggers.VoiceRecorderPlayingInContext); });
            Cancel = new RelayCommand(x => { StateMachine.Fire(Triggers.Cancel); });
        }


        private void _skipStep()
        {
            try
            {
                DialogStr = "";
                mVoiceRecorderControlViewModel.ResetData();

                ++CurrentStepIndex;
                CurrentTutorialStep = mCurrentWizard.TutorialSteps[mCurrentStepIndex];
                CurrentVideoFilePath = Path.Combine(SessionHelper.WizardVideoDirectory, CurrentTutorialStep.VideoFileName + ".avi");
                VoiceRecorderControlViewModel.CurrentFilePath = _tutorialStepFilePath();

                StateMachine.Fire(Triggers.ReadyForUserAction);
            }
            catch (Exception ex)
            {
                mcLogger.Error("_skipStep" + ex.Message);
            }
        }


        private string _tutorialStepFilePath()
        {
            string _tagName = CurrentTutorialStep.PhraseWeights.Count > 0 ? CurrentTutorialStep.PhraseWeights.Keys.ElementAt(0) : "";

            if (string.IsNullOrEmpty(_tagName))
            {
                return "";
            }
            else
            {
                string _mp3FilePath = Character.CharacterPrefix + "_" + _tagName;

                return Path.Combine(SessionHelper.WizardAudioDirectory, _mp3FilePath + ".mp3");
            }
        }


        private async void _cancel()
        {
            try
            {
                var result = await DialogHost.Show(new YesNoDialog("Cancel wizard", "Do you want to save changes?"), "WizardPageDialogHost");

                if (result != null)
                    DialogData.Instance.CharacterCollection.Add(Character);

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

        private void _goBackToDialog()
        {
            EventAggregator.Instance.GetEvent<GoBackEvent>().Publish();
            Reset();
        }


        private async void _playDialogLineInContext()
        {
            string _tutorialStepVideoFilePathCache = mCurrentVideoFilePath;

            await Task.Run(async() =>
            {

                if (IsPlayingLineInContext)
                {
                    mCancellationTokenSource.Cancel();

                    if (mVoiceRecorderControlViewModel.IsPlaying)
                    {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            mVoiceRecorderControlViewModel.PlayOrStop(mVoiceRecorderControlViewModel.CurrentFilePath);
                        }));
                    }
                    else if (mMediaPlayerControlViewModel.IsPlaying)
                    {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            mMediaPlayerControlViewModel.StopMediaPlayer();
                        }));
                    }
                
                    IsPlayingLineInContext = false;
                }
                else
                {
                    try
                    {
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
                                    if(mVoiceRecorderControlViewModel.IsPlaying || mMediaPlayerControlViewModel.IsPlaying)
                                      await  Task.Delay(500);   //task.delay will only logical blocks thread instead of thread.sleep which blocks thread
                                }
                                while (mVoiceRecorderControlViewModel.IsPlaying || mMediaPlayerControlViewModel.IsPlaying);

                                Thread.Sleep(500);
                            }


                            if(index < _dialogLength-1)
                                Thread.Sleep(500);

                            index++;
                        }

                        IsPlayingLineInContext = false;
                    }
                    catch (OperationCanceledException)
                    {
                        IsPlayingLineInContext = false;
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
            CharacterFormDialog dialog;

            if (mCharacter == null)
                dialog = new CharacterFormDialog();
            else
                dialog = new CharacterFormDialog(mCharacter);

            var result = await DialogHost.Show(dialog, "WizardPageDialogHost");

            if (result != null)
            {
                DialogStr = "";
                mVoiceRecorderControlViewModel.ResetData();

                mCharacter = (result as WizardParameter).Character;
                CurrentWizard = DialogData.Instance.WizardTypesCollection[(result as WizardParameter).WizardTypeIndex];
                CurrentTutorialStep = CurrentWizard.TutorialSteps[0];
                CurrentVideoFilePath = Path.Combine(SessionHelper.WizardVideoDirectory, CurrentTutorialStep.VideoFileName + ".avi");

                _registerListeners();
                StateMachine.Fire(Triggers.ReadyForUserAction);
            }
            else
            {
                _clearListeners();
                StateMachine.Fire(Triggers.LeaveWizard);
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
                            .Show(new YesNoDialog("Warning", "You didn't write text for this dialog line. Do you want to save step without it?", "Yes", "No"), "WizardPageDialogHost");

                        if(result == null)
                        {
                            StateMachine.Fire(Triggers.ReadyForUserAction);
                            return;
                        }
                    }

                    PhraseEntry entry = new PhraseEntry
                    {
                        PhraseRating = CurrentTutorialStep.PhraseRating,
                        DialogStr = DialogStr,
                        PhraseWeights = CurrentTutorialStep.PhraseWeights,
                        FileName = mVoiceRecorderControlViewModel.CurrentFilePath
                    };

                    mCharacter.Phrases.Add(entry);
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
            DialogData.Instance.CharacterCollection.Add(mCharacter);

            var result = await DialogHost.
                Show(new YesNoDialog("Success", "Character successfully created!", "Add new character", "Close wizard"), "WizardPageDialogHost");

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
                bool _oldValue = mIsPlayingLineInContext;
                if (_oldValue == value)
                    return;

                mIsPlayingLineInContext = value;
                OnPropertyChanged("IsPlayingLineInContext");

                if (mIsPlayingLineInContext)
                {
                    StateMachine.Fire(Triggers.VoiceRecorderPlayingInContext);
                    _clearListeners();
                }
                else
                {
                    StateMachine.Fire(Triggers.ReadyForUserAction);
                    _registerListeners();
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
