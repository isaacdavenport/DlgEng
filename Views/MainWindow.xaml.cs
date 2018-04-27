﻿//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System.Windows;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using DialogEngine.ViewModels;
using System.Windows.Input;
using System;
using System.Runtime.Caching;
using log4net;
using System.Reflection;
using DialogEngine.Core;

namespace DialogEngine
{
    /// <summary>
    /// Application's main window
    /// </summary>
    public partial class MainWindow : Window
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string mHomeView = "/Views/DialogView.xaml";
        private string mCurrentView = "";
        // used to cache pages in application to keep page state persistent
        private ObjectCache mMemoryCache = MemoryCache.Default;

        #endregion

        #region - constructor -

        public MainWindow()
        {
            _initializeMaterialDesign();
            InitializeComponent();

            this.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseBack,_onBrowseBack,(sender,e) => { e.CanExecute = mainFrame.CanGoBack; }));
            this.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseHome, _onBrowseHome));
            this.CommandBindings.Add(new CommandBinding(NavigationCommands.GoToPage, _onGoToPage, (sender, e) => { e.CanExecute = e.Parameter != null; }));

            DataContext = new MainWindowViewModel();

            _showHomePage();
        }

        #endregion

        #region - private methods -

        private void _onBrowseBack(object sender, ExecutedRoutedEventArgs e)
        {
            mainFrame.GoBack();
        }

        private void _onBrowseHome(object sender, ExecutedRoutedEventArgs e)
        {
            _onViewRequest(mHomeView);
        }

        private void _onGoToPage(object sender, ExecutedRoutedEventArgs e)
        {
            _onViewRequest((string)e.Parameter);
        }

        private void _onViewRequest(string uri)
        {
            var newUri = new Uri(uri, UriKind.RelativeOrAbsolute);

            try
            {
                if (!mCurrentView.Equals(uri))
                {
                    PageBase page = mMemoryCache.Get(uri) as PageBase;

                    if(page != null)
                    {
                        mainFrame.Navigate(page);
                    }
                    else
                    {
                        PageBase _newPage =  Application.LoadComponent(new Uri(uri, UriKind.Relative)) as PageBase;
                        mMemoryCache.Add(uri, _newPage,null);
                        mainFrame.Navigate(_newPage);
                    }

                    mCurrentView = uri;
                }
            }
            catch (Exception ex)
            {
                mcLogger.Error("_onViewRequest. " + ex.Message);
            }
        }


        private void _showHomePage()
        {
            _onViewRequest(mHomeView);
        }

        private void _initializeMaterialDesign()
        {
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