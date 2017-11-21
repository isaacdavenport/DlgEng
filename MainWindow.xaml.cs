//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using DialogEngine.Controls;
using log4net;
using log4net.Config;

namespace DialogEngine
{
    /// <summary>
    /// Application's main window
    /// </summary>
    public partial class MainWindow : Window
    {
        #region -fields-
        
        // Default application logger
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region - constructor -

        public MainWindow()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            mcLogger.Info("Application started.");

            InitializeComponent();

            mainFrame.Source = new Uri("Views/Dialog/Dialog.xaml", UriKind.Relative);

            
        }

        #endregion

        #region -Public functions-

        public void WriteStatusInfo(string _infoMessage, Brush _infoColor)
        {
            StatusBarTextBox.Foreground = _infoColor;
            StatusBarTextBox.Text = _infoMessage;
        }

        #endregion

        #region - private methods -

        private void _settings_Click(object sender, RoutedEventArgs e)
        {
            var _settingsDialog = new SettingsDialog();
            _settingsDialog.Owner = this;

            _settingsDialog.ShowDialog();
        }

        // Click on main menu item dialog
        private void _dialog_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            mainFrame.Source = new Uri("Views/Dialog/Dialog.xaml", UriKind.Relative);
        }

        private void _aboutDialogEngine_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://sites.google.com/isaacdavenport.com/toys2life/home");
        }

        #endregion
    }
}