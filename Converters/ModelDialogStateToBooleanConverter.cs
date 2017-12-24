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
        /// <summary>
        /// Check radio button if ModelDialogState enum value is equal to casted ModelDialogState string value
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_targetType"></param>
        /// <param name="_parameter"></param>
        /// <param name="_culture"></param>
        /// <returns>bool</returns>
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            ModelDialogState _state = (ModelDialogState)_parameter;

            ModelDialogState _enumValue = (ModelDialogState)_value;

            return _state == _enumValue;
        }

        /// <summary>
        /// Casts ModelDialogState string value to ModelDialogState enum value
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_targetType"></param>
        /// <param name="_parameter"></param>
        /// <param name="_culture"></param>
        /// <returns>new ModelDialogState</returns>
        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            EventAggregator.Instance.GetEvent<ChangedModelDialogStateEvent>().Publish();


            return (ModelDialogState)_parameter;
        }
    }
}
