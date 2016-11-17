using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DialogEngine
{
    public class MP3Player
    {
        [DllImport("winmm.dll")]

        static extern int mciSendString(string mciCommand, StringBuilder buffer, int bufferSize, IntPtr callback);
        string fileName;
        public int Send(string mciCommand)
        {
            int returnCode = mciSendString(mciCommand, null, 0, IntPtr.Zero);
            return returnCode;
        }

   /*     static MP3Player()
        {
            
        }
        */
        public int Play(string fileLocation) {
            int returnCodePlay = 0;
            fileName = fileLocation;
            returnCodePlay = Send("play " + fileName);
            return returnCodePlay;
        }
    }
}