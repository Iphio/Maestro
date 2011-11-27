using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maestro
{
    public class Judge
    {

        private List<Step> stepList { get; set; }
        private int lastIndex;
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
            //Get all the actions at the current time
            List<Step> steps = currentTimeSteps(currentTime);

            Step currentStep;

            int score = 0;

            //Foreach step
            for (int i = 0; i < stepList.Count; ++i)
            {
                currentStep = steps.ElementAt(i);

                //If the step is valid
                if (currentStep.timing - ERRORMARGIN < currentStep.timing && currentStep.timing < currentStep.timing + ERRORMARGIN)
                {
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
                    else //If touch hand
                        if (currentStep.action == ActionType.TouchFeet && (lFoot == currentStep.area || lFoot == currentStep.area))
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

        //Get the steps at the current time
        private List<Step> currentTimeSteps(int currentTime)
        {
            return null;
        }

    }
}
