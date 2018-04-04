

using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class InputValuesToBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _characterPrefix = values[0] as string;
            string _characterName = values[1] as string;

            return !(string.IsNullOrEmpty(_characterPrefix)
                   || string.IsNullOrEmpty(_characterName));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
