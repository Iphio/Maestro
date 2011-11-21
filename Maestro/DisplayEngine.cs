using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maestro
{
    class DisplayEngine
    {
        //Current gamescreen
        Canvas GameScreen { get; set; }

        public double columnSpace { get; set; }
        public double rowSpace { get; set; }

        //Grid
        private Rectangle[] grid;

        //Current displayed screen
        public Screen CurrentScreen { get; private set; }

        //Constructor
        public DisplayEngine(Canvas mainScreen)
        {
            GameScreen = mainScreen;
            CurrentScreen = Screen.Main;
            grid = new Rectangle[9];

            //Divide the screen into 3 parts
            columnSpace = GameScreen.Width / 3.0;
            rowSpace = GameScreen.Height / 3.0;

        }

        //!Calculate grid position
        public void GenerateGrid()
        {
            //Add columns
            /*for (double i = columnSpace; i < GameScreen.Width; i += columnSpace)
            {
                Line currentColumn = new Line();
                currentColumn.Stroke = System.Windows.Media.Brushes.DarkSlateBlue;
                currentColumn.StrokeThickness = 5;
                currentColumn.X1 = i;
                currentColumn.Y1 = 0;

                currentColumn.X2 = i;
                currentColumn.Y2 = GameScreen.Height;

                GameScreen.Children.Add(currentColumn);
            }

            //Add rows
            for (double j = rowSpace; j < GameScreen.Height; j += rowSpace)
            {
                Line currentRow = new Line();
                currentRow.StrokeThickness = 5;
                currentRow.Stroke = System.Windows.Media.Brushes.LightSteelBlue;

                currentRow.X1 = 0;
                currentRow.Y1 = j;

                currentRow.X2 = GameScreen.Width;
                currentRow.Y2 = j;

                GameScreen.Children.Add(currentRow);


            }
            GameScreen.Children.Add(new Line());*/

            int x = 0, y = 0;

            //Put the grid
            for (int c = 0; c < 9; c++)
            {
                grid[c] = new Rectangle();
                grid[c].Width = columnSpace;
                grid[c].Height = rowSpace;

                grid[c].RenderTransform = new TranslateTransform(x * columnSpace, y * rowSpace);
                grid[c].Opacity = 0.0;

                GameScreen.Children.Add(grid[c]);

                x++;

                x = x % 3;

                if (x == 0)
                {
                    y++;
                    y = y % 3;
                }
            }
        }


        //Update the screen
        public void updateScreen(int left, int right)
        {

            grid[left].Opacity = 0.7;
            grid[left].Fill = Brushes.Green;
            grid[right].Opacity = 0.7;
            grid[right].Fill = Brushes.Gold;

            switch (CurrentScreen) {
                case Screen.Main :
                    if (left == 3 || right == 3)
                {
                    //go to profile
                    CurrentScreen = Screen.Profile;

                }
                    break;

                case Screen.Profile :

                    if (left == 7 || right == 7)
                    {
                        CurrentScreen = Screen.Main;
                    }

                    break;

            }

            Display();
        }        

        //Update the display
        public void Display()
        {
            ImageBrush main = new ImageBrush();
            main.ImageSource = new BitmapImage(
                    new Uri("images\\Main\\main.jpg", UriKind.Relative));

            ImageBrush profile = new ImageBrush();
            profile.ImageSource = new BitmapImage(
                    new Uri("images\\Profile\\profile.jpg", UriKind.Relative));

            if (CurrentScreen == Screen.Main)
            {
                GameScreen.Background = main;
            }
            else if (CurrentScreen == Screen.Profile)
            {
                GameScreen.Background = profile;
            }
           /* else if (CurrentScreen == Screen.Leaderboards)
            {
                Gamescreen.Background = "";
            }
            else if (CurrentScreen == Screen.Game)
            {
                Gamescreen.Background = "";
            }
            else if (CurrentScreen == Screen.Score)
            {
                Gamescreen.Background = "";
            }
            else if (CurrentScreen == Screen.SelectSong)
            {
                Gamescreen.Background = "";
            }
            else
            {
                //this isn't good
            }*/
        }


    }
}
