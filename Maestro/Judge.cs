using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Maestro
{
    public class Judge
    {
        private List<Step> _stepList { get; set; }

        public Difficulty _selectedDifficulty { get; set; }

        #region constants
        const int ERRORMARGIN = 1001;
        const int BADMARGIN = 600;
        const int GOODMARGIN = 400;
        const int EXCELLENTMARGIN = 200;

        const int BADMARK = 1;
        const int GOODMARK = 2;
        const int EXCELLENTMARK = 3;
        #endregion

        //Last judged note
        private int _lastIndex;

        public Judge()
        {
            _lastIndex = 0;
        }

        public void updateSteps(List<Step> listOfSteps)
        {
            _stepList = listOfSteps;
        }

        //Difficulty selection
        public void selectDifficulty(Difficulty dif)
        {
            _selectedDifficulty = dif;
        }

        //Returns the score of the current frame
        public int getScore(int lHand, int rHand, int lFoot, int rFoot, int currentTime, Point lHandP, Point rHandP, int[] scoreTable)
        {
            Step currentStep;

            int score = 0;
            
            //Foreach step
            for (int i = _lastIndex; i < _stepList.Count; ++i)
            {
                currentStep = _stepList.ElementAt(i);

                if (currentTime > currentStep.timing + ERRORMARGIN)
                {
                   // return score;
                }
                //Hold hand case
                else if (!currentStep.done && currentStep.action == ActionType.HoldHand && currentStep.stepDifficulty == _selectedDifficulty && currentStep.timing + currentStep.holdTime <= currentTime)
                {
                    //If we reach the end of the hold
                    if (currentTime == (currentStep.timing + currentStep.holdTime))
                    {
                        currentStep.step_Done();
                    }

                    if (currentStep.area == lHand || currentStep.area == rHand)
                        score++;
                }
                //Hold foot case
                else if (!currentStep.done && currentStep.action == ActionType.HoldFoot && currentStep.stepDifficulty == _selectedDifficulty && currentStep.timing + currentStep.holdTime <= currentTime)
                {
                    //If we reach the end of the hold
                    if (currentTime == (currentStep.timing + currentStep.holdTime))
                    {
                        currentStep.step_Done();
                    }

                    if (currentStep.area == lFoot || currentStep.area == rFoot)
                        score++;
                }
                //Clap case
                else if (!currentStep.done && currentStep.action == ActionType.Clap && currentStep.stepDifficulty == _selectedDifficulty && currentTime - ERRORMARGIN < currentStep.timing && currentStep.timing < currentTime + ERRORMARGIN)
                {

                    //Check the distance between the hands
                    if (Math.Sqrt(Math.Pow((lHandP.X - rHandP.X), 2) + Math.Pow((lHandP.Y - rHandP.Y), 2)) < 30)
                    {
                        score = evaluate(currentTime, currentStep, score, scoreTable);
                        currentStep.step_Done();
                        _lastIndex = i;

                    }

                }
                //Touch case
                else if (!currentStep.done && currentStep.stepDifficulty == _selectedDifficulty && currentTime - ERRORMARGIN < currentStep.timing && currentStep.timing < currentTime + ERRORMARGIN)
                {
                    //If touch left hand
                    if (currentStep.action == ActionType.TouchHandLeft && (lHand == currentStep.area))
                    {
                        //Current step is done
                        _lastIndex = i;
                        currentStep.step_Done();
                        score = evaluate(currentTime, currentStep, score, scoreTable);
                    }
                    //If touch left foot
                    else if (currentStep.action == ActionType.TouchFeetLeft && (lFoot == currentStep.area))
                    {
                        //Current step is done
                        _lastIndex = i;
                        currentStep.step_Done();
                        score = evaluate(currentTime, currentStep, score, scoreTable);
                    }
                    //If touch right hand
                    else if (currentStep.action == ActionType.TouchHandRight && rHand == currentStep.area)
                    {
                        //Current step is done
                        _lastIndex = i;
                        currentStep.step_Done();
                        score = evaluate(currentTime, currentStep, score, scoreTable);
                    }
                    else if (currentStep.action == ActionType.TouchFeetRight && rFoot == currentStep.area)
                    {

                        //Current step is done
                        _lastIndex = i;
                        currentStep.step_Done();
                        score = evaluate(currentTime, currentStep, score, scoreTable);
                    }
                }
            }
            return score;
        }

        private static int evaluate(int currentTime, Step currentStep, int frameScore, int[] scoreTable)
        {
            //Check the timing
            if (currentStep.timing - BADMARGIN < currentTime && currentTime < currentStep.timing + BADMARGIN)
            {
                if (currentStep.timing - GOODMARGIN < currentTime && currentTime < currentStep.timing + GOODMARGIN)
                {
                    if (currentStep.timing - EXCELLENTMARGIN < currentTime && currentTime < currentStep.timing + EXCELLENTMARGIN)
                    {
                        frameScore += EXCELLENTMARK;
                        scoreTable[0]++;
                        currentStep.done = true;
                    }
                    //GOOD MARK
                    else
                    {
                        frameScore += GOODMARK;
                        scoreTable[1]++;
                        currentStep.done = true;
                    }
                }
                //BAD MARK
                else
                {
                    frameScore += BADMARK;
                    scoreTable[2]++;
                    currentStep.done = true;
                }
            }
            return frameScore;
        }
    }
}
