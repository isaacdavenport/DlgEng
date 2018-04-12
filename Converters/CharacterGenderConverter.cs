

using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class CharacterGenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // M - Male  F - Female
            string _characterGenderShortName = (string)value;

            return _characterGenderShortName.Equals("M") ? "Male" : "Female";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // M - Male  F - Female
            string _characterGenderName = (string)value;

            return _characterGenderName.Equals("Male") ? "M" : "F";
        }
    }
}
