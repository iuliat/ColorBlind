using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ColorBlind
{
    public sealed partial class GamePage : Page
    {
        public int Chosen;
        public SolidColorBrush levelColor;
        public Button levelScoreButtom, livesDisplay;
        public Button CurrentlevelButton;
        public List<Dictionary<String, int>> levelUpgrades = new List<Dictionary<string, int>>();
        public int Level=1, NextLevelScore, PointLevel, Speed;
        public int lives = 5;

        public List<Dictionary<String, int>> getLevelUpgrades()
        {
            for(int i = 1; i < 5; i++)
            {
                Dictionary<String, int> level = new Dictionary<String, int>();
                level.Add("NextLevelScore", 100*i);
                level.Add("PointLevel", 10*i);
                level.Add("Level", i);
                level.Add("Speed", 100*i); //no idea
                levelUpgrades.Add(level);
            }
            return levelUpgrades;
        }

        public void getSettingsForLevel(int Level)
        {
            NextLevelScore = levelUpgrades[Level - 1]["NextLevelScore"];
            PointLevel = levelUpgrades[Level - 1]["PointLevel"];
            Speed = levelUpgrades[Level - 1]["Speed"];
            Level = levelUpgrades[Level - 1]["Level"];
        }

        public void StartScore()
        {
            int numberOfScores = colors.Count;
           // int Index = 0;
            Chosen = GenerateSize.Next(numberOfScores);
            levelColor = colors[Chosen];

            levelScoreButtom = new Button() { Content = "0" };
            levelScoreButtom.Background = levelColor;
            levelScoreButtom.MinHeight = 60;
            levelScoreButtom.Height = 60;
            levelScoreButtom.MinWidth = (ScreenWidth / (numberOfScores + 2)) - 6;
            levelScoreButtom.Width = (ScreenWidth / (numberOfScores + 2)) - 6;
            levelScoreButtom.Padding = new Thickness(0, 0, 0, 0);
            levelScoreButtom.Margin = new Thickness((ScreenWidth / (numberOfScores + 2)) + 3, ((double)(BarHeightTop) - 60.0) / 2.0, 0, 0);
            levelScoreButtom.FontStyle = Windows.UI.Text.FontStyle.Oblique;


            livesDisplay = new Button();
            livesDisplay.Content = "Lives:" + lives;
            livesDisplay.Width = (ScreenWidth / (numberOfScores + 2)) - 6;
            livesDisplay.Height = 60;
            livesDisplay.FontSize = 12;
            livesDisplay.Margin = new Thickness(300, ((double)(BarHeightTop) - 60.0) / 2.0, 0,0);
            //livesDisplay.Foreground = levelColor;
            livesDisplay.Background = levelColor;
           

            //foreach (SolidColorBrush color in colors)
            //{
            //Button colorScore = new Button() { Content = "0" };
            //colorScore.Background = color;
            //if (Index == Chosen)
            //{
            //    colorScore.MinHeight = 60;
            //    colorScore.Height = 60;
            //    colorScore.MinWidth = (ScreenWidth / (numberOfScores + 2)) - 6;
            //    colorScore.Width = (ScreenWidth / (numberOfScores + 2)) - 6;

            //    colorScore.Padding = new Thickness(0, 0, 0, 0);
            //    colorScore.Margin = new Thickness((ScreenWidth / (numberOfScores + 2)) * Index + 3, ((double)(BarHeightTop) - 60.0) / 2.0, 0, 0);

            //    colorScore.FontStyle = Windows.UI.Text.FontStyle.Oblique;
            //}
            //else
            //{
            //    colorScore.MinHeight = 50;
            //    colorScore.Height = 50;
            //    colorScore.MinWidth = (ScreenWidth / (numberOfScores + 2)) - 12;
            //    colorScore.Width = (ScreenWidth / (numberOfScores + 2)) - 12;

            //    colorScore.Padding = new Thickness(0, 0, 0, 0);
            //    colorScore.Margin = new Thickness((ScreenWidth / (numberOfScores + 2)) * Index + 6, ((double)(BarHeightTop) - 50.0) / 2.0, 0, 0);
            //}

            //Index++;

            ScoreBoard.Children.Add(levelScoreButtom);
            ScoreBoard.Children.Add(livesDisplay);


          CurrentlevelButton = new Button() { Content = "Level:" + Level };
            CurrentlevelButton.Background = levelColor;
            CurrentlevelButton.MinHeight = 50;
            CurrentlevelButton.Height = 50;

            CurrentlevelButton.MinWidth = (ScreenWidth / (numberOfScores + 2)) * 2 - 12;
            CurrentlevelButton.Width = (ScreenWidth / (numberOfScores + 2)) * 2 - 12;

            CurrentlevelButton.Padding = new Thickness(0, 0, 0, 0);
            CurrentlevelButton.Margin = new Thickness(200, ((double)(BarHeightTop) - 50.0) / 2.0, 0, 0);

            ScoreBoard.Children.Add(CurrentlevelButton);
        }
    public void StopScore()
        {
            ScoreBoard.Children.Clear();
        }
    }
}
