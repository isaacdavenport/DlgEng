//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using log4net;
using log4net.Config;
using Microsoft.Win32;
using DialogEngine.Helpers;
using DialogEngine.ViewModels;
using DialogEngine.Views;
using DialogEngine.Dialogs;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;

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

            _initializeMaterialDesign();

            InitializeComponent();

            mainFrame.NavigationService.Navigate(new DialogView(DateTime.Now));
        }

        #endregion

        #region - event handlers -

        // Click on main menu item dialog
        private void _dialog_Click(object sender, RoutedEventArgs e)
        {
            if(!mainFrame.Content.ToString().Contains("DialogView"))
            mainFrame.NavigationService.GoBack();

            e.Handled = true;
        }

        private void _aboutDialogEngine_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("www.toys2life.net");
        }


        private void _settings_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.OpenDialogCommand.Execute(new SettingsDialogControl(),this.SettingsBtn);
        }

        #endregion

        #region - private methods -

        private void _createCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.NavigationService.CanGoForward)
            {
                mainFrame.NavigationService.GoForward();
            }
            else
            {
                mainFrame.NavigationService.Navigate(new WizardView());
            }

            e.Handled = true;
        }

        private void _initializeMaterialDesign()
        {
            // Create dummy objects to force the MaterialDesign assemblies to be loaded
            // from this assembly, which causes the MaterialDesign assemblies to be searched
            // relative to this assembly's path. Otherwise, the MaterialDesign assemblies
            // are searched relative to Eclipse's path, so they're not found.
            var card = new Card();
            var hue = new Hue("Dummy", Colors.Black, Colors.White);
        }

        #endregion

        #region - public methods -

        public void WriteStatusInfo(string _infoMessage, Brush _infoColor)
        {
            StatusBarTextBox.Foreground = _infoColor;
            StatusBarTextBox.Text = _infoMessage;
        }

        #endregion
    }
}