//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

//  Converters convert input parameter to a value expected by a gui item  

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
    /// Generate unique groupName for radio button. 
    /// Extends <see cref="IMultiValueConverter"/> because we used it in multibinding.
    /// We need unique groupname for radio button, because radiobutton groupname has application scope (unfortunately :( )
    /// </summary>
    public class DynamicGroupNameConverter : IMultiValueConverter
    {
        /// <summary>
        /// Creates unique groupName for radiobutton based on parent control name and another unique parameter
        /// </summary>
        /// <param name="_values"></param>
        /// <param name="_targetType"></param>
        /// <param name="_parameter"></param>
        /// <param name="_culture"></param>
        /// <returns>unique groupName</returns>
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            string _characterName = _values[0]?.ToString();

            string _comboboxName = _values[1]?.ToString();

            return _comboboxName + _characterName;

        }


        /// <summary>
        /// Converts value from target to source
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_targetTypes"></param>
        /// <param name="_parameter"></param>
        /// <param name="_culture"></param>
        /// <returns>This is used in OneWay binding, so method returns null</returns>
        public object[] ConvertBack(object _value, Type[] _targetTypes, object _parameter, CultureInfo _culture)
        {
            return null;
        }
    }
}
