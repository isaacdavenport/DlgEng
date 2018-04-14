//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

//  Converters convert input parameter to a value expected by a gui item  

using System;
using System.Globalization;
using System.Windows.Data;
using System.Data;
using DialogEngine.Hendlers;
using DialogEngine.Services;

namespace DialogEngine.Converters
{
    /// <summary>
    /// Converts matrix to <see cref="DataView"/> 
    /// </summary>
    public class MatrixToDataViewConverter : IValueConverter
    {
        /// <summary>
        /// Converts matrix to <see cref="DataView"/> 
        /// </summary>
        /// <param name="value">Rssi data received from toys</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var array = value as int[,];
            if (array == null) return null;
            var rows = array.GetLength(0);
            if (rows == 0) return null;
            var columns = array.GetLength(1);
            if (columns == 0) return null;

            var t = new DataTable();
            // Add columns with name "0", "1", "2", ...
            t.Columns.Add(new DataColumn("--"));

            for (var c = 0; c < columns; c++)
            {
                t.Columns.Add(new DataColumn(c.ToString()));
            }

            t.Columns.Add(new DataColumn("Update time"));

            // Add data to DataTable
            for (var r = 0; r < rows; r++)
            {
                var newRow = t.NewRow();
                newRow[0] = r.ToString();

                for (var c = 1; c <= columns; c++)
                {
                    newRow[c] = array[r, c-1];                    
                }

                newRow[columns+1] = SerialSelectionService.CharactersLastHeatMapUpdateTime[r].ToString("mm.ss.fff");
                t.Rows.Add(newRow);
            }

            return t.DefaultView;
        }

        /// <summary>
        /// We don't need convertion back
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
