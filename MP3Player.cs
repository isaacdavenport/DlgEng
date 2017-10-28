using System;
using WMPLib;

namespace DialogEngine
{
    public class WindowsMediaPlayerMp3
    {
        public WMPLib.WindowsMediaPlayer Player;
        public WindowsMediaPlayerMp3() {
             Player = new WMPLib.WindowsMediaPlayer();
        }

        public int PlayMp3(string _path) {
            Player.URL = _path;
            Player.controls.play();
            return 0;  //TODO add error handling    
        }

        public bool IsPlaying() {
            WMPPlayState _currentPlayState = Player.playState;

            if (_currentPlayState == WMPPlayState.wmppsPlaying || _currentPlayState == WMPPlayState.wmppsBuffering 
                    || _currentPlayState == WMPPlayState.wmppsTransitioning) {
                return true;
            }
            return false;
        }

        public int Status() {
            int _code = 1000;
            try {
                _code = (int)Player.playState;
            }
            catch {
               Console.WriteLine("MP3 Player Status not readable"); 
            }
            return _code;
        }

    }
}