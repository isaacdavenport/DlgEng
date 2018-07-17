

using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DialogEngine.Workflows.SerialSelectionWorkflow
{
    public enum SerialStates
    {
       Start,
       Init,
       Idle,
       SerialPortNameError,
       USB_disconnectedError,
       ReadMessage,
       FindClosestPair,
       SelectNextCharacters,
       Finish
    }

    public enum SerialTriggers
    {
        Start,
        Initialize,
        Idle,
        SerialPortNameError,
        USB_disconnectedError,
        ReadMessage,
        FindClosestPair,
        SelectNextCharacters,
        Finish
    }

    public class StateMachine : Stateless.StateMachine<SerialStates, SerialTriggers>, INotifyPropertyChanged
    {
        #region - fields -

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - constructor -

        public StateMachine(Action action) : base(SerialStates.Start)
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
            //OnTransitioned
            //  (
            //    (t) => Debug.WriteLine
            //      (
            //        "SerialSelectionWorkflow transitioned from {0} -> {1} [{2}]",
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
