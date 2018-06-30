using System;
using System.Globalization;
using System.Windows.Data;
using DialogEngine.Workflows.WizardWorkflow;

namespace DialogEngine.Converters
{
    public class IsEnabledCancelBtnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                States state = (States)value;
                return state == States.ReadyForUserAction;
            }
            catch (Exception) { }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
