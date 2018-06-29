
using System.Windows.Input;

namespace DialogEngine
{
    public static class GlobalCommands
    {
        private static readonly RoutedUICommand mcOpenCharacterFormCommand = new RoutedUICommand("", "OpenCharacterFormCommand", typeof(GlobalCommands));

        public static RoutedUICommand OpenCharacterFormCommand
        {
            get { return mcOpenCharacterFormCommand; }
        }

    }
}
