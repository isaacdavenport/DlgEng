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
using DialogEngine.Views;
using DialogEngine.Dialogs;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using DialogEngine.Helpers;
using System.Collections.Generic;
using DialogEngine.Core;

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
        private readonly string mcJsonEditorExeName = "JSONedit.exe";
        private Dictionary<string, PageBase> mPages = new Dictionary<string, PageBase>();

        #endregion

        #region - constructor -

        public MainWindow()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            _initializeMaterialDesign();
            InitializeComponent();

            mainFrame.NavigationService.Navigate(new DialogView(DateTime.Now));
            mcLogger.Info("Application started.");
        }



        #endregion

        #region - event handlers -

        // Click on main menu item dialog
        private void _dialog_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.NavigationService.CanGoBack)
            {
                mainFrame.NavigationService.GoBack();
            }
            else
            {
                mainFrame.NavigationService.Navigate(new DialogView(DateTime.Now));
            }
        }

        private void _editWithJsonEditor_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Path.Combine(SessionVariables.TutorialDirectory,mcJsonEditorExeName),Path.Combine(SessionVariables.WizardDirectory, "StarterCharacterWizard.json"));
        }


        private void _aboutDialogEngine_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("www.toys2life.net");
        }


        private void _tutorial_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Path.Combine(SessionVariables.TutorialDirectory,"tutorial.pdf"));
        }


        private void _settings_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.OpenDialogCommand.Execute(new SettingsDialogControl(),this.SettingsBtn);
        }


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

        #endregion

        #region - private methods -

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