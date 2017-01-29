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

        public int PlayMp3(string path) {
            Player.URL = path;
            Player.controls.play();
            return 0;  //TODO add error handling
        }

        public bool IsPlaying() {
            WMPPlayState currentPlayState = Player.playState;

            if (currentPlayState == WMPPlayState.wmppsPlaying || currentPlayState == WMPPlayState.wmppsBuffering 
                    || currentPlayState == WMPPlayState.wmppsTransitioning) {
                return true;
            }
            else {
                return false;
            }
        }

        public int Status() {
            int code = 1000;
            try {
                code = (int)Player.playState;
            }
            catch {
               Console.WriteLine("MP3 Player Status not readable"); 
            }
            return code;
        }

    }
}