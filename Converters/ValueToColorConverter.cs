
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Used to mark selected characters in DataGrid 
    /// Calculate background color for DataGridCell 
    /// </summary>
    public class ValueToColorConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gridCell = value as DataGridCell;

            int row = DataGridRow.GetRowContainingElement(gridCell).GetIndex();

            // we have one column for row header so we need to sub 1
            int column = gridCell.Column.DisplayIndex > 0 ? gridCell.Column.DisplayIndex - 1 : gridCell.Column.DisplayIndex;

            if( row == column && SelectNextCharacters.HeatMap[row,column] > 0)
            {
                if( row == SelectNextCharacters.NextCharacter1 || row == SelectNextCharacters.NextCharacter2)
                {
                    return Brushes.Red;
                }
            }

            return null;

        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
