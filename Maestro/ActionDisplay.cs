using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Maestro
{
    class ActionDisplay
    {
        const int DISPLAYMARGIN = 1001;

        private Canvas gameScreen { get; set; }
        private List<Step> stepList { get; set; }
        public double columnSpace {get;set;}
        public double rowSpace {get;set;}

        //Last displayed index
        private int lastIndex;

        public ActionDisplay()
        {

        }

        public ActionDisplay(Canvas gameScreen)
        {
            this.gameScreen = gameScreen;

            lastIndex = 0;

            //Divide the screen into 3 parts
            columnSpace = gameScreen.Width / 3.0;
            rowSpace = gameScreen.Height / 3.0;
        }

        public void loadSteps(List<Step> stepList)
        {
            this.stepList = stepList;
        }

        public void displayStep(int currentTime)
        {

            Step currentStep;
            gameScreen.Children.Clear();



            //Foreach step
            for (int i = lastIndex; i < stepList.Count; ++i)
            {
                currentStep = stepList.ElementAt(i);

                //If the step is valid                
                if (currentStep.done == false &&  currentTime - DISPLAYMARGIN <= currentStep.timing && currentStep.timing <= currentTime + DISPLAYMARGIN)
                {
                    //Store as last valid index
                    lastIndex = i;

                    //Create the visual component
                    Ellipse circle = new Ellipse();
                    circle.Stroke = Brushes.Gold;
                    circle.StrokeThickness = 15;
                    circle.Fill = Brushes.Red;

                    //Display
                    switch (currentStep.area)
                    {
                        case 0:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 2, rowSpace / 2);
                            break;

                        case 1:
                            circle.RenderTransform = new TranslateTransform(columnSpace + columnSpace / 2, rowSpace / 2);
                            break;

                        case 2:
                            circle.RenderTransform = new TranslateTransform(2 * columnSpace + columnSpace / 2, rowSpace / 2);
                            break;

                        case 3:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 2, rowSpace + rowSpace / 2);
                            break;

                        case 4:
                            circle.RenderTransform = new TranslateTransform(columnSpace + columnSpace / 2, rowSpace + rowSpace / 2);
                            break;

                        case 5:
                            circle.RenderTransform = new TranslateTransform(2 * columnSpace + columnSpace / 2, rowSpace + rowSpace / 2);
                            break;

                        case 6:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 2, 2 * rowSpace + rowSpace / 2);
                            break;

                        case 7:
                            circle.RenderTransform = new TranslateTransform(columnSpace + columnSpace / 2, 2 * rowSpace + rowSpace / 2);
                            break;

                        case 8:
                            circle.RenderTransform = new TranslateTransform(2 * columnSpace + columnSpace / 2, 2 * rowSpace + rowSpace / 2);
                            break;
                    }

                    //Add to the screen
                    gameScreen.Children.Add(circle);
                }
            }
        }
    }
}
