using DialogEngine.Controls.ViewModels;
using DialogEngine.Controls.VoiceRecorder;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Wizard;
using System;
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

            this.DataContext = new VoiceRecorderControlViewModel(this,NAudioEngine.Instance);
        }

        #endregion

        #region - dependency properties -


        public static readonly DependencyProperty CurrentCharacterProperty =
            DependencyProperty.Register("CurrentCharacter", typeof(Character), typeof(VoiceRecorederControl),new PropertyMetadata(null));

        public static readonly DependencyProperty CurrentTutorialStepProperty =
            DependencyProperty.Register("CurrentTutorialStep", typeof(TutorialStep), typeof(VoiceRecorederControl),new PropertyMetadata(null));

        public static readonly DependencyProperty RecordingAllowedProperty =
            DependencyProperty.Register("RecordingAllowed", typeof(bool), typeof(VoiceRecorederControl),new PropertyMetadata(false));

        public static readonly DependencyProperty IsPlayingLineInContextProperty =
            DependencyProperty.Register("IsPlayingLineInContext", typeof(bool), typeof(VoiceRecorederControl), new PropertyMetadata(false));

        public Character CurrentCharacter
        {
            get { return (Character)GetValue(CurrentCharacterProperty); }
            set { SetValue(CurrentCharacterProperty, value); }
        }

        public TutorialStep CurrentTutorialStep
        {
            get { return (TutorialStep)GetValue(CurrentTutorialStepProperty); }
            set
            {
                SetValue(CurrentTutorialStepProperty, value);
                (this.DataContext as VoiceRecorderControlViewModel).ResetData();
            }
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

