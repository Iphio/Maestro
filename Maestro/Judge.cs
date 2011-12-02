using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maestro
{
    public class Judge
    {

        private List<Step> stepList { get; set; }

        private Difficulty selectedDifficulty;

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

        //Difficulty selection
        public void selectDifficulty(Difficulty dif)
        {
            selectedDifficulty = dif;
        }

        //Returns the score of the current frame
        public int getScore(int lHand, int rHand, int lFoot, int rFoot, int currentTime)
        {
            Step currentStep;

            int score = 0;

            //Foreach step
            for (int i = lastIndex; i < stepList.Count; ++i)
            {
                currentStep = stepList.ElementAt(i);

                if (currentTime > currentStep.timing + ERRORMARGIN)
                    return score;

                //If the step is valid
                if (!currentStep.done && currentTime - ERRORMARGIN < currentStep.timing && currentStep.timing < currentTime + ERRORMARGIN)
                {
                    lastIndex = i;

                    

                    //If touch hand
                    if (currentStep.action == ActionType.TouchHand && (lHand == currentStep.area || rHand == currentStep.area))
                    {
                        //Check the timing
                        if (currentStep.timing - BADMARGIN < currentTime && currentTime < currentStep.timing + BADMARGIN)
                        {

                            if (currentStep.timing - GOODMARGIN < currentTime && currentTime < currentStep.timing + GOODMARGIN)
                            {

                                if (currentStep.timing - EXCELLENTMARGIN < currentTime && currentTime < currentStep.timing + EXCELLENTMARGIN)
                                {

                                    score += EXCELLENTMARK;

                                }
                                //GOOD MARK
                                else
                                {
                                    score += GOODMARK;
                                }

                            }
                            //BAD MARK
                            else
                            {
                                score += BADMARK;
                            }

                        }
                    }
                    else //If touch foot
                        if (currentStep.action == ActionType.TouchFeet && (lFoot == currentStep.area || rFoot == currentStep.area))
                        {
                            //Check the timing
                            if (currentStep.timing - BADMARGIN < currentTime && currentTime < currentStep.timing + BADMARGIN)
                            {

                                if (currentStep.timing - GOODMARGIN < currentTime && currentTime < currentStep.timing + GOODMARGIN)
                                {

                                    if (currentStep.timing - EXCELLENTMARGIN < currentTime && currentTime < currentStep.timing + EXCELLENTMARGIN)
                                    {

                                        score += EXCELLENTMARK;

                                    }
                                    //GOOD MARK
                                    else
                                    {
                                        score += GOODMARK;
                                    }

                                }
                                //BAD MARK
                                else
                                {
                                    score += BADMARK;
                                }

                            }
                        }
                }

            }

            return score;
        }
    }
}
