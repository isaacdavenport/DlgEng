//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Events.DialogEvents;
using DialogEngine.Helpers;
using DialogEngine.Models.Logger;
using DialogEngine.ViewModels.Dialog;
using log4net;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace DialogEngine
{

    public class MP3Player
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly object mcPadlock = new object();
        private static MP3Player msInstance = null;

        // started time of playing .mp3 file
        private TimeSpan mStartedTime;

        // length of .mp3 file in s
        private double mDuration;

        private Timer mTimer;

        private Timer mVolumeTimer;

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

            Player.MediaOpened += Player_MediaOpened;
            Player.MediaFailed += Player_MediaFailed;

        }

        #endregion

        #region - event handlers -

        private  void _timerElapsed(object state)
        {

                mcLogger.Info("start _timer_Tick");

                double _durationOfPlaying = DateTime.Now.TimeOfDay.TotalSeconds - mStartedTime.TotalSeconds;

                // 0.5 seconds we need to mute player
                if (_durationOfPlaying > (SessionVariables.MaxTimeToPlayFile - 0.5))
                {
                    mTimer.Change(Timeout.Infinite,Timeout.Infinite);

                    mVolumeTimer.Change(0,100);
                }


        }


        private  void _volumeTimerElapsed(object state)
        {
            mcLogger.Info("start volumeTimer_Tick");

            try
            {
                Application.Current.Dispatcher.BeginInvoke(() =>
                {

                    if (Player.Volume == 0)
                    {
                        mVolumeTimer.Change(Timeout.Infinite,Timeout.Infinite);

                        Player.Stop();

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

            mcLogger.Info("end volumeTimer_Tick");

        }


        private void Player_BufferingEnded(object sender, EventArgs e)
        {
            mDuration = Player.NaturalDuration.TimeSpan.TotalSeconds;
        }


        private void Player_MediaOpened(object sender, EventArgs e)
        {
            if(Player.NaturalDuration.HasTimeSpan)
            mDuration = Player.NaturalDuration.TimeSpan.TotalSeconds;
        }


        private void Player_MediaFailed(object sender, ExceptionEventArgs e)
        {
            DialogViewModel.Instance.AddItem(new ErrorMessage("Media filed."));
        }


        #endregion

        #region - private functions -


        private void _stopPlayingCurrentDialogLine()
        {
            mTimer.Change(Timeout.Infinite,Timeout.Infinite);
            mVolumeTimer.Change(Timeout.Infinite,Timeout.Infinite);

            if (IsPlaying())
            {
                if (mDuration > SessionVariables.MaxTimeToPlayFile)
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
                }
                catch (Exception ex)
                {
                    mcLogger.Error("StopImmediatelyPlayingCurrentDialogLine error. Message: " + ex.Message);
                }
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
                mTimer.Change(Timeout.Infinite,Timeout.Infinite);
                mVolumeTimer.Change(Timeout.Infinite,Timeout.Infinite);

                Player.Volume = 0.8;
                Player.Open(new Uri(_path));
                //mDuration = Player.NaturalDuration.TimeSpan.TotalSeconds;

                Player.Play();

                mcLogger.Debug("Current .mp3 file " + _path );

                mStartedTime = DateTime.Now.TimeOfDay;

                return 0;  //TODO add error handling    
            }
            catch(Exception ex)
            {
                //player is busy
                mcLogger.Error("PlayMp3 error. "+ ex.Message);
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
                double position = Player.Position.TotalSeconds;

                if( position == 0)
                {
                    return false;
                }

                return Player.NaturalDuration.TimeSpan.TotalSeconds != Player.Position.TotalSeconds;
            }
            catch(Exception ex)
            {
                mcLogger.Error("IsPlaying " + ex.Message);
                // application is busy
                return true;
            }
        }

        #endregion
    }
}