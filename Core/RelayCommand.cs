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
    /// 
    ///   Implementation of ICommand interface
    /// </summary>
    
    public class RelayCommand : ICommand
    {
        #region Constants and Fields

        private readonly Predicate<object> mCanExecute;

        private readonly Action<object> mExecute;

        private Action<object> mAction;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Creates a new command that can always execute.
        /// </summary>
        /// <param name = "_execute">The execution logic.</param>
        public RelayCommand(Action<object> _execute)
            : this(_execute, null)
        {
        }

        /// <summary>
        ///   Creates a new command.
        /// </summary>
        /// <param name = "_execute">The execution logic.</param>
        /// <param name = "_canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> _execute, Predicate<object> _canExecute)
        {
            if (_execute == null)
            {
                throw new ArgumentNullException("Execute");
            }

            this.mExecute = _execute;
            this.mCanExecute = _canExecute;
        }

        #endregion

        #region Events

        /// <summary>
        /// CanExecuteChanged event
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_parameter"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public bool CanExecute(object _parameter)
        {
            return this.mCanExecute == null || this.mCanExecute(_parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            //if (CanExecuteChanged != null)
            //    CanExecuteChanged(this, new EventArgs());
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_parameter"></param>
        public void Execute(object _parameter)
        {
            this.mExecute(_parameter);
        }

        #endregion

        #endregion
    }
}
