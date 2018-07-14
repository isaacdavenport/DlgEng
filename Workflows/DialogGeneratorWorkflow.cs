

using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DialogEngine.Workflows.DialogGeneratorWorkflow
{
    public enum States
    {
        Start,
        Init,
        Idle,
        GenerateADialog,
        PreparingDialogParameters,
        DialogStarted,
        DialogFinished
    }

    public enum Triggers
    {
        Start,
        Initialize,
        WaitForNewCharacters,
        GenerateADialog,
        PrepareDialogParameters,
        StartDialog,
        FinishDialog
    }

    public class StateMachine : Stateless.StateMachine<States, Triggers>, INotifyPropertyChanged
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

            ////used to debug commands and UI components
            //OnTransitioned
            //  (
            //    (t) => Debug.WriteLine
            //      (
            //        "DialogWorkflow transitioned from {0} -> {1} [{2}]",
            //        t.Source, t.Destination, t.Trigger
            //      )
            //  );
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
