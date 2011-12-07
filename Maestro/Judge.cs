using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maestro
{
    public class Judge
    {
        private List<Step> stepList { get; set; }

        public Difficulty selectedDifficulty { get; set; }

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
        private int lastIndex;

        public Judge()
        {
            lastIndex = 0;
        }

        public void updateSteps(List<Step> listOfSteps)
        {
            stepList = listOfSteps;
        }

        //Difficulty selection
        public void selectDifficulty(Difficulty dif)
        {
            selectedDifficulty = dif;
        }

        //Returns the score of the current frame
        public int getScore(int lHand, int rHand, int lFoot, int rFoot, int currentTime, int []scoreTable)
        {
            Step currentStep;

            int score = 0;

            //Foreach step
            for (int i = lastIndex; i < stepList.Count; ++i)
            {
                currentStep = stepList.ElementAt(i);

                if (currentTime > currentStep.timing + ERRORMARGIN)
                {

                }
                else

                    //If the step is valid
                    if (!currentStep.done && currentStep.stepDifficulty == selectedDifficulty && !currentStep.done && currentTime - ERRORMARGIN < currentStep.timing && currentStep.timing < currentTime + ERRORMARGIN)
                    {
                        //If touch left hand
                        if (currentStep.action == ActionType.TouchHandLeft && (lHand == currentStep.area))
                        {
                            //Current step is done
                            lastIndex = i;
                            currentStep.done = true;
                            score = evaluate(currentTime, currentStep, score, scoreTable);
                        }
                        //If touch left foot
                        else if (currentStep.action == ActionType.TouchFeetLeft && (lFoot == currentStep.area))
                        {
                            //Current step is done
                            lastIndex = i;
                            currentStep.done = true;
                            score = evaluate(currentTime, currentStep, score, scoreTable);
                        }
                        //If touch right hand
                        else if (currentStep.action == ActionType.TouchHandRight && rHand == currentStep.area)
                        {
                            //Current step is done
                            lastIndex = i;
                            currentStep.done = true;
                            score = evaluate(currentTime, currentStep, score, scoreTable);
                        }
                        else if (currentStep.action == ActionType.TouchFeetRight && rFoot == currentStep.area)
                        {

                            //Current step is done
                            lastIndex = i;
                            currentStep.done = true;
                            score = evaluate(currentTime, currentStep, score, scoreTable);
                        }
                        //If it's a drag action
                        else if (currentStep.action == ActionType.HoldHand && (rHand == currentStep.area || lHand == currentStep.area))
                        {
                            score++;
                        }
                        else if (currentStep.action == ActionType.HoldFoot && (rFoot == currentStep.area || lFoot == currentStep.area))
                        {
                            score++;
                        }
                    }
            }
            return score;
        }

        private static int evaluate(int currentTime, Step currentStep, int score,int[] scoreTable)
        {
            //Check the timing
            if (currentStep.timing - BADMARGIN < currentTime && currentTime < currentStep.timing + BADMARGIN)
            {
                if (currentStep.timing - GOODMARGIN < currentTime && currentTime < currentStep.timing + GOODMARGIN)
                {
                    if (currentStep.timing - EXCELLENTMARGIN < currentTime && currentTime < currentStep.timing + EXCELLENTMARGIN)
                    {
                        score += EXCELLENTMARK;
                        scoreTable[0]++;
                    }
                    //GOOD MARK
                    else
                    {
                        score += GOODMARK;
                        scoreTable[1]++;
                    }
                }
                //BAD MARK
                else
                {
                    score += BADMARK;
                    scoreTable[2]++;
                }
            }
            return score;
        }
    }
}
