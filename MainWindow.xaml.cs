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
using Microsoft.Win32;
using DialogEngine.Helpers;
using DialogEngine.ViewModels.Dialog;

namespace DialogEngine
{
    /// <summary>
    /// Application's main window
    /// </summary>
    public partial class MainWindow : Window
    {
        #region - fields -
        
        // Default application logger
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region - constructor -

        public MainWindow()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            mcLogger.Info("Application started.");

            InitializeComponent();

            mainFrame.Source = new Uri("Views/Dialog/DialogView.xaml", UriKind.Relative);

            
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
            mainFrame.Source = new Uri("Views/Dialog/DialogView.xaml", UriKind.Relative);
        }

        private void _aboutDialogEngine_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://sites.google.com/isaacdavenport.com/toys2life/home");
        }

        private void _addCharacter_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();

            _openFileDialog.DefaultExt = "json";
            _openFileDialog.Filter = "Json file (*.json) | *.json";

            Nullable<bool> result = _openFileDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    string fileName = _openFileDialog.FileName;

                    File.Copy(fileName, Path.Combine(SessionVariables.BaseDirectory, SessionVariables.CharactersDirectory, Path.GetFileName(fileName)));
                }
                catch (Exception ex)
                {
                    mcLogger.Error("Error during saving new character " + ex.Message);
                    MessageBox.Show("Error during saving new character.");
                }
            }

        }

        private void _addDialogModel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();

            _openFileDialog.DefaultExt = "json";
            _openFileDialog.Filter = "Json file (*.json) | *.json";

            Nullable<bool> result = _openFileDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    string fileName = _openFileDialog.FileName;

                    File.Copy(fileName, Path.Combine(SessionVariables.BaseDirectory, SessionVariables.DialogsDirectory, Path.GetFileName(fileName)));
                }
                catch (Exception ex)
                {
                    mcLogger.Error("Error during saving new dialog model " + ex.Message);
                    MessageBox.Show("Error during saving new dialog model.");
                }
            }
        }

        #endregion

        #region - public methods -

        public void WriteStatusInfo(string _infoMessage, Brush _infoColor)
        {
            StatusBarTextBox.Foreground = _infoColor;
            StatusBarTextBox.Text = _infoMessage;
        }



        #endregion

        private void _reloadFiles_Click(object sender, RoutedEventArgs e)
        {
            DialogViewModel.Instance.ReloadDialogData();
        }
    }
}