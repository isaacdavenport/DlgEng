

using System.Windows;

namespace DialogEngine.Dialogs.ValidationRules
{
    public class RegexValidtionRuleWrapper : DependencyObject
    {
        #region - dependency properties -
        public static readonly DependencyProperty RegexPatternProperty =
            DependencyProperty.Register("RegexPattern", typeof(string),
            typeof(RegexValidtionRuleWrapper), new FrameworkPropertyMetadata(""));

        public string RegexPattern
        {
            get { return (string)GetValue(RegexPatternProperty); }
            set { SetValue(RegexPatternProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string),
            typeof(RegexValidtionRuleWrapper), new FrameworkPropertyMetadata(""));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        #endregion
    }
}
