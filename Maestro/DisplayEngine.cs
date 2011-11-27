using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;

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

        //Textbox
        private TextBox[] texts;

        //Current displayed screen
        public Screen CurrentScreen { get; private set; }

        //Active for command
        private bool active;
        private static Timer count;

        //Constructor
        public DisplayEngine(Canvas mainScreen)
        {
            GameScreen = mainScreen;
            CurrentScreen = Screen.Main;
            grid = new Rectangle[9];

            texts = new TextBox[4];

            //Divide the screen into 3 parts
            columnSpace = GameScreen.Width / 3.0;
            rowSpace = GameScreen.Height / 3.0;

            active = true;
            count = new Timer(2000);
            count.Elapsed += delegate
            {

                active = true;
            
            };
            
        }

        //!Calculate grid position
        public void GenerateGrid()
        {
            /*
            //Add columns
            for (double i = columnSpace; i < GameScreen.Width; i += columnSpace)
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
            GameScreen.Children.Add(new Line());
             */

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

            for (int c = 0; c < 4; c++)
            {
                texts[c] = new TextBox();
                //texts[c].FontFamily = new FontFamily("Jokerman");
                //texts[c].TextAlignment = System.Windows.TextAlignment.Center;
                //texts[c].Width = columnSpace;
                texts[c].Background = null;
                texts[c].BorderBrush = null;
                texts[c].Foreground = Brushes.Red;

                //GameScreen.Children.Add(texts[c]);
            }
        }

        public Screen clap(int hand, int leftFoot, int rightFoot)
        {
            updateScreen(hand, hand, leftFoot, rightFoot);
            return CurrentScreen;
        }

        private void changeScreen(Screen screen, int shift)
        {
            //GameScreen.Children.Clear();
            GameScreen.Children.Clear();
            CurrentScreen = screen;
            active = false;
            count.Start();

            if (screen == Screen.Profile)
            {
                for (int c = 0; c < 4; c++)
                {
                    texts[c].FontFamily = new FontFamily("Jokerman");
                    texts[c].TextAlignment = System.Windows.TextAlignment.Center;
                    texts[c].Width = columnSpace;
                }

                texts[0].FontSize = 96;
                texts[1].FontSize = 48;
                texts[2].FontSize = 48;
                texts[3].FontSize = 36;

                texts[0].RenderTransform = new TranslateTransform(columnSpace, 150);
                texts[1].RenderTransform = new TranslateTransform(0, 130);
                texts[2].RenderTransform = new TranslateTransform(columnSpace * 2, 130);
                texts[3].RenderTransform = new TranslateTransform(columnSpace, 110);

                if (shift == 0)
                {
                    texts[0].Text = "Minho";
                    texts[1].Text = "Kihwan";
                    texts[2].Text = "Heri";
                    
                }
                else if (shift == 1)
                {
                    texts[0].Text = "Kihwan";
                    texts[1].Text = "Heri";
                    texts[2].Text = "Minho";
                }
                else if (shift == 2)
                {
                    texts[0].Text = "Heri";
                    texts[1].Text = "MInho";
                    texts[2].Text = "Kihwan";
                }
                texts[3].Text = "Player";
                

                for (int c = 0; c < 4; c++)
                    GameScreen.Children.Add(texts[c]);
            }
            else if (screen == Screen.SelectSong)
            {
                for (int c = 0; c < 4; c++)
                {
                    texts[c].FontFamily = new FontFamily("Jokerman");
                    texts[c].TextAlignment = System.Windows.TextAlignment.Center;
                    texts[c].Width = columnSpace;
                }

                /*
                texts[0].FontSize = 72;
                texts[1].FontSize = 40;
                texts[2].FontSize = 40;
                texts[3].FontSize = 32;
                 * */
                texts[0].FontSize = 96;
                texts[1].FontSize = 48;
                texts[2].FontSize = 48;
                texts[3].FontSize = 36;

                texts[0].RenderTransform = new TranslateTransform(columnSpace, 150);
                texts[1].RenderTransform = new TranslateTransform(0, 130);
                texts[2].RenderTransform = new TranslateTransform(columnSpace * 2, 130);
                texts[3].RenderTransform = new TranslateTransform(columnSpace, 110);

                if (shift == 0 || shift == 3)
                {
                    texts[0].Text = "Song A";
                    texts[1].Text = "Song B";
                    texts[2].Text = "Song C";
                }
                else if (shift == 1)
                {
                    texts[0].Text = "Song B";
                    texts[1].Text = "Song C";
                    texts[2].Text = "Song A";
                }
                else if (shift == 2)
                {
                    texts[0].Text = "Song C";
                    texts[1].Text = "Song A";
                    texts[2].Text = "Song B";
                }

                if (shift == 0 || shift == 1 || shift == 2)
                {
                    texts[3].Text = "Easy";
                }

                if (shift == 3)
                {
                    texts[3].Text = "Medium";
                    /*
                    if (_difficulty == Difficulty.Easy)
                        texts[3].Text = "Medium";
                    if (_difficulty == Difficulty.Medium)
                        texts[3].Text = "Hard";
                    if (_difficulty == Difficulty.Hard)
                        texts[3].Text = "Easy";
                     * */
                }

                for (int c = 0; c < 4; c++)
                    GameScreen.Children.Add(texts[c]);
            }
            else if (screen == Screen.Leaderboards)
            {
                for (int c = 0; c < 4; c++)
                {
                    texts[c].FontFamily = new FontFamily("MV Boli");
                    texts[c].TextAlignment = System.Windows.TextAlignment.Left;
                    texts[c].Width = 3 * columnSpace;
                    texts[c].FontSize = 40;
                }

                texts[0].RenderTransform = new TranslateTransform(140, 240);
                texts[1].RenderTransform = new TranslateTransform(140, 300);
                texts[2].RenderTransform = new TranslateTransform(140, 360);

                texts[0].Text = "1 Heri   \tSong A  \t298";
                texts[1].Text = "2 Kihwan   \tSong B  \t201";
                texts[2].Text = "3 Minho   \tSong C  \t195";
                texts[3].Text = "";

                for (int c = 0; c < 4; c++)
                    GameScreen.Children.Add(texts[c]);
            }
            else if (screen == Screen.Score)
            {
                for (int c = 0; c < 4; c++)
                {
                    texts[c].FontFamily = new FontFamily("MV Boli");
                    texts[c].TextAlignment = System.Windows.TextAlignment.Center;
                    texts[c].Width = columnSpace;
                    texts[c].FontSize = 60;
                }
                texts[0].FontFamily = new FontFamily("Jokerman");
                texts[0].FontSize = 216;

                texts[0].RenderTransform = new TranslateTransform(60, 180);
                texts[1].RenderTransform = new TranslateTransform(columnSpace, 260);
                texts[2].RenderTransform = new TranslateTransform(2 * columnSpace, 260);

                texts[0].Text = "B";
                texts[1].Text = "Minho\nSong A";
                texts[2].Text = "Score\n195";
                texts[3].Text = "";

                for (int c = 0; c < 4; c++)
                    GameScreen.Children.Add(texts[c]);
            }
            else
            {
                /*
                for (int c = 0; c < 4; c++)
                    texts[c].Text = "";

                for (int c = 0; c < 4; c++)
                    GameScreen.Children.Add(texts[c]);
                 * */
            }
        }

        //Update the screen
        public void updateScreen(int leftHand, int rightHand, int leftFoot, int rightFoot)
        {
            //Clear everything
            /*grid[0].Opacity = grid[1].Opacity = grid[2].Opacity = grid[3].Opacity = grid[4].Opacity = grid[5].Opacity = grid[6].Opacity = grid[7].Opacity = grid[8].Opacity = 0.0;


            grid[leftHand].Opacity = 0.7;
            grid[leftHand].Fill = Brushes.Green;
            grid[rightHand].Opacity = 0.7;
            grid[rightHand].Fill = Brushes.Gold;*/

            
            
            if (active)
            {

                switch (CurrentScreen)
                {
                    case Screen.Main:
                        if (leftHand == 4)
                        {
                            //go to profile
                            changeScreen(Screen.Profile, 0);
                        }
                        break;
                    case Screen.Profile:
                        if (leftHand == 3)
                        {
                            //move profile left
                            changeScreen(Screen.Profile, 1);
                        }
                        if (leftHand == 4)
                        {
                            changeScreen(Screen.Leaderboards, 0);
                        }
                        if (rightHand == 5)
                        {
                            //move profile right
                            changeScreen(Screen.Profile, 2);
                        }
                        if (leftHand == 7)
                        {
                            changeScreen(Screen.SelectSong, 0);
                        }
                        if (leftHand == 8)
                        {
                            changeScreen(Screen.Main, 0);
                        }
                        break;
                    case Screen.Leaderboards:
                        if (leftHand == 8)
                        {
                            changeScreen(Screen.Profile, 0);
                        }
                        break;
                    case Screen.SelectSong:
                        if (leftHand == 3)
                        {
                            //move song left
                            changeScreen(Screen.SelectSong, 1);
                        }
                        if (leftHand == 4)
                        {
                            //change difficulty
                            changeScreen(Screen.SelectSong, 3);
                        }
                        if (rightHand == 5)
                        {
                            //move song right
                            changeScreen(Screen.SelectSong, 2);
                        }
                        if (leftHand == 7)
                        {
                            changeScreen(Screen.Game, 0);
                        }
                        if (leftHand == 8)
                        {
                            changeScreen(Screen.Profile, 0);
                        }
                        break;
                    case Screen.Game:
                        if (leftHand == 8)
                        {
                            changeScreen(Screen.Score, 0);
                        }
                        break;
                    case Screen.Score:
                        if (leftHand == 8)
                        {
                            changeScreen(Screen.SelectSong, 0);
                        }
                        break;
                    default:
                        break;
                }

            }

            Display();
        }

        //Update the display
        public void Display()
        {

            ImageBrush main = new ImageBrush();
            main.ImageSource = new BitmapImage(
                    new Uri("images\\screen_main.png", UriKind.Relative));

            ImageBrush profile = new ImageBrush();
            profile.ImageSource = new BitmapImage(
                    new Uri("images\\screen_profile.png", UriKind.Relative));

            ImageBrush leaderboards = new ImageBrush();
            leaderboards.ImageSource = new BitmapImage(
                    new Uri("images\\screen_leaderboards.png", UriKind.Relative));

            ImageBrush selectsong = new ImageBrush();
            selectsong.ImageSource = new BitmapImage(
                    new Uri("images\\screen_selectsong.png", UriKind.Relative));

            ImageBrush game = new ImageBrush();
            game.ImageSource = new BitmapImage(
                    new Uri("images\\screen_game.png", UriKind.Relative));

            ImageBrush score = new ImageBrush();
            score.ImageSource = new BitmapImage(
                    new Uri("images\\screen_score.png", UriKind.Relative));

            if (CurrentScreen == Screen.Main)
            {
                GameScreen.Background = main;
            }
            else if (CurrentScreen == Screen.Profile)
            {
                GameScreen.Background = profile;
            }
            else if (CurrentScreen == Screen.Leaderboards)
            {
                GameScreen.Background = leaderboards;
            }
            else if (CurrentScreen == Screen.Game)
            {
                GameScreen.Background = game;
            }
            else if (CurrentScreen == Screen.Score)
            {
                GameScreen.Background = score;
            }
            else if (CurrentScreen == Screen.SelectSong)
            {
                GameScreen.Background = selectsong;
            }
            else
            {
                //this isn't good
            }
        }

        public void updateSelect(int leftHand, int rightHand, int leftFoot, int rightFoot)
        {

            if (active)
            {

                switch (CurrentScreen)
                {
                    case Screen.Profile:
                        if (leftHand == 3)
                        {

                        }
                        break;
                    default:
                        break;
                }
            }
        }


        public void generateMarker(int gridNum, int actionType)
        {
            grid[gridNum].Fill = Brushes.Gold;
        }

    }
}