using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DialogEngine.Core
{
    /// <summary>
    ///   A command whose sole purpose is to 
    ///   relay its functionality to other
    ///   objects by invoking delegates. The
    ///   default return value for the CanExecute
    ///   method is 'true'.
    /// </summary>
    
    public class RelayCommand : ICommand
    {
        #region Constants and Fields

        private readonly Predicate<object> _canExecute;

        private readonly Action<object> _execute;

        private Action<object> _action;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Creates a new command that can always execute.
        /// </summary>
        /// <param name = "execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        ///   Creates a new command.
        /// </summary>
        /// <param name = "execute">The execution logic.</param>
        /// <param name = "canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("Execute");
            }

            this._execute = execute;
            this._canExecute = canExecute;
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion


        #region Implemented Interfaces

        #region ICommand

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            //if (CanExecuteChanged != null)
            //    CanExecuteChanged(this, new EventArgs());
            CommandManager.InvalidateRequerySuggested();
        }

        public void Execute(object parameter)
        {
            this._execute(parameter);
        }

        #endregion

        #endregion
    }
}
