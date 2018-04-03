//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

//  Converters convert input parameter to a value expected by a gui item  

using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Converts current DataGridRow to its index in DataGrid and vice versa
    /// </summary>
    public class RowIndexConverter : IValueConverter
    {
        /// <summary>
        /// Converts current DataGridRow to its index in DataGrid
        /// </summary>
        /// <param name="_value">DataGridRow</param>
        /// <param name="_targetType"></param>
        /// <param name="_parameter"></param>
        /// <param name="_culture"></param>
        /// <returns>Returns row index in DataGrid</returns>
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            return (_value as DataGridRow).GetIndex() + 1;  // add 1 because intexes starts with 0
        }

        /// <summary>
        /// Converts index to DataGridRow
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_targetType"></param>
        /// <param name="_parameter"></param>
        /// <param name="_culture"></param>
        /// <returns>It is OneWay binding so method returns null</returns>
        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            return null;
        }
    }
}
