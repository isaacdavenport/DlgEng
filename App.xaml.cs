//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Dialogs;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Shared;
using log4net;
using log4net.Config;
using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

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

        #region - constructor -

        public App()
        {
            var _openCharacterFormCommandBinding = new CommandBinding(GlobalCommands.OpenCharacterFormCommand, _openCharacterForm);
            CommandManager.RegisterClassCommandBinding(typeof(Window), _openCharacterFormCommandBinding);

            var _importCharactgerCommandBinding = new CommandBinding(GlobalCommands.ImportCharacterCommand, _importCharacter);
            CommandManager.RegisterClassCommandBinding(typeof(Window), _importCharactgerCommandBinding);

            var _exportFormCommandBinding = new CommandBinding(GlobalCommands.ExportCharacterCommand, _exportCharacter);
            CommandManager.RegisterClassCommandBinding(typeof(Window), _exportFormCommandBinding);

            var _editWithJSONEditorCommandBinding = new CommandBinding(GlobalCommands.EditWithJSONEditorCommand, _editWithJSONEditor);
            CommandManager.RegisterClassCommandBinding(typeof(Window), _editWithJSONEditorCommandBinding);
        }


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

        private async void _openCharacterForm(object sender, ExecutedRoutedEventArgs e)
        {
            CharacterFormDialog dialog = e.Parameter == null ? new CharacterFormDialog() 
                                                             : new CharacterFormDialog(e.Parameter as Character);

            await DialogHost.Show(dialog);            
        }

        private async void _exportCharacter(object sender, ExecutedRoutedEventArgs e)
        {
            ExportCharacterDialog dialog = e.Parameter == null ? new ExportCharacterDialog() 
                                                               : new ExportCharacterDialog(e.Parameter as Character);

            await DialogHost.Show(dialog);
        }

        private async void _importCharacter(object sender, ExecutedRoutedEventArgs e)
        {
            ImportCharacterDialog dialog = new ImportCharacterDialog();

            await DialogHost.Show(dialog);
        }


        private async void _editWithJSONEditor(object sender, ExecutedRoutedEventArgs e)
        {
            EditWithJSONEditorDialog  dialog = e.Parameter == null ? new EditWithJSONEditorDialog()
                                                : new EditWithJSONEditorDialog(e.Parameter as Character);

            await DialogHost.Show(dialog);
        }

        #endregion 
    }
}