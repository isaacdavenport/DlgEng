

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace DialogEngine.Workflows.MainWindowWorkflows
{
    public enum States
    {
        Start,
        DialogView,
        WizardView
    }

    public enum NavWindowTriggers
    {
        Start,
        NavigateToDialogView,
        NavigateToWizardView
    }

    public class StateMachine : Stateless.StateMachine<States, NavWindowTriggers>, INotifyPropertyChanged
    {
        #region - fields -

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - constructor -

        public StateMachine(Action action) : base(States.Start)
        {
            OnTransitioned
            (
                (t) =>
                {
                    OnPropertyChanged("State");
                    CommandManager.InvalidateRequerySuggested();
                }
            );

            //used to debug commands and UI components
            OnTransitioned
              (
                (t) => Debug.WriteLine
                  (
                    "MainWindowWorkflow transitioned from {0} -> {1} [{2}]",
                    t.Source, t.Destination, t.Trigger
                  )
              );
        }

        #endregion

        #region - private functions -

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
