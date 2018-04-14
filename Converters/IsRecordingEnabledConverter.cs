

using DialogEngine.ViewModels.WizardWorkflow;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class IsRecordingEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            States state = (States)values[0];
            string[] states = parameter.ToString().Split('|');            
            bool _isRecordingAllowed = false;
            try
            {
                _isRecordingAllowed = (bool)values[1];
            }
            catch{}

            return _isRecordingAllowed && states.Contains(state.ToString());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
