//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Generate unique groupname for radio button. 
    /// Extends <see cref="IMultiValueConverter"/> because we used it in multibinding.
    /// We need unique groupname for radio button, because radiobutton groupname has application scope (unfortunately :( )
    /// </summary>
    public class DynamicGroupNameConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string characterName = values[0]?.ToString();

            string comboboxName = values[1]?.ToString();

            return comboboxName + characterName;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
