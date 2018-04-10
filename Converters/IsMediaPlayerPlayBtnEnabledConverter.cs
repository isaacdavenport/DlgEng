

using DialogEngine.ViewModels.Workflows;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class IsMediaPlayerPlayBtnEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            States state = (States)value;
            string[] states = parameter.ToString().Split('|');

            return states.Contains(state.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new  NotImplementedException();
        }
    }
}
