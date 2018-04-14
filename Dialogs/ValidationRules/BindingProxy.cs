

using System.Windows;

namespace DialogEngine.Dialogs.ValidationRules
{
    public class BindingProxy : Freezable
    {
        #region - overrides of Freezable -

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        #endregion

        #region - dependency property -

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

        #endregion
    }
}
