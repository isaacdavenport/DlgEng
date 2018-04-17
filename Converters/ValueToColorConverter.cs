//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

//  Converters convert input parameter to a value expected by a gui item  

using DialogEngine.Models.Shared;
using DialogEngine.Services;
using DialogEngine.ViewModels;
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
        /// Calculate background color for datagrid cell
        /// </summary>
        /// <param name="value">DataGridCell</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var _gridCell = value as DataGridCell;
            int row = DataGridRow.GetRowContainingElement(_gridCell).GetIndex();
            int column = _gridCell.Column.DisplayIndex;

            if(column == 0)
            {
                return Brushes.WhiteSmoke;
            }

            // first column is row header so we need to sub for 1
            int _heatMapColumn = column - 1;

            if( row == _heatMapColumn && SerialSelectionService.HeatMap[row, _heatMapColumn] > 0)
            {
                if(  row == DialogData.Instance.CharacterCollection[SerialSelectionService.NextCharacter1].RadioNum 
                  || row == DialogData.Instance.CharacterCollection[SerialSelectionService.NextCharacter2].RadioNum)
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
