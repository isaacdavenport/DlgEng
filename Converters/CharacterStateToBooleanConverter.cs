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
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            CharacterState _state = (CharacterState)_parameter;

            CharacterState _enumValue = (CharacterState)_value;

            return _state == _enumValue;
        }

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            EventAggregator.Instance.GetEvent<ChangedCharactersStateEvent>().Publish();


            return (CharacterState)_parameter;
        }
    }
}
