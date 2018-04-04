

using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class IsPlayInContextEnabledConveter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool _isPlayingEnabled = (bool)values[0];
            bool _isPlayingLineInContext = (bool)values[1];

            return _isPlayingEnabled  || _isPlayingLineInContext;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
