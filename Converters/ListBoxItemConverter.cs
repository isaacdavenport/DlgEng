//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

//  Converters convert input parameter to a value expected by a gui item  

using DialogEngine.Models.Dialog;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Add radio number to character name if radio assigned to character
    /// </summary>
    public class ListBoxItemConverter : IValueConverter
    {
        /// <summary>
        /// Add radio number to character name if radio assigned to character
        /// </summary>
        /// <param name="value">Character <see cref="Character"/></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Radio number concatenated to character name</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Character character = value as Character;
            string _displayValue = character.RadioNum >= 0 ? "R"+character.RadioNum.ToString() : "";

            return  _displayValue + "  " + character.CharacterName;
        }

        /// <summary>
        /// We don't need convertion back
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
