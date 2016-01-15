using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ColorBlind
{
    public sealed partial class GamePage : Page
    {
        SolidColorBrush colorTotal = GetColorFromHex("#00000000");

        public void StartScore()
        {
            int numberOfScores = colors.Count;
            int Index = 0;
            int Chosen = GenerateSize.Next(numberOfScores);

            foreach (SolidColorBrush color in colors)
            {
                Button colorScore = new Button() { Content = "0" };
                colorScore.Background = color;
                if (Index == Chosen)
                {
                    colorScore.MinHeight = 60;
                    colorScore.Height = 60;
                    colorScore.MinWidth = (ScreenWidth / (numberOfScores + 2)) - 6;
                    colorScore.Width = (ScreenWidth / (numberOfScores + 2)) - 6;

                    colorScore.Padding = new Thickness(0, 0, 0, 0);
                    colorScore.Margin = new Thickness((ScreenWidth / (numberOfScores + 2)) * Index + 3, ((double)(BarHeightTop) - 60.0) / 2.0, 0, 0);

                    colorScore.FontStyle = Windows.UI.Text.FontStyle.Oblique;
                }
                else
                {
                    colorScore.MinHeight = 50;
                    colorScore.Height = 50;
                    colorScore.MinWidth = (ScreenWidth / (numberOfScores + 2)) - 12;
                    colorScore.Width = (ScreenWidth / (numberOfScores + 2)) - 12;

                    colorScore.Padding = new Thickness(0, 0, 0, 0);
                    colorScore.Margin = new Thickness((ScreenWidth / (numberOfScores + 2)) * Index + 6, ((double)(BarHeightTop) - 50.0) / 2.0, 0, 0);
                }

                Index++;

                ScoreBoard.Children.Add(colorScore);
            }

            Button totalScore = new Button() { Content = "0" };
            totalScore.Background = colorTotal;
            totalScore.MinHeight = 50;
            totalScore.Height = 50;

            totalScore.MinWidth = (ScreenWidth / (numberOfScores + 2)) * 2 - 12;
            totalScore.Width = (ScreenWidth / (numberOfScores + 2)) * 2 - 12;

            totalScore.Padding = new Thickness(0, 0, 0, 0);
            totalScore.Margin = new Thickness((ScreenWidth / (numberOfScores + 2)) * Index + 6, ((double)(BarHeightTop) - 50.0) / 2.0, 0, 0);

            ScoreBoard.Children.Add(totalScore);
        }
        public void StopScore()
        {
            ScoreBoard.Children.Clear();
        }
    }
}
