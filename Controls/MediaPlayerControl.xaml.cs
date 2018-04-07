using DialogEngine.Controls.ViewModels;
using log4net;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DialogEngine.Controls.Views
{
    /// <summary>
    /// Interaction logic for MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayerControl : UserControl
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer mcMediaTimer = new DispatcherTimer();
        private TimeSpan mMediaDuration;

        #endregion

        #region - constructor -

        public MediaPlayerControl()
        {
            InitializeComponent();

            mcMediaTimer.Interval = TimeSpan.FromMilliseconds(25);
            mcMediaTimer.Tick += _mediaTimer_Tick;

            this.VideoPlayer.MediaOpened += _mediaElement_MediaOpened;
            this.VideoPlayer.MediaEnded += _mediaElement_MediaEnded;
            this.VideoPlayer.MediaFailed += _mediaElement_MediaFailed;
            this.VideoPlayer.Loaded += _mediaElement_Loaded;
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

        #region - event handlers -

        private void _mediaPlayerControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MediaPlayerControlViewModel).PlayRequested += _mediaPlayerControl_PlayRequested;
            (this.DataContext as MediaPlayerControlViewModel).StopRequested += _mediaPlayerControl_StopRequested; ;
        }

        private void _mediaTimer_Tick(object sender, EventArgs e)
        {
            VideoPlayerSlider.Value = VideoPlayer.Position.TotalMilliseconds;
        }

        private void _mediaPlayerControl_StopRequested(object sender, EventArgs e)
        {
            if (VideoPlayer.CanPause)
                VideoPlayer.Stop();
        }

        private void _mediaPlayerControl_PlayRequested(object sender, EventArgs e)
        {
            VideoPlayer.Play();
        }

        private void _mediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Play();
            VideoPlayer.Stop();
        }

        private void _mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            mcLogger.Error("_mediaElement_MediaFailed " + e.ErrorException);
        }

        private void _mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MediaPlayerControlViewModel).IsPlaying = false; 
            mcMediaTimer.Stop();
            VideoPlayer.Stop();
            VideoPlayerSlider.Value = 0;
        }

        private void _mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            mMediaDuration = VideoPlayer.NaturalDuration.HasTimeSpan ? VideoPlayer.NaturalDuration.TimeSpan : new TimeSpan(0,0,0);
            VideoPlayerSlider.Maximum = mMediaDuration.TotalMilliseconds;
            mcMediaTimer.Start();
        }

        #endregion


    }
}
