//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Controls.Views;
using DialogEngine.Core;
using log4net;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DialogEngine.Controls.ViewModels
{
    /// <summary>
    /// Implementation of <see cref="ViewModelBase" />
    /// DataContext for MediaPlayer.xaml/>     
    /// </summary>
    public class MediaPlayerViewModel : ViewModelBase
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer mcMediaTimer = new DispatcherTimer();
        private MediaPlayer mView;
        private MediaElement mMediaElement;
        private TimeSpan mMediaDuration;
        private bool mIsPlaying;

        #endregion

        #region - constructor -

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view">Reference on View <see cref="MediaPlayer"/></param>
        public MediaPlayerViewModel(MediaPlayer view)
        {
            this.mView = view;

            this.mMediaElement = mView.VideoPlayer;
            this.mMediaElement.MediaOpened += _mediaElement_MediaOpened;
            this.mMediaElement.MediaEnded += _mediaElement_MediaEnded;
            this.mMediaElement.MediaFailed += _mediaElement_MediaFailed;

            mcMediaTimer.Interval = TimeSpan.FromMilliseconds(50);
            mcMediaTimer.Tick += _mediaTimer_Tick;

            _bindCommands();
        }

        #endregion

        #region - commands-

        /// <summary>
        /// Starts video
        /// </summary>
        public RelayCommand StartVideo { get; set; }

        /// <summary>
        /// Stops video
        /// </summary>
        public RelayCommand StopVideo  { get; set; }

        #endregion

        #region - event handlers -

        private void _mediaTimer_Tick(object sender, EventArgs e)
        {
            mView.VideoPlayerSlider.Value = mMediaElement.Position.TotalMilliseconds;
        }

        private void _mediaElement_MediaFailed(object sender, System.Windows.ExceptionRoutedEventArgs e)
        {
            mcLogger.Error("_mediaElement_MediaFailed " + e.ErrorException);
        }

        private void _mediaElement_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            IsPlaying = false;
            mcMediaTimer.Stop();
            mMediaElement.Stop();
            mView.VideoPlayerSlider.Value = 0;
        }

        private void _mediaElement_MediaOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            mMediaDuration = mMediaElement.NaturalDuration.TimeSpan;
            mView.VideoPlayerSlider.Maximum = mMediaDuration.TotalMilliseconds;
            mcMediaTimer.Start();
        }

        #endregion

        #region - private functions -

        private void _bindCommands()
        {
            StartVideo = new RelayCommand(x => _startVideo());
            StopVideo = new RelayCommand(x => _stopVideo());
        }

        private void _stopVideo()
        {
            IsPlaying = false;
            mView.VideoPlayer.Pause();
        }

        private void _startVideo()
        { 
            if(mView.VideoPlayer.Position.TotalMilliseconds == 0)
                mView.VideoPlayer.Source = new Uri(@"Resources/Videos/WZ_BjornAreYouFeelingMetal.avi", UriKind.Relative);

            IsPlaying = true;
            mView.VideoPlayer.Play();

            if(mMediaDuration.TotalSeconds > 0)
            {
                mcMediaTimer.Start();
            }
        }
        #endregion

        #region - properties -

        /// <summary>
        /// Is media player playing
        /// </summary>
        public bool IsPlaying
        {
            get { return mIsPlaying; }
            set
            {
                mIsPlaying = value;
                OnPropertyChanged("IsPlaying");
            }
        }

        #endregion
    }
}
