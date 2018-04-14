

using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class ParseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return bool.Parse((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value).ToString();
        }
    }
}
