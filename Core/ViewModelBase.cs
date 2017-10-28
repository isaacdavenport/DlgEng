using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace DialogEngine.Core
{

    /// <summary>
    /// Base class for implementing ViewModel class 
    /// Implements <see cref="INotifyPropertyChanged"/> and <see cref="IDisposable"/>
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region - Events  -

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - Constructor -

        /// <summary>
        /// Intitalizes new instance of ViewModelBase.
        /// </summary>
        public ViewModelBase()
        {

        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Gets reference to application main window.
        /// </summary>
        public Window MainWindow
        {
            get { return Application.Current.MainWindow; }
        }


        /// <summary>
        /// Gets application dispatcher.
        /// </summary>
        protected Dispatcher Dispatcher
        {
            get
            {
                if (Application.Current != null)
                {
                    return Application.Current.Dispatcher;
                }
                else
                {
                    return Dispatcher.CurrentDispatcher;
                }
            }
        }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Notifies of property changed.
        /// </summary>
        /// <param name="propertyName">Name of changed property.</param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Used to execute methods on application close.
        /// </summary>
        /// <param name="e">Closing event.</param>
        /// <remarks>Call method on overriden OnClosing() method in application main window.</remarks>
        public virtual void OnClose(CancelEventArgs e)
        {

        }


        #endregion

        #region - IDisposable implementation -

        ~ViewModelBase()
        {
            this.Dispose(false);
        }

        protected void Dispose(bool disposing)
        {

            if (disposing)
            {
                OnDisposing();
            }
        }

        protected virtual void OnDisposing() { }

        /// <summary>
        /// Dispose the object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
