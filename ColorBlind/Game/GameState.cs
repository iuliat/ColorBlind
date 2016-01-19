using System;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ColorBlind
{
    public sealed partial class GamePage : Page
    {
        private Boolean Paused = false;

        private void Start(object sender, RoutedEventArgs e)
        {
            Intro.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Visible;
            PlayButton.Visibility = Visibility.Collapsed;
            ResetButton.Visibility = Visibility.Visible;
            HomeButton.Visibility = Visibility.Visible;
            if (this.DrawTimer == null)
            {
                this.DrawTimer = new Timer(TriggerCallback, this, Slowness, Slowness);
                this.HardTimer = new Timer((object state) => { (state as GamePage).Slowness --; (state as GamePage).Blinkness ++; }, this, HardshipTime, HardshipTime);
                this.StartScore();
            }
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            if (this.DrawTimer != null)
            {
                this.DrawTimer.Dispose();

                Paused = false;
                this.DrawTimer = null;

                lock (RectangleList)
                {
                    RectangleList.Clear();
                    GameArea.Children.Clear();
                }

                this.HardTimer.Dispose();
                Slowness = SlownessDefault;
                Blinkness = BlinknessDefault;

                StopScore();
            }
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            if (this.DrawTimer != null)
            {
                if (this.Paused == false)
                {
                    this.DrawTimer.Change(Timeout.Infinite, Slowness);
                    this.HardTimer.Change(Timeout.Infinite, HardshipTime);
                    this.Paused = true;
                }
                else
                {
                    this.DrawTimer.Change(Slowness, Slowness);
                    this.HardTimer.Change(HardshipTime, HardshipTime);
                    this.Paused = false;
                }
            }
        }
        private void GoHome(object sender, RoutedEventArgs e)
        {
            Stop(sender, e);
            Intro.Visibility = Visibility.Visible;
            PauseButton.Visibility = Visibility.Collapsed;
            ResetButton.Visibility = Visibility.Collapsed;
            NextLevelButton.Visibility = Visibility.Collapsed;
            HomeButton.Visibility = Visibility.Collapsed;
            PlayButton.Visibility = Visibility.Visible;
            GameOver.Visibility = Visibility.Collapsed;
            NextLevel.Visibility = Visibility.Collapsed;
        }


        private void GoToNextLevel(object sender, RoutedEventArgs e)
        {
            NextLevel.Visibility = Visibility.Collapsed;
            NextLevelButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Visible;
            ResetButton.Visibility = Visibility.Visible;
            HomeButton.Visibility = Visibility.Visible;
            GameOver.Visibility = Visibility.Collapsed;

            ColorButton.Foreground = levelColor;
            ColorButton.BorderBrush = levelColor;
            ColorButton.Background = levelColor;
            levelScoreButtom.Foreground = levelColor;
            livesDisplay.Foreground = levelColor;
            CurrentlevelButton.Foreground = levelColor;




            // ColorButton.Margin = new Thickness(40, 0, 0, 0);
            //ScoreBoard.Children.Add(ColorButton);
            if (this.DrawTimer != null)
            {
                if (this.Paused == false)
                {
                    this.DrawTimer.Change(Timeout.Infinite, Slowness);
                    this.HardTimer.Change(Timeout.Infinite, HardshipTime);
                    this.Paused = true;
                }
                else
                {
                    this.DrawTimer.Change(Slowness, Slowness);
                    this.HardTimer.Change(HardshipTime, HardshipTime);
                    this.Paused = false;
                }
            }
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            Stop(sender, e);
            lives = 5;
            Start(sender, e);
            NextLevel.Visibility = Visibility.Collapsed;
            GameOver.Visibility = Visibility.Collapsed;
            NextLevelButton.Visibility = Visibility.Collapsed;

            int numberOfScores = colors.Count;
            Chosen = GenerateSize.Next(colors.Count);
            levelColor = colors[Chosen];
            ColorButton.Foreground = levelColor;
            ColorButton.BorderBrush = levelColor;
            ColorButton.Background = levelColor;

            levelScoreButtom.Margin = new Thickness(ScreenWidth * 3 / 4, 15, 0, 0);
            levelScoreButtom.Visibility = Visibility.Visible;
            levelScoreButtom.FontSize = 14;
            levelScoreButtom.Foreground = levelColor;
            levelScoreButtom.Text = "Score:" + 0;

            livesDisplay.Margin = new Thickness(ScreenWidth * 3 / 4, 30, 0, 0);
            livesDisplay.Foreground = levelColor;
            livesDisplay.Visibility = Visibility.Visible;
            livesDisplay.FontSize = 14;
            livesDisplay.Text = "Lives:" + lives;

            CurrentlevelButton.Foreground = levelColor;
            CurrentlevelButton.Margin = new Thickness(ScreenWidth * 3 / 4, 45, 0, 0);
            CurrentlevelButton.Visibility = Visibility.Visible;
            CurrentlevelButton.FontSize = 14;
            CurrentlevelButton.Text = "Level:" + Level;
            ScoreBoard.Children.Add(levelScoreButtom);
            ScoreBoard.Children.Add(livesDisplay);
            ScoreBoard.Children.Add(CurrentlevelButton);



        }
    }
}
