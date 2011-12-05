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

            gameScreen.Children.Add(border);
            gameScreen.Children.Add(t);
        }

        public void displayStep(int currentTime)
        {

            Step currentStep;
            gameScreen.Children.Clear();


            //Foreach step
            for (int i = lastIndex; i < stepList.Count; ++i)
            {
                currentStep = stepList.ElementAt(i);

                #region step Done
         
                if (currentStep.done == true && currentTime - DISPLAYMARGIN <= currentStep.timing && currentTime <= currentStep.timing + DISPLAYMARGIN)

                {
                    //Visual for performance levels
                    TextBox textGreat = new TextBox();

                    textGreat.Background = null;
                    textGreat.BorderBrush = null;
                    textGreat.TextAlignment = System.Windows.TextAlignment.Center;
                    textGreat.Width = columnSpace;

                    textGreat.FontSize = 72;
                    textGreat.FontFamily = new FontFamily("Jokerman");
                    textGreat.Foreground = new RadialGradientBrush(Colors.Red, Colors.DarkRed);

                    double marginX = 0;
                    double marginY = rowSpace * 0.25;

                    textGreat.RenderTransform = new TranslateTransform(currentStep.area % 3 + marginX, currentStep.area / 3 + marginY);
                    textGreat.Text = "Great!!";
                    //textGreat.Text = "Good!";
                    //textGreat.Text = "No Good";

                    //gameScreen.Children.Add(textGreat);
                    drawWithLabel(textGreat);

                }

                #endregion


                //If the step is valid                
                if (currentStep.done == false && currentStep.timing - DISPLAYMARGIN <= currentTime && currentTime <= currentStep.timing + DISPLAYMARGIN)
                {
                    //Store as last valid index
                    lastIndex = i;

                    Ellipse circle = new Ellipse();
                    circle.Width = rowSpace * 0.75;
                    circle.Height = rowSpace * 0.75;
                    circle.StrokeThickness = 10;

                    //SolidColorBrush myBrush = new SolidColorBrush();
                    //System.Windows.Media.Animation.ColorAnimation colorAnime = new System.Windows.Media.Animation.ColorAnimation(Colors.Red, new Color(), TimeSpan.FromSeconds(3));
                    //myBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnime);
                    //circle.Fill = myBrush;

                    switch (currentStep.action)
                    {
                        case ActionType.TouchHandLeft:
                            {
                                circle.Stroke = new LinearGradientBrush(Colors.Orange, Colors.BlueViolet, 90);
                                circle.Fill = new RadialGradientBrush(Colors.Red, Colors.DarkRed);
                                break;
                            }
                        case ActionType.TouchHandRight:
                            {
                                circle.Stroke = new LinearGradientBrush(Colors.BlueViolet, Colors.Orange, 90);
                                circle.Fill = new RadialGradientBrush(Colors.Yellow, Colors.Goldenrod);
                                break;
                            }
                        case ActionType.TouchFeetLeft:
                            {
                                circle.Stroke = new LinearGradientBrush(Colors.Snow, Colors.HotPink, 90);
                                circle.Fill = new RadialGradientBrush(Colors.LightGreen, Colors.Green);
                                break;
                            }
                        case ActionType.TouchFeetRight:
                            {
                                circle.Stroke = new LinearGradientBrush(Colors.HotPink, Colors.Snow, 90);
                                circle.Fill = new RadialGradientBrush(Colors.Blue, Colors.DarkBlue);
                                break;
                            }
                        default:
                            {
                                circle.Stroke = new LinearGradientBrush(Colors.Orange, Colors.BlueViolet, 90);
                                circle.Fill = new RadialGradientBrush(Colors.Red, Colors.DarkRed);
                                break;
                            }
                    }

                    //TODO : DISPLAY THE REDUCING CIRCLE, HOLD SYSTEM

                    double marginX = columnSpace * 0.23;
                    double marginY = rowSpace * 0.13;

                    circle.RenderTransform = new TranslateTransform(currentStep.area % 3 + marginX, currentStep.area / 3 + marginY);

                    //Add to the screens
                    gameScreen.Children.Add(circle);
                }


                // current score display
                TextBox score = new TextBox();

                score.Background = null;
                score.BorderBrush = null;
                score.TextAlignment = System.Windows.TextAlignment.Center;
                score.Width = columnSpace * 0.8;

                score.FontSize = 36;
                score.FontFamily = new FontFamily("MV Boli");
                score.Foreground = Brushes.Snow;

                score.RenderTransform = new TranslateTransform(columnSpace * 2.2, 0);
                score.Text = "Score : " + currentScore; ;

                drawWithLabel(score);

            }
        }
    }
}
