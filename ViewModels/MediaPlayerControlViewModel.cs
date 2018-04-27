//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Core;
using log4net;
using System;
using System.Reflection;


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
        public event EventHandler PauseRequested;
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
        public RelayCommand PauseVideo  { get; set; }

        #endregion

        #region - private functions -

        private void _bindCommands()
        {
            StartVideo = new RelayCommand(x => StartMediaPlayer());
            PauseVideo = new RelayCommand(x => _pauseMediaPlayer());
        }

        private void _pauseMediaPlayer()
        {
            PauseRequested(this, EventArgs.Empty);
        }
        #endregion

        #region - public functions -

        public void StartMediaPlayer()
        {
            PlayRequested(this, EventArgs.Empty);
        }

        public void StopMediaPlayer()
        {
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
