

using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class IsRecordingEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool _isPlaying = (bool)values[0];
            bool _isRecordingAllowed = false;
            try
            {
                _isRecordingAllowed = (bool)values[1];
            }
            catch{}

            return !_isPlaying && _isRecordingAllowed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
