

using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class IsVoiceRecorderPlayingEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool _isRecording = (bool)values[0];
            string _currentFilePath = (string)(values[1] ?? string.Empty);
            bool _isPlayingLineInContext = (bool)values[2];

            return !_isRecording && _currentFilePath.Length > 0 && !_isPlayingLineInContext;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
