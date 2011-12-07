using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maestro
{
    public class Step
    {
	
	//This is a test for Step.cs
        public int timing { get; set; }
        public Difficulty stepDifficulty {get; set;}
        public int area { get; set; }
        public ActionType action { get; set; }
        public int holdTime { get; set; }

        public bool done { get; set; }

        public Step()
        {
            timing = 0;
            stepDifficulty = Difficulty.Easy;
            area = 0;
            action = ActionType.TouchHandLeft;
            done = false;

            
        }

        public Step(int timing, Difficulty dif, int area, ActionType action, int holdTime)
        {
            this.holdTime = holdTime;
            done = false;
            this.timing = timing;
            stepDifficulty = dif;
            this.area = area;
            this.action = action;

        }


    }
}
