//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Controls.ViewModels;
using DialogEngine.Controls.VoiceRecorder;
using DialogEngine.Core;
using DialogEngine.Dialogs;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Shared;
using DialogEngine.Models.Wizard;
using DialogEngine.ViewModels.Workflows;
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

        public StateMachine mStateMachine;
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
            MediaPlayerControlViewModel = new MediaPlayerControlViewModel();
            VoiceRecorderControlViewModel = new VoiceRecorderControlViewModel(NAudioEngine.Instance);
            IsPlayingLineInContext = false;

            _configureStateMachine();
            _bindCommands();
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
            IsPlayingLineInContext = false;

            _configureStateMachine();
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

        #region - event handlers -

        private void _voiceRecorderControlViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsPlaying"))
            {
                if (mVoiceRecorderControlViewModel.IsPlaying)
                {
                    StateMachine.Fire(Triggers.VoiceRecorderAction);
                }
                else
                {
                    StateMachine.Fire(Triggers.ReadyForUserAction);
                }
            }
        }

        private void _mediaPlayerControlViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName.Equals("IsPlaying"))
            {
                if (mMediaPlayerControlViewModel.IsPlaying)
                {
                    StateMachine.Fire(Triggers.VideoPlayerAction);
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
                .Permit(Triggers.ShowFormDialog, States.ShowFormDialog);

            StateMachine.Configure(States.ShowFormDialog)
                .OnEntry(t => _view_Loaded())
                .Permit(Triggers.Back, States.Back)
                .Permit(Triggers.ReadyForUserAction, States.ReadyForUserAction);

            StateMachine.Configure(States.ReadyForUserAction)
                .OnEntry(t => _registerListeners())
                .OnExit(t => _clearListeners())
                .Permit(Triggers.SaveAndNext, States.SaveAndNext)
                .Permit(Triggers.SkipStep, States.SkipStep)
                .Permit(Triggers.Cancel, States.Cancel)
                .Permit(Triggers.VoiceRecorderAction, States.VoiceRecorderAction)
                .Permit(Triggers.VideoPlayerAction, States.VideoPlayerAction);

            StateMachine.Configure(States.VoiceRecorderAction)
                .Permit(Triggers.ReadyForUserAction,States.ReadyForUserAction);

            StateMachine.Configure(States.VideoPlayerAction)
                .Permit(Triggers.ReadyForUserAction, States.ReadyForUserAction);

            StateMachine.Configure(States.SaveAndNext)
                .OnEntry(t => _nextStep())
                .Permit(Triggers.ReadyForUserAction, States.ReadyForUserAction);

            StateMachine.Configure(States.SkipStep)
                .OnEntry(t => _skipStep())
                .Permit(Triggers.ReadyForUserAction,States.ReadyForUserAction);

            StateMachine.Configure(States.Back)
                .OnEntry(t => _goBackToDialog())
                .OnExit(t => { StateMachine.Fire(Triggers.Start); })
                .Permit(Triggers.Start, States.Start);
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
            string _tutorialStepVideoFilePathCache = mCurrentVideoFilePath;

            await Task.Run(async() =>
            {

                if (IsPlayingLineInContext)
                {
                    mCancellationTokenSource.Cancel();
                    IsPlayingLineInContext = false;

                }
                else
                {
                    try
                    {
                        List<List<string>> _dialogsList = CurrentTutorialStep.PlayUserRecordedAudioInContext;
                        mCancellationTokenSource = new CancellationTokenSource();
                        IsPlayingLineInContext = true;
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
                                    string path = Path.Combine(SessionVariables.WizardVideoDirectory, _dialogLine + ".avi");
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
                mCharacter = (result as WizardParameter).Character;
                CurrentWizard = DialogData.Instance.WizardTypesCollection[(result as WizardParameter).WizardTypeIndex];
                CurrentTutorialStep = CurrentWizard.TutorialSteps[0];
                CurrentVideoFilePath = Path.Combine(SessionVariables.WizardVideoDirectory, CurrentTutorialStep.VideoFileName + ".avi");

                StateMachine.Fire(Triggers.ReadyForUserAction);
            }
            else
            {
                StateMachine.Fire(Triggers.Back);
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

        #region - public functions -


        public void Reset()
        {
            mCharacter = null;
            CurrentStepIndex = 0;
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
            get { return mCurrentState; }
            set
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
                mIsPlayingLineInContext = value;
                OnPropertyChanged("IsPlayingLineInContext");
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
