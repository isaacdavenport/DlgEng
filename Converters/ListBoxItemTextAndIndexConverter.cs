

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DialogEngine.Converters
{
    public class ListBoxItemTextAndIndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DependencyObject item = (DependencyObject)values[1];
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            int index =ic.ItemContainerGenerator.IndexFromContainer(item) + 1;

            string _tagName = values[0].ToString();

            return index + ". " + _tagName.Substring(5, _tagName.Length - 5); 
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
