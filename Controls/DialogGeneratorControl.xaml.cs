
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DialogEngine.Controls.Views
{
    /// <summary>
    /// Interaction logic for DialogControl.xaml
    /// </summary>
    public partial class DialogGeneratorControl : UserControl
    {
        #region - constructor -

        public DialogGeneratorControl()
        {
            InitializeComponent();
        }

        #endregion

        #region - dependency properties -

        public static readonly DependencyProperty CollectionChangedProperty =
            DependencyProperty.Register("CollectionChanged", typeof(bool), typeof(DialogGeneratorControl), new PropertyMetadata(false,_onCollectionChanged ));

        private static void _onCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                DialogGeneratorControl _dialogControl = d as DialogGeneratorControl;

                (VisualTreeHelper.GetChild(_dialogControl.TextOutput, 0) as ScrollViewer).ScrollToBottom();

            }
            catch (Exception) { }
        }


        public bool CollectionChanged
        {
            get { return (bool)GetValue(CollectionChangedProperty); }
            set { SetValue(CollectionChangedProperty, value); }
        }

        #endregion
    }
}
