//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Events.DialogEvents;
using DialogEngine.Helpers;
using log4net;
using System;
using System.Reflection;
using System.Timers;
using WMPLib;

namespace DialogEngine
{
    public class MP3Player
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly object mcPadlock = new object();
        private static MP3Player msInstance = null;

        private TimeSpan mStartedTime;
        private double mDuration;
        private Timer mTimer = new Timer(1000);
        private Timer mVolumeTimer = new Timer(200); //ms

        public WMPLib.WindowsMediaPlayer Player;

        #endregion

        #region - constructor -

        /// <summary>
        /// Creates instance of MP3Player
        /// </summary>
        public MP3Player()
        {
            Events.EventAggregator.Instance.GetEvent<StopPlayingCurrentDialogLineEvent>().Subscribe(_stopPlayingCurrentDialogLine);
            Events.EventAggregator.Instance.GetEvent<StopImmediatelyPlayingCurrentDialogLIne>().Subscribe(_stopImmediatelyPlayingCurrentDialogLine);

            Player = new WMPLib.WindowsMediaPlayer();

            Player.MediaError += _player_MediaError;
            Player.PlayStateChange += _playState_Change;

            mTimer.Elapsed += _timer_Tick;
            mVolumeTimer.Elapsed += _volumeTimer_Tick;

        }

        #endregion

        #region - event handlers -

        private void _playState_Change(int NewState)
        {
            // state 3 - playing
            if(NewState == 3)
            mDuration = Player.currentMedia.duration;
        }


        private void _player_MediaError(object pMediaObject)
        {
            mcLogger.Error("Incorrect .mp3 file.");
        }

        #endregion

        #region - private functions -


        private void _volumeTimer_Tick(object sender, EventArgs e)
        {
            if (Player.settings.volume == 0)
            {
                mVolumeTimer.Stop();

                Player.controls.stop();

                return;
            }
            else
            {
                Player.settings.volume -= 10; // percentage
            }
        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            double _durationOfPlaying = DateTime.Now.TimeOfDay.TotalSeconds - mStartedTime.TotalSeconds;

            // 2 seconds we need to mute player 200 ms for  10 %
            if (_durationOfPlaying > (SessionVariables.MaxTimeToPlayFile - 2))
            {
                mTimer.Stop();

                mVolumeTimer.Start();
            }

        }

        private void _stopPlayingCurrentDialogLine()
        {
            if (IsPlaying())
            {

                if (mDuration > SessionVariables.MaxTimeToPlayFile)
                {
                    mTimer.Start();
                }
            }
        }

        
        private void _stopImmediatelyPlayingCurrentDialogLine()
        {
            if (IsPlaying())
            {
                try
                {
                    Player.controls.stop();
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

                Player.settings.volume = 100;
                Player.URL = _path;

                mStartedTime = DateTime.Now.TimeOfDay;

                return 0;  //TODO add error handling    
            }
            catch(Exception ex)
            {
                //player is busy
                mcLogger.Error(ex.Message);
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
                WMPPlayState _currentPlayState = Player.playState;

                if (_currentPlayState == WMPPlayState.wmppsPlaying || _currentPlayState == WMPPlayState.wmppsBuffering
                        || _currentPlayState == WMPPlayState.wmppsTransitioning)
                {
                    return true;
                }
                return false;
            }
            catch(Exception _ex)
            {
                // application is busy
                return true;
            }
        }


        /// <summary>
        /// Check status of player
        /// </summary>
        /// <returns>Status of mp3 player</returns>
        public int Status()
        {
            int _code = 1000;

            try
            {
                _code = (int)Player.playState;
            }
            catch
            {
                mcLogger.Error("MP3 Player Status not readable");
            }
            return _code;
        }

        #endregion
    }
}