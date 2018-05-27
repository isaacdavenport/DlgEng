//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Helpers;
using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace DialogEngine
{
    /// <summary>
    /// Application startup class 
    /// </summary>
    public partial class App : Application
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region - event handlers -

        // Handling all unhandled exceptions in application
        private void _application_DisptatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }

        private async void _application_Startup(object sender, StartupEventArgs e)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            await ConfigHelper.Instance.DeserializeSettingsFromFile();

            mcLogger.Info("Application started.");
        }

        #endregion 
    }
}