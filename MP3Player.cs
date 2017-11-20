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

        public WindowsMediaPlayerMp3()
        {
            Player = new WMPLib.WindowsMediaPlayer();
            Player.MediaError += Player_MediaError;

        }

        private void Player_MediaError(object pMediaObject)
        {
            WriteStatusBarInfo("Incorrect .mp3 file.", Brushes.Red);
        }

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

        public int Status()
        {
            int _code = 1000;
            try
            {
                _code = (int)Player.playState;
            }
            catch
            {
                Console.WriteLine("MP3 Player Status not readable");
            }
            return _code;
        }

        public void WriteStatusBarInfo(string _infoMessage, Brush _infoColor)
        {


            if (Application.Current.Dispatcher.CheckAccess())
            {

                try
                {
                    (Application.Current.MainWindow as MainWindow).WriteStatusInfo(_infoMessage, _infoColor);

                }
                catch (Exception e)
                {
                    mcLogger.Error(e.Message);
                }
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    try
                    {
                        (Application.Current.MainWindow as MainWindow).WriteStatusInfo(_infoMessage, _infoColor);

                    }
                    catch (Exception e)
                    {
                        mcLogger.Error(e.Message);
                    }
                }));

            }
        }
    }
}