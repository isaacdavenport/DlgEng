using DialogEngine.Models.Dialog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class ListBoxItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            Character character = value as Character;

            string _displayValue = character.RadioNum >= 0 ? "R"+character.RadioNum.ToString() : "";

            return  _displayValue + " " + character.CharacterName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
