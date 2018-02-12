//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System.Windows;

namespace DialogEngine
{
    /// <summary>
    /// Application startup class 
    /// </summary>
    public partial class App : Application
    {

        

        // Handling all unhandled exceptions in application
        private void _application_DisptatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }

        private void _application_Startup(object sender, StartupEventArgs e)
        {
           
            MessageFilter.Register();
        }

        private void _application_Deactivated(object sender, System.EventArgs e)
        {
            MessageFilter.Revoke();
        }
    }
}