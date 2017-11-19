﻿namespace DialogEngine.Core
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    using DialogEngine.Helpers;

    /// <summary>
    ///     Base class for all pages in application
    ///     Extends <see cref="Page" />
    ///     Implements <see cref="INotifyPropertyChanged" />
    /// </summary>
    public abstract class PageBase : Page, INotifyPropertyChanged
    {
        /// <summary>
        /// </summary>
        public PageBase()
        {
            this.Loaded += this.onPageBaseLoaded;
            this.Unloaded += this.onPageBaseUnloaded;
        }

        /// <summary>
        ///     Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Forces reload of parent frame.
        /// </summary>
        public void Refresh()
        {
            var _mainFrame = VisualHelper.GetNearestContainer<Frame>(this);

            if (_mainFrame != null) _mainFrame.Refresh();
        }

        /// <summary>
        ///     Occurs when page is loaded for first time.
        /// </summary>
        protected virtual void OnPageLoaded()
        {
        }

        /// <summary>
        ///     Occurs when page is unloaded.
        /// </summary>
        protected virtual void OnPageUnloaded()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="_propertyName"></param>
        protected void OnPropertyChanged(string _propertyName)
        {
            if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs(_propertyName));
        }

        /// <summary>
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void onPageBaseLoaded(object _sender, RoutedEventArgs _e)
        {
            this.Loaded -= this.onPageBaseLoaded;

            this.OnPageLoaded();
        }

        /// <summary>
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void onPageBaseUnloaded(object _sender, RoutedEventArgs _e)
        {
            this.Unloaded -= this.onPageBaseUnloaded;

            // Remove all command bindings on unload
            while (this.CommandBindings.Count > 0) this.CommandBindings.RemoveAt(0);

            this.OnPageUnloaded();
        }
    }
}