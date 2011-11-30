﻿using System;
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

        public String Title{get;set;}
        public int Length { get; set; }
        public String Artist { get; set; }
        public String Filepath { get; set; }//should be fixed

        [XmlArrayItem(typeof(Step))]
        public List<Step> _listOfSteps { get; set; }

        public Song()
        {

        }

        public Song(String SongTitle)
        {
            this.Title = SongTitle;
            mciSendString("status \""+this.Title+"\" length", str, 128, IntPtr.Zero);
            this.Length = int.Parse(str.ToString());
            this.Artist = "TEST";
            this.Filepath = "C:\\TEST";//should be fixed. may not even need it

            _listOfSteps = new List<Step>();


            //CREATE A SONG HERE ! (don't forget to delete once it's done....)
            _listOfSteps.Add(new Step(2000, Difficulty.Easy, 5, ActionType.Push));
            _listOfSteps.Add(new Step(2050, Difficulty.Easy, 1, ActionType.Push));
            _listOfSteps.Add(new Step(5000, Difficulty.Easy, 7, ActionType.Push));

        }

        public void PlaySong(int vol)
        {
            mciSendString("play \""+this.Title+"\"", null, 0, IntPtr.Zero);
            mciSendString("setaudio \"" + this.Title + "\" volume to "+vol, str, 0, IntPtr.Zero);
        }

        public int getCurrentMillisecond()
        {
            mciSendString("Status \"" + this.Title + "\" position", str, 0, IntPtr.Zero);
            int time = int.Parse(str.ToString());
            return time;
        }

        public void pause()
        {

        }

        public void resume()
        {

        }
    }
}
