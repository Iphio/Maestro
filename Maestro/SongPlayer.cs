using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maestro
{
    class SongPlayer
    {
        private List<String> _songList { get; set; }
        private String currSong;
        private Song song;
        public void Start_Song(String toPlay)
        {
            this.song = new Song(toPlay);
            this.song.PlaySong();
        }

    }
}
