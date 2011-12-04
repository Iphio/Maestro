using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maestro
{
    

    public class Profile
    {
        //Attributes
        public int highScore { get; set; }
        public String name { get; set; }

        public Profile()
        {
        }

        public Profile(String name)
        {
            this.name = name;
        }
        public Profile(String name, int score)
        {
            this.name = name;
            highScore = score;
        }
        public void updateScore(int newScore)
        {
            highScore = newScore;
        }


    }


}
