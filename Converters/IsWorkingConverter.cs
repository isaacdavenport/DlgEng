using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class IsWorkingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Visibility _isWorking = (Visibility)value;

                return _isWorking == Visibility.Visible ? false : true;
            }
            catch (Exception) { }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
