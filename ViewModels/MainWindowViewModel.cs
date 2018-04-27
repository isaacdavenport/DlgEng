﻿

using DialogEngine.Core;
using DialogEngine.Dialogs;
using DialogEngine.Helpers;
using DialogEngine.Workflows.MainWindowWorkflows;
using log4net;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Diagnostics;
using GalaSoft.MvvmLight.CommandWpf;
using System.IO;
using System.Reflection;
using System;
using System.Windows.Navigation;

namespace DialogEngine.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region - fields -

        // Default application logger
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string mcJsonEditorExeName = "JSONedit.exe";
        private readonly string mcJsonBkpFileName = "StarterCharacterWizard_Bkp.json";
        private StateMachine mStateMachine;
        private States mCurrentState;

        #endregion

        #region - constructor -

        public MainWindowViewModel()
        {
            StateMachine = new StateMachine
            (
                action: () => { }
            );

            StateMachine.PropertyChanged += _stateMachine_PropertyChanged;

            _configureStateMachine();
            _bindCommands();

            StateMachine.Fire(Triggers.NavigateToDialogView);
        }

        #endregion

        #region - commands -

        public Core.RelayCommand EditWithJSONEditor { get; set; }
        public Core.RelayCommand OpenSettingsDialog { get; set; }
        public Core.RelayCommand AboutToys2Life { get; set; }
        public Core.RelayCommand ReadTutorial { get; set; }
        public RelayCommand<NavigationEventArgs> MainFrameNavigated { get; set; }

        #endregion

        #region - event handlers -

        private void _stateMachine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("State"))
            {
                CurrentState = StateMachine.State;
            }
        }

        #endregion

        #region - private functions -

        private void _configureStateMachine()
        {
            StateMachine.Configure(States.Start)
                .Permit(Triggers.NavigateToDialogView, States.DialogView)
                .Permit(Triggers.NavigateToWizardView, States.WizardView);

            StateMachine.Configure(States.DialogView)
                .Permit(Triggers.NavigateToWizardView, States.WizardView);

            StateMachine.Configure(States.WizardView)
                .Permit(Triggers.NavigateToDialogView, States.DialogView);
        }


        private void _bindCommands()
        {
            ReadTutorial = new Core.RelayCommand(x => _readTutorial());
            AboutToys2Life = new Core.RelayCommand(x => _aboutToys2Life());
            OpenSettingsDialog = new Core.RelayCommand(x => _openSettingsDialog());
            EditWithJSONEditor = new Core.RelayCommand(x => _editWithJSONEditor());
            MainFrameNavigated = new RelayCommand<NavigationEventArgs>(_onMainFrameNavigated);
        }

        private void _onMainFrameNavigated(NavigationEventArgs obj)
        {
            string _typeName = obj.Content.GetType().Name;

            switch (_typeName)
            {
                case "WizardView":
                    {
                        StateMachine.Fire(Triggers.NavigateToWizardView);
                        break;
                    }
                case "DialogView":
                    {
                        StateMachine.Fire(Triggers.NavigateToDialogView);
                        break;
                    }
            }
        }


        private void _readTutorial()
        {
            Process.Start(Path.Combine(SessionHelper.TutorialDirectory,SessionHelper.TutorialFileName));
        }

        private void _aboutToys2Life()
        {
            Process.Start(SessionHelper.Toys2LifeWebsiteUrl);
        }


        private async void _openSettingsDialog()
        {
           await  DialogHost.Show(new SettingsDialog(), "RootDialogHost");
        }


        private void _editWithJSONEditor()
        {
            Process.Start(Path.Combine(SessionHelper.TutorialDirectory, mcJsonEditorExeName), 
                          Path.Combine(SessionHelper.WizardDirectory, mcJsonBkpFileName));

        }

        #endregion

        #region - properties -

        public StateMachine StateMachine
        {
            get { return mStateMachine; }
            set
            {
                mStateMachine = value;
                OnPropertyChanged("StateMachine");
            }
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

        #endregion
    }
}