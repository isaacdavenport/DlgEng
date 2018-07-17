

using DialogEngine.Workflows.DialogPageWorkflow;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class IsEnabledDialogViewControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DvmStates _dvmState = (DvmStates)value;
            string[] states = parameter.ToString().Split('|');

            return !states.Contains(_dvmState.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
