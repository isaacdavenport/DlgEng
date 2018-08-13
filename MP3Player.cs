//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Events.DialogEvents;
using DialogEngine.Helpers;
using DialogEngine.Models.Logger;
using log4net;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace DialogEngine
{
    /// <summary>
    /// Wrapper for <see cref="MediaPlayer"/>
    /// Used for playing dialog's .mp3 files
    /// </summary>
    public class MP3Player
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly object mcPadlock = new object();
        private static MP3Player msInstance = null;
        // length of .mp3 file in seconds
        private double mDuration;
        private bool mIsLoaded;
        private bool mIsPlayingStopped;
        // started time of playing .mp3 file
        private TimeSpan mStartedTime;
        private Timer mTimer;
        private Timer mVolumeTimer;
        // wpf media player
        public MediaPlayer Player = new MediaPlayer();

        #endregion

        #region - constructor -

        /// <summary>
        /// Creates instance of MP3Player
        /// </summary>
        public MP3Player()
        {
            Events.EventAggregator.Instance.GetEvent<StopPlayingCurrentDialogLineEvent>().Subscribe(_stopPlayingCurrentDialogLine);
            Events.EventAggregator.Instance.GetEvent<StopImmediatelyPlayingCurrentDialogLIne>().Subscribe(_stopImmediatelyPlayingCurrentDialogLine);

            mVolumeTimer= new Timer(_volumeTimerElapsed, null, Timeout.Infinite, Timeout.Infinite);
            mTimer = new Timer(_timerElapsed, null,Timeout.Infinite, Timeout.Infinite);

            Player.MediaOpened += _player_MediaOpened;
            Player.MediaFailed += _player_MediaFailed;
        }


        #endregion

        #region - event handlers -

        private  void _timerElapsed(object state)
        {
            double _durationOfPlaying = DateTime.Now.TimeOfDay.TotalSeconds - mStartedTime.TotalSeconds;

            // 0.5 seconds we need to mute player
            if (_durationOfPlaying > (SessionHelper.MaxTimeToPlayFile - 0.5))
            {
                mTimer.Change(Timeout.Infinite, Timeout.Infinite);
                mVolumeTimer.Change(0, 100);
            }
        }


        private  void _volumeTimerElapsed(object state)
        {
            try
            {
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (Player.Volume == 0)
                    {
                        mVolumeTimer.Change(Timeout.Infinite,Timeout.Infinite);
                        Player.Stop();
                        mIsPlayingStopped = true;
                        return;
                    }
                    else
                    {
                        Player.Volume -= 0.2; // percentage
                    }
                }, DispatcherPriority.Send);
            }
            catch (Exception ex)
            {
                mcLogger.Error("VolumeTimer. " + ex.Message);
            };
        }


        private void _player_MediaOpened(object sender, EventArgs e)
        {
            if (Player.NaturalDuration.HasTimeSpan)
            {
                mDuration = Player.NaturalDuration.TimeSpan.TotalSeconds;
            }
            else
            {
                Debug.WriteLine("Automatic duration automatic");
            }

            Debug.WriteLine("loaded + " + mDuration);
            mIsLoaded = true;
        }


        private void _player_MediaFailed(object sender, ExceptionEventArgs e)
        {
            DialogDataHelper.AddMessage(new ErrorMessage("Media filed."));
        }

        #endregion

        #region - private functions -

        private void _stopPlayingCurrentDialogLine()
        {
            mTimer.Change(Timeout.Infinite,Timeout.Infinite);
            mVolumeTimer.Change(Timeout.Infinite,Timeout.Infinite);

            if (IsPlaying())
            {
                if (mDuration > SessionHelper.MaxTimeToPlayFile)
                {
                    mTimer.Change(0,1000);
                }
            }
        }

        // when this function is inveoked, we stops player without any condition
        private void _stopImmediatelyPlayingCurrentDialogLine()
        {
            if (IsPlaying())
            {
                try
                {
                    Player.Stop();
                    mIsPlayingStopped = true;
                }
                catch (Exception ex)
                {
                    mcLogger.Error("StopImmediatelyPlayingCurrentDialogLine error. Message: " + ex.Message);
                }
            }
        }

        #endregion

        #region - public functions -

        /// <summary>
        /// Starts with .mp3 file
        /// </summary>
        /// <param name="_path">Path to .mp3 file</param>
        /// <returns>
        /// 0 - player successfully started .mp3 file
        /// 1 - error with starting .mp3 file 
        /// </returns>
        public int PlayMp3(string _path)
        {
            try
            {
                mIsPlayingStopped = false;
                mIsLoaded = false;
                mTimer.Change(Timeout.Infinite, Timeout.Infinite);
                mVolumeTimer.Change(Timeout.Infinite, Timeout.Infinite);

                Player.Volume = 0.8;
                Player.Open(new Uri(_path));
                Player.Play();

                mcLogger.Debug(_path);
                mStartedTime = DateTime.Now.TimeOfDay;
                return 0;  //TODO add error handling    
            }
            catch (Exception ex)
            {
                //player is busy
                mcLogger.Error("PlayMp3 error. " + ex.Message);
                return 1;
            }
        }

        /// <summary>
        /// Check is player playing
        /// </summary>
        /// <returns>Is player playing</returns>
        public bool IsPlaying()
        {
            try
            {
                Debug.WriteLine(Player.Position.TotalSeconds);
                if (mIsLoaded)
                {
                    return Player.Position.TotalSeconds < mDuration
                           && !mIsPlayingStopped;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                mcLogger.Error("IsPlaying " + ex.Message);
                // application is busy
                return true;
            }
        }

        #endregion

        #region - properties -

        /// <summary>
        /// Instance of MP3Player - Singleton
        /// </summary>
        public static MP3Player Instance
        {
            get
            {
                lock (mcPadlock)
                {
                    if (msInstance == null)
                    {
                        msInstance = new MP3Player();
                    }
                    return msInstance;
                }
            }
        }

        #endregion
    }
}