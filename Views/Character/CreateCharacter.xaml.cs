using DialogEngine.Core;


namespace DialogEngine.Views.Character
{
    /// <summary>
    /// Interaction logic for CreateCharacter.xaml
    /// </summary>
    public partial class CreateCharacter : PageBase
    {
        public CreateCharacter()
        {
            InitializeComponent();
        }

        protected async override void onPageLoaded()
        {

        }

        private void _startVideo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            VideoPlayer.Play();
        }

        private void _stopVideo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            VideoPlayer.Stop();
        }
    }
}
