//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Events.DialogEvents;
using log4net;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media;
using WMPLib;

namespace DialogEngine
{
    public class WindowsMediaPlayerMp3
    {
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public WMPLib.WindowsMediaPlayer Player;

        /// <summary>
        /// Creates instance of WindowsMediaPlayerMp3
        /// </summary>
        public WindowsMediaPlayerMp3()
        {
            Player = new WMPLib.WindowsMediaPlayer();
            Player.MediaError += Player_MediaError;
            Events.EventAggregator.Instance.GetEvent<StopPlayingCurrentDialogLineEvent>().Subscribe(StopPlayingCurrentDialogLine);

        }




        private void Player_MediaError(object pMediaObject)
        {
            mcLogger.Error("Incorrect .mp3 file.");
        }


        /// <summary>
        /// 
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
                Player.URL = _path;
                Player.controls.play();
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

        /// <summary>
        /// Stops player
        /// </summary>
        public void StopPlayingCurrentDialogLine()
        {
            try
            {
                Player.controls.stop();
            }
            catch (Exception ex)
            {
                mcLogger.Error(ex.Message);
            }
        }

    }
}