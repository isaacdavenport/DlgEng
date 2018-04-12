

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace DialogEngine.Hendlers.Workflows.SerialSelectionWorkflow
{
    public enum States
    {
       Start,
       Init,
       SerialPortNameError,
       USB_disconnectedError,
       ReadMessage,
       FindClosestPair,
       SelectNextCharacters,
       Finish
    }

    public enum Triggers
    {
        Start,
        Init,
        SerialPortNameError,
        USB_disconnectedError,
        ReadMessage,
        FindClosestPair,
        SelectNextCharacters,
        Finish
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

            //used to debug commands and UI components
            OnTransitioned
              (
                (t) => Debug.WriteLine
                  (
                    "SerialSelectionWorkflow transitioned from {0} -> {1} [{2}]",
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
