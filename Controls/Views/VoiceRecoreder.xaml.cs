using DialogEngine.Controls.ViewModels;
using DialogEngine.Controls.VoiceRecorder;
using System.Windows;
using System.Windows.Controls;

namespace DialogEngine.Controls.Views
{
    /// <summary>
    /// Interaction logic for VoiceRecoreder.xaml
    /// </summary>
    public partial class VoiceRecoreder : UserControl
    {

        public VoiceRecoreder()
        {
            InitializeComponent();

            this.DataContext = new VoiceRecorderViewModel(this,NAudioEngine.Instance);

        }
    }
}

