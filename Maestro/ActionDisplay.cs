using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace Maestro
{
    class ActionDisplay
    {
        const int DISPLAYMARGIN = 1001;

        private Canvas gameScreen { get; set; }
        private List<Step> stepList { get; set; }
        private int index = 0;
        public double columnSpace {get;set;}
        public double rowSpace {get;set;}

        //Step id's
        string[] uidSteps;

        //Last displayed index
        private int lastIndex;

        //Current score
        public int currentScore { get; set; }

        public Difficulty selectedDifficulty { get; set; }

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

            uidSteps = new string[36];

           
        }

        public void loadSteps(List<Step> stepList)
        {
            this.stepList = stepList;
        }

        public void drawWithLabel(TextBox t)
        {
            TextBox border = new TextBox();
            border.Background = null;
            border.BorderBrush = null;
            border.TextAlignment = t.TextAlignment;
            border.FontFamily = t.FontFamily;
            border.Width = t.Width;
            border.Text = t.Text;
            border.RenderTransform = t.RenderTransform;

            border.FontSize = t.FontSize * 1.02;
            border.Foreground = new RadialGradientBrush(Colors.White, Colors.LightGray);
            //
            border.Visibility = System.Windows.Visibility.Hidden;

            gameScreen.Children.Add(border);
            gameScreen.Children.Add(t);
        }

        public void prepareCanvas()
        {
            //Clear the canvas
            gameScreen.Children.Clear();

            //Margins
            double marginX = columnSpace * 0.23;
            double marginY = rowSpace * 0.13;

            //For each case
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Ellipse circle = new Ellipse();
                    circle.Width = rowSpace * 0.75;
                    circle.Height = rowSpace * 0.75;
                    circle.StrokeThickness = 10;

                    if(j == 0){
                                circle.Stroke = new LinearGradientBrush(Colors.Orange, Colors.BlueViolet, 90);
                                circle.Fill = new RadialGradientBrush(Colors.Red, Colors.DarkRed);
                                
                            }
                    else if (j == 1)
                            {
                                circle.Stroke = new LinearGradientBrush(Colors.BlueViolet, Colors.Orange, 90);
                                circle.Fill = new RadialGradientBrush(Colors.Yellow, Colors.Goldenrod);
                                
                            }
                    else if (j ==2)
                            {
                                circle.Stroke = new LinearGradientBrush(Colors.Snow, Colors.HotPink, 90);
                                circle.Fill = new RadialGradientBrush(Colors.LightGreen, Colors.Green);
                                
                            }
                    else 
                            {
                                circle.Stroke = new LinearGradientBrush(Colors.HotPink, Colors.Snow, 90);
                                circle.Fill = new RadialGradientBrush(Colors.Blue, Colors.DarkBlue);
                                
                            }

                    
                    //Place it correctly
                    circle.RenderTransform = new TranslateTransform(columnSpace * (i % 3) + marginX, rowSpace * (i / 3) + marginY);

                    //Make it invisible
                    circle.Opacity = 0;

                    circle.Uid = index.ToString();

                    //Store the ID
                    uidSteps[index] = circle.Uid;

                    //Increment the index
                    index++;
                    
                    //Add it to the canvas
                    gameScreen.Children.Add(circle);
                    }

                }

                // current score display
                /*
                TextBox score = new TextBox();

                score.Background = null;
                score.BorderBrush = null;
                score.TextAlignment = System.Windows.TextAlignment.Center;
                score.Width = columnSpace * 0.8;

                score.FontSize = 36;
                score.FontFamily = new FontFamily("MV Boli");
                score.Foreground = Brushes.Snow;

                score.RenderTransform = new TranslateTransform(columnSpace * 2.2, 0);
                score.Text = "Score : ";

                drawWithLabel(score);
                 * */

            }
            

        //Check if I should make the step visible or not
        public void checkSteps(int currentTime)
        {

            Step currentStep;
            //gameScreen.Children.Clear();


            //Foreach step
            for (int i = lastIndex; i < stepList.Count; ++i)
            {
                currentStep = stepList.ElementAt(i);

                #region step Done
         
                //if (currentStep.done == true && currentTime - DISPLAYMARGIN <= currentStep.timing && currentTime <= currentStep.timing + DISPLAYMARGIN)

                //{
                //    //Visual for performance levels
                //    TextBox textGreat = new TextBox();

                //    textGreat.Background = null;
                //    textGreat.BorderBrush = null;
                //    textGreat.TextAlignment = System.Windows.TextAlignment.Center;
                //    textGreat.Width = columnSpace;

                //    textGreat.FontSize = 72;
                //    textGreat.FontFamily = new FontFamily("Jokerman");
                //    textGreat.Foreground = new RadialGradientBrush(Colors.Red, Colors.DarkRed);

                //    double marginX = 0;
                //    double marginY = rowSpace * 0.25;

                //    textGreat.RenderTransform = new TranslateTransform(columnSpace * (currentStep.area % 3) + marginX, rowSpace * (currentStep.area / 3) + marginY);
                //    textGreat.Text = "Great!!";
                //    //textGreat.Text = "Good!";
                //    //textGreat.Text = "No Good";

                //    //gameScreen.Children.Add(textGreat);
                //    drawWithLabel(textGreat);

                //}

                #endregion

                //If the step is valid                
                if (currentStep.done == false && currentStep.timing - DISPLAYMARGIN <= currentTime && currentTime <= currentStep.timing + DISPLAYMARGIN)
                {
                    //Store as last valid index
                    lastIndex = i;
                    int indexToAppear = currentStep.area;
                    string currentID = "";

                    //Print the step
                    foreach (UIElement currentItem in gameScreen.Children)
                    {
                        switch (currentStep.action)
                        {
                            case ActionType.TouchHandLeft :
                                currentID= uidSteps[currentStep.area * 4 + 0];
                                break;

                            case ActionType.TouchHandRight:
                                currentID = uidSteps[currentStep.area * 4 + 1];
                                break;

                            case ActionType.TouchFeetLeft:
                                currentID = uidSteps[currentStep.area * 4 + 2];
                                break;

                            case ActionType.TouchFeetRight:
                                currentID = uidSteps[currentStep.area * 4 + 3];
                                break;

                        }

                        //Display the action
                        if (currentItem.Uid.CompareTo(currentID) == 0)
                        {
                            currentItem.Opacity = 100;
                            
                        }
                        else
                            currentItem.Opacity = 0;
                    }
                    
                    
                }


                

            }
        }
    }
}
