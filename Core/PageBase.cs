using System.ComponentModel;
using System.Windows.Controls;
using DialogEngine.Helpers;

namespace DialogEngine.Core
{   /// <summary>
    /// 
    /// </summary>
    public abstract class PageBase: Page , INotifyPropertyChanged
    {
        #region - Fields -
        #endregion

        #region - Events -

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region  - Constructor -

        public PageBase()
        {
            this.Loaded += OnPageBaseLoaded;
            this.Unloaded += OnPageBaseUnloaded;
        }

        #endregion

        #region - Private methods -

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnPageBaseLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Loaded -= OnPageBaseLoaded;

            OnPageLoaded();
        }


        private void OnPageBaseUnloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Unloaded -= OnPageBaseUnloaded;

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
