using DialogEngine.Controls.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace DialogEngine.Controls.Views
{
    /// <summary>
    /// Interaction logic for MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayerControl : UserControl
    {
        #region - constructor -

        public MediaPlayerControl()
        {
            InitializeComponent();

            this.DataContext = new MediaPlayerControlViewModel(this);
        }

        #endregion

        #region - dependency properties -

        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(MediaPlayerControl));

        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        #endregion

    }
}
