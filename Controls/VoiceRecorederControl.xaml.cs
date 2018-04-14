
using DialogEngine.ViewModels.WizardWorkflow;
using System.Windows;
using System.Windows.Controls;

namespace DialogEngine.Controls.Views
{
    /// <summary>
    /// Interaction logic for VoiceRecoreder.xaml
    /// </summary>
    public partial class VoiceRecorederControl : UserControl
    {
        #region - constructor -

        public VoiceRecorederControl()
        {
            InitializeComponent();
        }

        #endregion

        #region - dependency properties -


        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(States), typeof(VoiceRecorederControl),new PropertyMetadata(States.Start));

        public static readonly DependencyProperty RecordingAllowedProperty =
            DependencyProperty.Register("RecordingAllowed", typeof(bool), typeof(VoiceRecorederControl),new PropertyMetadata(false));

        public static readonly DependencyProperty IsPlayingLineInContextProperty =
            DependencyProperty.Register("IsPlayingLineInContext", typeof(bool), typeof(VoiceRecorederControl), new PropertyMetadata(false));

        public States State
        {
            get { return (States)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public bool RecordingAllowed
        {
            get { return (bool)GetValue(RecordingAllowedProperty); }
            set { SetValue(RecordingAllowedProperty, value);}
        }

        public bool IsPlayingLineInContext
        {
            get { return (bool)GetValue(IsPlayingLineInContextProperty); }
            set { SetValue(IsPlayingLineInContextProperty, value); }
        }

        #endregion
    }
}

