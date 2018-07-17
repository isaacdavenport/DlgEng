//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

//  Converters convert input parameter to a value expected by a gui item  


using DialogEngine.Models.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Checks radio button depending on ModelDialog _dvmState
    /// </summary>
    public class ModelDialogStateToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Checks if radio button  ModelDialogState enum value is equal to casted ModelDialogState string value
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

            return (ModelDialogState)_parameter;
        }
    }
}
