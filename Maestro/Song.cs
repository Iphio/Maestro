using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Maestro
{
    public class Song
    {
        StringBuilder str = new StringBuilder(128);
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string command, StringBuilder str, int iReturnLength, IntPtr hwndcallback);

        public String Title { get; set; }
        public int Length { get; set; }
        public String Artist { get; set; }
        public String Filepath { get; set; }//should be fixed

        [XmlArrayItem(typeof(Step))]
        public List<Step> _listOfSteps { get; set; }

        public Song()
        {

        }
        public Song(String SongTitle, String artist)
        {
            this.Title = SongTitle;
            this.Artist = artist;
            mciSendString("status \"songs\\" + this.Title + "\" length", str, 128, IntPtr.Zero);
            this.Length = int.Parse(str.ToString());
            //this.Artist = "TEST";
            //this.Filepath = "C:\\TEST";//should be fixed. may not even need it

            _listOfSteps = new List<Step>();

        }

        public void PlaySong(int vol)
        {
            mciSendString("play \"songs\\" + this.Title + "\"", null, 0, IntPtr.Zero);
            mciSendString("setaudio \"songs\\" + this.Title + "\" volume to " + vol, str, 0, IntPtr.Zero);
        }

        public int getCurrentMillisecond()
        {
            mciSendString("Status \"songs\\" + this.Title + "\" position", str, 128, IntPtr.Zero);
            int time = Convert.ToInt32(str.ToString());
            return time;
        }

        public void pause()
        {
            mciSendString("pause \"songs\\" + this.Title + "\"", str, 0, IntPtr.Zero);
        }

        public void resume()
        {
            mciSendString("resume \"songs\\" + this.Title + "\"", str, 0, IntPtr.Zero);
        }
        public int length()
        {
            mciSendString("Status \"songs\\" + this.Title + "\" length", str, 128, IntPtr.Zero);
            int time = Convert.ToInt32(str.ToString());
            return time;
        }
    }
}
