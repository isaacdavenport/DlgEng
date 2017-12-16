//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Check radio button depend of ModelDialog state
    /// </summary>
    public class ModelDialogStateToBooleanConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            ModelDialogState _state = (ModelDialogState)_parameter;

            ModelDialogState _enumValue = (ModelDialogState)_value;

            return _state == _enumValue;
        }

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            EventAggregator.Instance.GetEvent<ChangedModelDialogStateEvent>().Publish();


            return (ModelDialogState)_parameter;
        }
    }
}
