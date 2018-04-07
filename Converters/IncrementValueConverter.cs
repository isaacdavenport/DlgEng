

using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Increments received value
    /// Used when we want to show values from 1 but, array indexes starts with 0
    /// </summary>
    public class IncrementValueConverter : IValueConverter
    {
        /// <summary>
        /// Increments received value
        /// </summary>
        /// <param name="value">Value which we want to increment</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Incremented value for 1</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int _receivedIndex = (int)value;

            return _receivedIndex + 1;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
