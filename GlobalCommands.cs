
using System.Windows.Input;

namespace DialogEngine
{
    public static class GlobalCommands
    {
        private static readonly RoutedUICommand mcOpenCharacterFormCommand = new RoutedUICommand("", "OpenCharacterFormCommand", typeof(GlobalCommands));
        private static readonly RoutedUICommand mcImportCharacterCommand = new RoutedUICommand("","ImportCharacterCommand",typeof(GlobalCommands));
        private static readonly RoutedUICommand mcExportCharacterCommand = new RoutedUICommand("", "ExportCharacterCommand", typeof(GlobalCommands));
        private static readonly RoutedUICommand mcEditWithJSONEditorCommand = new RoutedUICommand("", "ExportWithJSONEditorCommand", typeof(GlobalCommands));

        public static RoutedUICommand OpenCharacterFormCommand
        {
            get { return mcOpenCharacterFormCommand; }
        }

        public static RoutedUICommand ImportCharacterCommand
        {
            get { return mcImportCharacterCommand; }
        }

        public static RoutedUICommand ExportCharacterCommand
        {
            get { return mcExportCharacterCommand; }
        }

        public static RoutedUICommand EditWithJSONEditorCommand
        {
            get { return mcEditWithJSONEditorCommand; }
        }

    }
}
