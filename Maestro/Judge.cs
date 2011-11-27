using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maestro
{
    public class Judge
    {

        private List<Step> stepList {get;set;}
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
        public int getScore(int lHand,int rHand, int lFoot, int rFoot, int currentTime)
        {
            //Get all the actions at the current time
            List<Step> steps = currentTimeSteps(currentTime);

            Step currentStep;

            //Foreach step
            for (int i = 0; i < steps.Count; ++i)
            {
                currentStep = steps.ElementAt(i);

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

                                return EXCELLENTMARK;

                            }
                            //GOOD MARK
                            else
                            {
                                return GOODMARK;
                            }

                        }
                        //BAD MARK
                        else
                        {
                            return BADMARK;
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

                                    return EXCELLENTMARK;

                                }
                                //GOOD MARK
                                else
                                {
                                    return GOODMARK;
                                }

                            }
                            //BAD MARK
                            else
                            {
                                return BADMARK;
                            }

                        }
                    }
            }

            return 0;
        }

        //Get the steps at the current time
        private List<Step> currentTimeSteps(int currentTime)
        {
            int errorPlus = currentTime + ERRORMARGIN;
            int errorMinus = currentTime - ERRORMARGIN;

            List<Step> currentTimeSteps = new List<Step>();

            for (int i = 0; i < stepList.Count; ++i)
            {

                //If we are above the current time abort
                if (stepList.ElementAt(i).timing > currentTime)
                {
                    i = stepList.Count;
                }
                //Else check if the step occurs at the current time
                else
                    if (errorMinus < stepList.ElementAt(i).timing && stepList.ElementAt(i).timing < errorPlus)
                    {
                        currentTimeSteps.Add(stepList.ElementAt(i));
                    }
            }

            return currentTimeSteps;
        }
        
    }
}
