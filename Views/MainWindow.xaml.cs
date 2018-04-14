//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System.Windows;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using DialogEngine.ViewModels;

namespace DialogEngine
{
    /// <summary>
    /// Application's main window
    /// </summary>
    public partial class MainWindow : Window
    {
        #region - constructor -

        public MainWindow()
        {
            _initializeMaterialDesign();
            InitializeComponent();
            DataContext = new MainWindowViewModel();
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