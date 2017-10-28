using System.ComponentModel;
using System.Windows.Controls;
using DialogEngine.Helpers;

namespace DialogEngine.Core
{   
    

    /// <summary>
    /// Base class for all pages in application
    /// Extends <see cref="Page"/>
    /// Implements <see cref="INotifyPropertyChanged"/>
    /// </summary>
    public abstract class PageBase: Page , INotifyPropertyChanged
    {
        #region - Fields -
        #endregion

        #region - Events -
        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region  - Constructor -
        /// <summary>
        /// 
        /// </summary>
        public PageBase()
        {
            this.Loaded += onPageBaseLoaded;
            this.Unloaded += onPageBaseUnloaded;
        }

        #endregion

        #region - Private methods -
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onPageBaseLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Loaded -= onPageBaseLoaded;

            OnPageLoaded();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onPageBaseUnloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Unloaded -= onPageBaseUnloaded;

            // Remove all command bindings on unload
            while (this.CommandBindings.Count > 0)
                CommandBindings.RemoveAt(0);

            OnPageUnloaded();
        }

        /// <summary>
        /// Occurs when page is loaded for first time.
        /// </summary>
        protected virtual void OnPageLoaded() { }

        /// <summary>
        /// Occurs when page is unloaded.
        /// </summary>
        protected virtual void OnPageUnloaded() { }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Forces reload of parent frame.
        /// </summary>
        public void Refresh()
        {
            var mainFrame = VisualHelper.GetNearestContainer<Frame>(this);

            if (mainFrame != null)
            {
                mainFrame.Refresh();
            }
        }

        #endregion
    }
}
