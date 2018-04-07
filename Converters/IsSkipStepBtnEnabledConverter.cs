﻿

using System;
using System.Globalization;
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
                 _currentStep = Int32.Parse(values[0].ToString());
                _stepsCount = Int32.Parse(values[1].ToString());
            }
            catch (Exception)
            {
                return true;
            }

            return _currentStep < _stepsCount - 1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}