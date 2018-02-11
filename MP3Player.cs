﻿//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Events.DialogEvents;
using DialogEngine.Helpers;
using DialogEngine.Models.Logger;
using DialogEngine.ViewModels.Dialog;
using log4net;
using System;
using System.Reflection;
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

        private DispatcherTimer mTimer = new DispatcherTimer();
        private DispatcherTimer mVolumeTimer = new DispatcherTimer(); //ms

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


            Player.MediaOpened += Player_MediaOpened;
            Player.MediaFailed += Player_MediaFailed;

            mTimer.Interval = TimeSpan.FromSeconds(1);

            mVolumeTimer.Interval = TimeSpan.FromMilliseconds(100);
            mTimer.Tick += _timer_Tick;
            mVolumeTimer.Tick += _volumeTimer_Tick;
        }

        #endregion

        #region - event handlers -

        private void Player_BufferingEnded(object sender, EventArgs e)
        {
            mDuration = Player.NaturalDuration.TimeSpan.TotalSeconds;
        }


        private void Player_MediaOpened(object sender, EventArgs e)
        {
            mDuration = Player.NaturalDuration.TimeSpan.TotalSeconds;
        }


        private void Player_MediaFailed(object sender, ExceptionEventArgs e)
        {
            DialogViewModel.Instance.AddItem(new ErrorMessage("Media filed."));
        }


        private void _volumeTimer_Tick(object sender, EventArgs e)
        {
            mcLogger.Info("start volumeTimer_Tick");
            try
            {
                if (Player.Volume == 0)
                {
                    mVolumeTimer.Stop();

                    Player.Stop();

                    return;
                }
                else
                {
                    Player.Volume -= 0.2; // percentage
                }
            }
            catch (Exception ex)
            {
                mcLogger.Error("VolumeTimer. " + ex.Message);
            };

            mcLogger.Info("end volumeTimer_Tick");

        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            mcLogger.Info("start _timer_Tick");
            double _durationOfPlaying = DateTime.Now.TimeOfDay.TotalSeconds - mStartedTime.TotalSeconds;

            // 0.5 seconds we need to mute player
            if (_durationOfPlaying > (SessionVariables.MaxTimeToPlayFile - 0.5))
            {
                mTimer.Stop();

                mVolumeTimer.Start();
            }

            mcLogger.Info("end _timer_Tick");
        }

        #endregion

        #region - private functions -


        private void _stopPlayingCurrentDialogLine()
        {
            mTimer.Stop();
            mVolumeTimer.Stop();

            if (IsPlaying())
            {
                if (mDuration > SessionVariables.MaxTimeToPlayFile)
                {
                    mTimer.Start();
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
                mTimer.Stop();
                mVolumeTimer.Stop();

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