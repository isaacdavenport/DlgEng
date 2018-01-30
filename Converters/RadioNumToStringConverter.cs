//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

//  Converters convert input parameter to a value expected by a gui item  

using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Converts radio number to its value or "No assigned" if value is -1
    /// </summary>
    public class RadioNumToStringConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Radio number</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>If radio number > 0 returns radio number else "No assigned" string</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int radioNum = Int32.Parse(value.ToString());

            return radioNum < 0 ? "No assigned" : radioNum.ToString();
        }


        /// <summary>
        /// 
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
