//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

// Base classes for mvvm, enabling gui decoupling from other logic

using System;
using System.ComponentModel;
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
        #region - Constructor -

        /// <summary>
        /// Intitalizes new instance of ViewModelBase.
        /// </summary>
        public ViewModelBase()
        {

        }

        #endregion

        #region - IDisposable implementation -

        ~ViewModelBase()
        {
            this.dispose(false);
        }


        protected void dispose(bool disposing)
        {

            if (disposing)
            {
                onDisposing();
            }
        }

        protected virtual void onDisposing() { }

        /// <summary>
        /// Dispose the object
        /// </summary>
        public void Dispose()
        {
            this.dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        #region - Events  -

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - Public methods -

        /// <summary>
        /// Notifies of property changed.
        /// </summary>
        /// <param name="_propertyName">Name of changed property.</param>
        public virtual void OnPropertyChanged(string _propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_propertyName));
            }
        }

        /// <summary>
        /// Used to execute methods on application close.
        /// </summary>
        /// <param name="_e">Closing event.</param>
        /// <remarks>Call method on overriden OnClosing() method in application main window.</remarks>
        public virtual void OnClose(CancelEventArgs _e)
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
    }
}
