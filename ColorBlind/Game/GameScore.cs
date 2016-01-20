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
        //public Button levelScoreButtom, livesDisplay;
        public Button ColorButton;
        public List<Dictionary<String, int>> levelUpgrades = new List<Dictionary<string, int>>();
        public int Level=1, NextLevelScore, PointLevel, Speed;
        public int lives = 5;
        int noOfLevels = 50;

        public List<Dictionary<String, int>> getLevelUpgrades()
        {
            for(int i = 1; i < noOfLevels; i++)
            {
                Dictionary<String, int> level = new Dictionary<String, int>();
                level.Add("NextLevelScore", 100*i);
                level.Add("PointLevel", 10*i);
                level.Add("Level", i);
                //level.Add("Speed", 2*i); // blinkness = max 7 for last level
                level.Add("Color", GenerateSize.Next(colors.Count));
                levelUpgrades.Add(level);
            }
            return levelUpgrades;
        }

        public void getSettingsForLevel(int Level)
        {
            //int numberOfScores = colors.Count;
            //Chosen = GenerateSize.Next(colors.Count);
            //levelColor = colors[Chosen];
            NextLevelScore = levelUpgrades[Level - 1]["NextLevelScore"];
            PointLevel = levelUpgrades[Level - 1]["PointLevel"];
           // Speed = levelUpgrades[Level - 1]["Speed"];
            Level = levelUpgrades[Level - 1]["Level"];
            levelColor = colors[levelUpgrades[Level - 1]["Color"]];
        }

        public void StartScore()
        {
            PauseButton.Visibility = Visibility.Visible;

            //int numberOfScores = colors.Count;
            //Chosen = GenerateSize.Next(numberOfScores);
            //levelColor = colors[Chosen];

            ColorButton = new Button();
            ColorButton.Foreground = levelColor;
            ColorButton.BorderBrush = levelColor;
            ColorButton.Background = levelColor;
            ColorButton.Margin = new Thickness(40, 0, 0, 0);
            ScoreBoard.Children.Add(ColorButton);


            levelScoreButtom.Margin = new Thickness(this.ActualWidth * 3 / 4, 15, 0, 0);
            levelScoreButtom.Visibility = Visibility.Visible;
            levelScoreButtom.FontSize = 14;
            levelScoreButtom.Foreground = levelColor;

            livesDisplay.Margin = new Thickness(this.ActualWidth * 3 / 4, 30, 0, 0);
            livesDisplay.Foreground = levelColor;
            livesDisplay.Visibility = Visibility.Visible;
            livesDisplay.FontSize = 14;

            CurrentlevelButton.Foreground = levelColor;
            CurrentlevelButton.Margin = new Thickness(this.ActualWidth * 3 / 4, 45, 0, 0);
            CurrentlevelButton.Visibility = Visibility.Visible;
            CurrentlevelButton.FontSize = 14;

        }
    public void StopScore()
        {
            ScoreBoard.Children.Clear();
        }
    }
}
