

using DialogEngine.ViewModels.Workflows;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class IsSkipStepBtnEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int _currentStep;
            int _stepsCount;

            try
            {
                 _currentStep = int.Parse(values[0].ToString());
                 _stepsCount = int.Parse(values[1].ToString());
            }
            catch
            {
                return false;
            }

            return _currentStep < _stepsCount-1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
