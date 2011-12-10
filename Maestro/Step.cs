using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maestro
{
    //Event state change
    public delegate void StateChangedEventHandler(Object source);

    public class Step
    {        

        //This is a test for Step.cs
        public int timing { get; set; }
        public Difficulty stepDifficulty { get; set; }
        public int area { get; set; }
        public ActionType action { get; set; }
        public int holdTime { get; set; }

        //Change the state of the step
        public event StateChangedEventHandler stepDone; 

        public bool done { get; set; }

        public bool scored { get; set; }

        public Step()
        {
            timing = 0;
            stepDifficulty = Difficulty.Easy;
            area = 0;
            action = ActionType.TouchHandLeft;
            done = false;

            scored = false;

        }

        public Step(int timing, Difficulty dif, int area, ActionType action, int holdTime)
        {
            this.holdTime = holdTime;
            done = false;
            this.timing = timing;
            stepDifficulty = dif;
            this.area = area;
            this.action = action;

            scored = false;
        }

        public void step_Done(){

            done = true;

            if (stepDone != null)
            {
                stepDone(this);
            }


        }


    }
}
