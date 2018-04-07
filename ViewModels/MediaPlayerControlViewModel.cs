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
        private bool mIsPlaying;

        public event EventHandler PlayRequested;
        public event EventHandler StopRequested;

        #endregion

        #region - constructor -

        /// <summary>
        /// Constructor
        /// </summary>
        public MediaPlayerControlViewModel()
        {
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

        #region - private functions -

        private void _bindCommands()
        {
            StartVideo = new RelayCommand(x => StartMediaPlayer());
            StopVideo = new RelayCommand(x => StopMediaPlayer());
        }

        #endregion

        #region - public functions -

        public void StartMediaPlayer()
        {
            IsPlaying = true;

            PlayRequested(this, EventArgs.Empty);
            //mView.VideoPlayer.Play();

            //if (mMediaDuration.TotalSeconds > 0)
            //{
            //    mcMediaTimer.Start();
            //}
        }


        private void StopMediaPlayer()
        {
            IsPlaying = false;
            StopRequested(this, EventArgs.Empty);
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
