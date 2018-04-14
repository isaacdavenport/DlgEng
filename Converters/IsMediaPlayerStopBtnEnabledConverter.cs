

using DialogEngine.ViewModels.WizardWorkflow;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class IsMediaPlayerStopBtnEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            States state = (States)values[0];
            bool _isPlaying = (bool)values[1];
            string[] states = parameter.ToString().Split('|');

            return _isPlaying && states.Contains(state.ToString());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
