

using DialogEngine.Models.Shared;
using System.Windows;
using System.Windows.Controls;

namespace DialogEngine.Dialogs
{
    public class SettingsDataTemplateSelector: DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            Setting setting = item as Setting;
            bool temp;

            if(bool.TryParse(setting.Value,out temp))
            {
                return element.FindResource("BoolDataTemplate") as DataTemplate;
            }
            else
            {
                return element.FindResource("StringDataTemplate") as DataTemplate;
            }
        }
    }
}
