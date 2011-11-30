using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Maestro
{
    public class Song
    {
        StringBuilder str = new StringBuilder(128);
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string command, StringBuilder str, int iReturnLength, IntPtr hwndcallback);

        private String Title;
        private int Length;
        private String Artist;
        private String Filepath;//should be fixed
        public List<Step> _listOfSteps { get; set; }


        public Song(String SongTitle)
        {
            this.Title = SongTitle;
            mciSendString("status \""+this.Title+"\" length", str, 0, IntPtr.Zero);
            this.Length = int.Parse(str.ToString());
            this.Artist = "";
            this.Filepath = "";//should be fixed. may not even need it
            _listOfSteps = Parser.loadStep();
        }

        public void PlaySong(int vol)
        {
            mciSendString("play \""+this.Title+"\"", null, 0, IntPtr.Zero);
            mciSendString("setaudio \"" + this.Title + "\" volume to vol", str, 0, IntPtr.Zero);
        }
        public int getCurrentMillisecond()
        {
            mciSendString("Status \"" + this.Title + "\" position", str, 0, IntPtr.Zero);
            int time = int.Parse(str.ToString());
            return time;
        }
    }
}
