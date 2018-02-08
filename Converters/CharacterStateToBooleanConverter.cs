//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

//  Converters convert input parameter to a value expected by a gui item  

using DialogEngine.Models.Enums;
using System;
using System.Windows.Data;
using System.Globalization;


namespace DialogEngine.Converters
{
    /// <summary>
    /// Checks radio button depending on Character state
    /// </summary>
    public class CharacterStateToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Checks radio button if CharacterState enum value is equal to casted CharacterState string value
        /// </summary>
        /// <param name="_value">Character state</param>
        /// <param name="_targetType"></param>
        /// <param name="_parameter">Expected stete</param>
        /// <param name="_culture"></param>
        /// <returns> bool </returns>
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            // get expected state
            CharacterState _state = (CharacterState)_parameter;

            // character state
            CharacterState _enumValue = (CharacterState)_value;

            return _state == _enumValue;
        }


        /// <summary>
        /// Casts CharacterState string value to CharacterState enum value
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_targetType"></param>
        /// <param name="_parameter">New character state as string value</param>
        /// <param name="_culture"></param>
        /// <returns>New character state as enum value</returns>
        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {         
            return (CharacterState)_parameter; ;
        }
    }
}
