//  Confidential Source Code Property Toys2Life LLC Colorado 2017
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
using DialogEngine.Models.Dialog;
using DialogEngine.Views;
using DialogEngine.Helpers;
using System.IO;
using System.Threading.Tasks;

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

        private async void _mainWindow_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Task _serializeSettingsTask = ConfigHelper.Instance.SerializeSettingsToFile();
            // TODO make this happen at end of wizard not when closing application
            //Task _serializeDialogDataTask = DialogDataHelper.SerializeDataToFile(Path.Combine(SessionHelper.WizardDirectory, 
            //    SessionHelper.JSONFileName));

            await _serializeSettingsTask;
            // await _serializeDialogDataTask;

            Application.Current.Shutdown();
        }

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
            _onViewRequest(e.Parameter);
        }

        private void _onViewRequest(object param)
        {
            try
            {
                if(param is Character)
                {
                    PageBase page = mMemoryCache.Get("/Views/WizardView.xaml") as PageBase;

                    if (page != null)
                    {
                        ((page as WizardView).DataContext as WizardViewModel).Character = param as Character;
                        mainFrame.Navigate(page);
                    }
                    else
                    {
                        PageBase _newPage = Application.LoadComponent(new Uri("/Views/WizardView.xaml", UriKind.Relative)) as PageBase;
                        mMemoryCache.Add("/Views/WizardView.xaml", _newPage, null);
                        ((_newPage as WizardView).DataContext as WizardViewModel).Character = param as Character;
                        mainFrame.Navigate(_newPage);
                    }

                    mCurrentView = "/Views/WizardView.xaml";
                }
                else
                {
                    string uri = (string)param;
                    if (!mCurrentView.Equals(uri))
                    {
                        PageBase page = mMemoryCache.Get(uri) as PageBase;

                        if (page != null)
                        {
                            mainFrame.Navigate(page);
                        }
                        else
                        {
                            PageBase _newPage = Application.LoadComponent(new Uri(uri, UriKind.Relative)) as PageBase;
                            mMemoryCache.Add(uri, _newPage, null);
                            mainFrame.Navigate(_newPage);
                        }

                        mCurrentView = uri;
                    }
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