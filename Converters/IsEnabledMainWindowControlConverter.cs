

using DialogEngine.Workflows.MainWindowWorkflows;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class IsEnabledMainWindowControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            States state = (States)value;
            string[] states = parameter.ToString().Split('|');

            return !states.Contains(state.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
