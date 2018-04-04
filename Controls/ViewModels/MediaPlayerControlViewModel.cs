//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Controls.Views;
using DialogEngine.Core;
using log4net;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;

namespace DialogEngine.Controls.ViewModels
{
    /// <summary>
    /// Implementation of <see cref="ViewModelBase" />
    /// DataContext for MediaPlayer.xaml/>     
    /// </summary>
    public class MediaPlayerControlViewModel : ViewModelBase
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer mcMediaTimer = new DispatcherTimer();
        private MediaPlayerControl mView;
        private MediaElement mMediaElement;
        private TimeSpan mMediaDuration;
        private bool mIsPlaying;

        #endregion

        #region - constructor -

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view">Reference on View <see cref="MediaPlayer"/></param>
        public MediaPlayerControlViewModel(MediaPlayerControl view)
        {
            this.mView = view;

            this.mMediaElement = mView.VideoPlayer;
            this.mMediaElement.MediaOpened += _mediaElement_MediaOpened;
            this.mMediaElement.MediaEnded += _mediaElement_MediaEnded;
            this.mMediaElement.MediaFailed += _mediaElement_MediaFailed;
            this.mMediaElement.Loaded += _mediaElement_Loaded;

            mcMediaTimer.Interval = TimeSpan.FromMilliseconds(25);
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

        private void _mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            mcLogger.Error("_mediaElement_MediaFailed " + e.ErrorException);
        }

        private void _mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            IsPlaying = false;
            mcMediaTimer.Stop();
            mMediaElement.Stop();
            mView.VideoPlayerSlider.Value = 0;
        }

        private void _mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            mMediaDuration = mMediaElement.NaturalDuration.HasTimeSpan ? mMediaElement.NaturalDuration.TimeSpan : new TimeSpan(0,0,0);
            mView.VideoPlayerSlider.Maximum = mMediaDuration.TotalMilliseconds;
            mcMediaTimer.Start();
        }

        private void _mediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            // run and stop player for video preview
            mMediaElement.Play();
            mMediaElement.Stop();
        }

        #endregion

        #region - private functions -

        private void _bindCommands()
        {
            StartVideo = new RelayCommand(x => StartMediaPlayer());
            StopVideo = new RelayCommand(x => _stopMediaPlayer());
        }

        private void _stopMediaPlayer()
        {
            IsPlaying = false;
            mView.VideoPlayer.Pause();
        }


        #endregion

        #region - public functions -

        public void StartMediaPlayer()
        {
            IsPlaying = true;
            mView.VideoPlayer.Play();

            if (mMediaDuration.TotalSeconds > 0)
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
