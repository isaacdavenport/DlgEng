//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Helpers;
using DialogEngine.Models.Shared;
using log4net;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;


namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for SettingsDialogControl.xaml
    /// </summary>
    public partial class SettingsDialog : UserControl, INotifyPropertyChanged
    {
        #region -fields-

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private List<Setting> mSettings;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - constructor -

        /// <summary>
        /// Creates instance of <see cref="SettingsDialog"/>
        /// </summary>
        public SettingsDialog()
        {
            DataContext = this;
            InitializeComponent();

            Settings = ConfigHelper.Instance.Settings;
        }

        #endregion

        #region - event handlers -

        private void _closeBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<TextBox> _textBoxes = VisualHelper.FindVisualChildren<TextBox>(this).ToList();

            foreach(TextBox tb in _textBoxes)
            {
                tb.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                if (Validation.GetHasError(tb))
                    return;
            }

            DialogHost.CloseDialogCommand.Execute(null, sender as Button);
        }

        #endregion

        #region - public functions -

        /// <summary>
        /// Notifies of property changed.
        /// </summary>
        /// <param name="_propertyName">Name of changed property.</param>
        public virtual void OnPropertyChanged(string _propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }

        #endregion

        #region - properties -

        public List<Setting> Settings
        {
            get { return mSettings; }
            set
            {
                mSettings = value;
                OnPropertyChanged("Settings");
            }
        }

        #endregion
    }
}
