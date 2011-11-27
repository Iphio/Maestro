using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Maestro
{
    class Song
    {
        [DllImport("winmm.dll")]
        private extern int mciSendString(string command, StringBuilder str, int iReturnLength, IntPtr hwndcallback);

        private String Title;
        private int Length;
        private String Artist;
        private String Filepath;//should be fixed
        public List<Step> _listOfSteps { get; set; }


        public Song(String SongTitle)
        {
            this.Title = SongTitle;
            this.Length = mciSendString("status MediaFile length", null, 0, IntPtr.Zero);
            this.Artist = "";
            this.Filepath = "";//should be fixed. may not even need it
            _listOfSteps = Parser.loadStep();
        }

        public void PlaySong()
        {
            mciSendString("play \""+this.Title+".mp3\"", null, 0, IntPtr.Zero);
        }
        public int getCurrentMillisecond()
        {
            int time = mciSendString("Status MediaFile position", null, 0, IntPtr.Zero);
            return time;
        }
    }
}
