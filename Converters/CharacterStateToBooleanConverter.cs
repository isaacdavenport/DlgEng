//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Check radio button depend of Character state
    /// </summary>
    public class CharacterStateToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Check radio button if CharacterState enum value is equal to casted CharacterState string value
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_targetType"></param>
        /// <param name="_parameter"></param>
        /// <param name="_culture"></param>
        /// <returns> bool </returns>
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            CharacterState _state = (CharacterState)_parameter;

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
