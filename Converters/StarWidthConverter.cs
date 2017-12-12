//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Gridview doesn't provide width="*" for columns width, so this converter solved that problem
    /// </summary>
    public class StarWidthConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            ListView _listview = _value as ListView;
            double _width = _listview.ActualWidth;
            GridView _gv = _listview.View as GridView;

            for (int i = 0; i < _gv.Columns.Count; i++)
            {
                if (!Double.IsNaN(_gv.Columns[i].Width))
                    _width -= _gv.Columns[i].Width;
            }
            return _width - 20;// this is to take care of margin/padding
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;   // it is oneWay binding so we can return null
        }
    }
}
