

using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class TutorialStepNameConverter : IValueConverter
    {   

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _tutorialStepName = (string)value;

            return _tutorialStepName.Substring(5, _tutorialStepName.Length - 5);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
