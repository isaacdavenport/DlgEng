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
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DialogEngine.ViewModels
{
    public class WizardViewModel : ViewModelBase
    {
        #region - fields -

        
        private readonly string mCurrentLineName = "USR_CurrentLine";
        private WizardView mView;
        private Character mCharacter;
        private WizardType mCurrentWizard;
        private TutorialStep mCurrentTutorialStep;
        private string mCurrentVideoFilePath;
        private int mCurrentStepIndex;
        private bool mIsPlayingLineInContext;

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

        #endregion

        #region - private functions -


        private void _bindCommands()
        {
            DialogHostLoaded = new RelayCommand(x => _view_Loaded());
            SaveAndNext = new RelayCommand(x => _nextStep());
            PlayInContext = new RelayCommand(x => _playDialogLineInContext());
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
                    mIsPlayingLineInContext = false;

                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        mView.voiceRecorder.PlayInContextBtn.Content = "Play in context";
                    }));
                }
                else
                {

                    mIsPlayingLineInContext = true;

                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        mView.voiceRecorder.PlayInContextBtn.Content = "Stop";
                    }));

                    List<List<string>> _dialogsList = CurrentTutorialStep.PlayUserRecordedAudioInContext;

                    foreach (List<string> dialog in _dialogsList)
                    {
                        foreach (string _dialogLine in dialog)
                        {
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


                            do
                            {
                                Task.Delay(500);
                            }
                            while (_voiceRecorderVM.IsPlaying || _mediaPlayerVM.IsPlaying);

                            Thread.Sleep(1000);
                        }

                        Task.Delay(1000);
                    }

                    mIsPlayingLineInContext = true;

                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        mView.voiceRecorder.PlayInContextBtn.Content = "Play in context";
                    }));
                }
            });

            CurrentVideoFilePath = _tutorialStepVideoFilePathCache;


        }

        public async  Task checkIsFinished(VoiceRecorderControlViewModel vm1, MediaPlayerControlViewModel vm2)
        {
            await Task.Run(() => {



            });
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


        private void _nextStep()
        {
            if(mCurrentStepIndex < mCurrentWizard.TutorialSteps.Count-1)
            {
                ++CurrentStepIndex;
                CurrentTutorialStep = mCurrentWizard.TutorialSteps[mCurrentStepIndex];
                CurrentVideoFilePath = Path.Combine(SessionVariables.WizardVideoDirectory, CurrentTutorialStep.VideoFileName + ".avi");
            }
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
            get
            {
                return mCurrentVideoFilePath;
            }
            set
            {
                mCurrentVideoFilePath = value;
                OnPropertyChanged("CurrentVideoFilePath");
            }
        }
        #endregion

    }
}
