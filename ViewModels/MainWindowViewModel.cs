

using DialogEngine.Core;
using DialogEngine.Dialogs;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Helpers;
using DialogEngine.ViewModels.MainWindowWorkflows;
using DialogEngine.Views;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
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
            _subscribeToEvents();

            StateMachine.Fire(Triggers.NavigateToDialogView);
        }

        #endregion

        #region - commands -

        public RelayCommand GoToDialogView { get; set; }
        public RelayCommand GoToWizardView { get; set; }
        public RelayCommand EditWithJSONEditor { get; set; }
        public RelayCommand OpenSettingsDialog { get; set; }
        public RelayCommand AboutToys2Life { get; set; }
        public RelayCommand ReadTutorial { get; set; }

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
                .Permit(Triggers.NavigateToDialogView,States.DialogView)
                .Permit(Triggers.NavigateToWizardView,States.WizardView);

            StateMachine.Configure(States.DialogView)
                .OnEntry(t => _goToDialogView())
                .Permit(Triggers.NavigateToWizardView, States.WizardView);

            StateMachine.Configure(States.WizardView)
                .OnEntry(t => _goToWizardView())
                .Permit(Triggers.NavigateToDialogView, States.DialogView);
        }


        private void _bindCommands()
        {
            GoToDialogView = new RelayCommand(x => { StateMachine.Fire(Triggers.NavigateToDialogView); });
            GoToWizardView = new RelayCommand(x => { StateMachine.Fire(Triggers.NavigateToWizardView); });
            ReadTutorial = new RelayCommand(x => _readTutorial());
            AboutToys2Life = new RelayCommand(x => _aboutToys2Life());
            OpenSettingsDialog = new RelayCommand(x => _openSettingsDialog());
            EditWithJSONEditor = new RelayCommand(x => _editWithJSONEditor());
        }

        private void _subscribeToEvents()
        {
            EventAggregator.Instance.GetEvent<GoBackEvent>().Subscribe(() => { StateMachine.Fire(Triggers.NavigateToDialogView); });
        }

        private void _goToWizardView()
        {
            NavigationService service = (Application.Current.MainWindow as MainWindow).mainFrame.NavigationService;

            service.Navigate(new WizardView());
        }


        private void _goToDialogView()
        {
            NavigationService service = (Application.Current.MainWindow as MainWindow).mainFrame.NavigationService;

            if (service.CanGoBack)
                service.GoBack();
            else
                service.Navigate(new DialogView(DateTime.Now));
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
