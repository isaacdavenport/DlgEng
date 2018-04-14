

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

            try
            {
                bool.Parse(setting.Value);
                return element.FindResource("BoolDataTemplate") as DataTemplate;
            }
            catch (System.Exception)
            {
                return element.FindResource("StringDataTemplate") as DataTemplate;
            }
        }
    }
}
