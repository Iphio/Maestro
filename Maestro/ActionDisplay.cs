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

        public void displayStep(int currentTime)
        {

            Step currentStep;
            gameScreen.Children.Clear();


            //Foreach step
            for (int i = lastIndex; i < stepList.Count; ++i)
            {
                currentStep = stepList.ElementAt(i);

                #region step Done
                if (currentStep.done == true)
                {

                    TextBox textGreat = new TextBox();

                    textGreat.Background = null;
                    textGreat.BorderBrush = null;
                    textGreat.Foreground = Brushes.Red;

                    textGreat.FontSize = 48;
                    textGreat.FontFamily = new FontFamily("Jokerman");
                    textGreat.Text = "Great!";

                    switch (currentStep.area)
                    {
                        case 0:
                            textGreat.RenderTransform = new TranslateTransform(columnSpace / 4, rowSpace / 8);
                            break;

                        case 1:
                            textGreat.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace, rowSpace / 8);
                            break;

                        case 2:
                            textGreat.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace * 2, rowSpace / 8);
                            break;

                        case 3:
                            textGreat.RenderTransform = new TranslateTransform(columnSpace / 4, rowSpace / 8 + rowSpace);
                            break;

                        case 4:
                            textGreat.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace, rowSpace / 8 + rowSpace);
                            break;

                        case 5:
                            textGreat.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace * 2, rowSpace / 8 + rowSpace);
                            break;

                        case 6:
                            textGreat.RenderTransform = new TranslateTransform(columnSpace / 4, rowSpace / 8 + rowSpace * 2);
                            break;

                        case 7:
                            textGreat.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace, rowSpace / 8 + rowSpace * 2);
                            break;

                        case 8:
                            textGreat.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace * 2, rowSpace / 8 + rowSpace * 2);
                            break;
                    }

                    gameScreen.Children.Add(textGreat);

                }
                #endregion

                //If the step is valid                
                if (currentStep.done == false && currentStep.timing - DISPLAYMARGIN <= currentTime && currentTime <= currentStep.timing + DISPLAYMARGIN)
                {
                    //Store as last valid index
                    lastIndex = i;

                    //Create the visual component
                    /*Ellipse circle = new Ellipse();
                    circle.Stroke = Brushes.Gold;
                    circle.StrokeThickness = 45;
                    circle.Fill = Brushes.Red;*/

                    Ellipse circle = new Ellipse();
                    circle.Width = 3 * rowSpace / 4;
                    circle.Height = 3 * rowSpace / 4;

                    circle.Stroke = Brushes.Orange;
                    circle.StrokeThickness = 10;

                    System.Windows.Media.Animation.ColorAnimation colorAnime = new System.Windows.Media.Animation.ColorAnimation(Colors.Red, new Color(), TimeSpan.FromSeconds(3));
                    SolidColorBrush myBrush = new SolidColorBrush();

                    myBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnime);
                    circle.Fill = myBrush;

                    //TODO : DISPLAY THE REDUCING CIRCLE, HOLD SYSTEM

                    //Display
                    switch (currentStep.area)
                    {
                        case 0:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 4, rowSpace / 8);
                            break;

                        case 1:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace, rowSpace / 8);
                            break;

                        case 2:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace * 2, rowSpace / 8);
                            break;

                        case 3:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 4, rowSpace / 8 + rowSpace);
                            break;

                        case 4:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace, rowSpace / 8 + rowSpace);
                            break;

                        case 5:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace * 2, rowSpace / 8 + rowSpace);
                            break;

                        case 6:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 4, rowSpace / 8 + rowSpace * 2);
                            break;

                        case 7:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace, rowSpace / 8 + rowSpace * 2);
                            break;

                        case 8:
                            circle.RenderTransform = new TranslateTransform(columnSpace / 4 + columnSpace * 2, rowSpace / 8 + rowSpace * 2);
                            break;
                    }

                    //Add to the screen
                    gameScreen.Children.Add(circle);
                }
            }
        }
    }
}
