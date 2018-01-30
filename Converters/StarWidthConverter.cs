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
    /// Assigns width for GridViewColumn as Grid.StarWith
    /// </summary>
    public class StarWidthConverter : IValueConverter
    {
        /// <summary>
        /// Gridview doesn't provide width="*" for columns width, so this converter solved that problem
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Calculated width</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListView _listview = value as ListView;

            double width = ((_listview.Parent as TabItem).Parent as TabControl).ActualWidth;

            GridView _gridView = _listview.View as GridView;

            for (int i = 1; i < _gridView.Columns.Count; i++)
            {
                if (!Double.IsNaN(_gridView.Columns[i].Width))
                    width -= _gridView.Columns[i].Width;
            }

            return width - 20;// this is to take care of margin/padding
        }

        /// <summary>
        /// Converts Grid.StarWidth to double 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Returns null because it is always "OneWay" binding</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;   // it is oneWay binding so we can return null
        }
    }
}
