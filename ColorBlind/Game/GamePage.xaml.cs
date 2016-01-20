using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Core;
using System.Diagnostics;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace ColorBlind
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private LinkedList<Rectangle> RectangleList = new LinkedList<Rectangle>();
        private Random GenerateSize = new Random(DateTime.Now.Millisecond);
        Timer DrawTimer = null;
        Timer HardTimer = null;

        public GamePage()
        {
            this.generateColors();
            this.getLevelUpgrades();
            this.getSettingsForLevel(1);

            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            PauseButton.Visibility = Visibility.Collapsed;
            ResetButton.Visibility = Visibility.Collapsed;
            NextLevelButton.Visibility = Visibility.Collapsed;
            HomeButton.Visibility = Visibility.Collapsed;
            GameDescription.Text = "Catch all the blocks having the color\n" + " displayed in the left corner of the screen.\n" + "Let's see how quick you are.";
           // GameDescription.Width = 350;
          //  GameDescription.Height = 350;
            // GameDescription.Margin = new Thickness(((this.ActualWidth - GameDescription.Width) / 2), ((this.ActualHeight - GameDescription.Height) / 2), 0, 0);
          //  GameDescription.Margin = new Thickness(0, GameDescription.Height, 0, 0);
        }

        public void generateColors()
        {
            SolidColorBrush red = GetColorFromHex("#FFD60A0A");
            SolidColorBrush yellow = GetColorFromHex("#FFFFF824");
            SolidColorBrush purple = GetColorFromHex("#FF9C2167");
            SolidColorBrush blue = GetColorFromHex("#FF0F538E");
            SolidColorBrush green = GetColorFromHex("#FF005710");

            colors.Add(red);
            colors.Add(yellow);
            colors.Add(purple);
            colors.Add(blue);
            colors.Add(green);
        }

        async private void TriggerCallback(object state)
        {
            Page GameArea = state as GamePage;
            await GameArea.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, DrawCallback);
        }

        public static SolidColorBrush GetColorFromHex(string hexaColor)
        {
            return new SolidColorBrush(
                Windows.UI.Color.FromArgb(
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16),
                    Convert.ToByte(hexaColor.Substring(7, 2), 16)
                )
            );
        }

        private void DrawCallback()
        {
            LinkedList<Rectangle> RemoveList = new LinkedList<Rectangle>();

            checkLevel();

            lock (RectangleList)
            {
                foreach (Rectangle rectangle in RectangleList)
                {
                    double y = rectangle.Margin.Top + Blinkness;

                    if (rectangle.Margin.Top > this.ActualHeight - BarHeightTop)
                    {
                        RemoveList.AddLast(rectangle);
                        if (rectangle.Fill == levelColor)
                        {
                            Die();
                        }
                    }
                    else
                    {
                        rectangle.Margin = new Thickness(rectangle.Margin.Left, y, 0, 0);
                    }
                }

                foreach (Rectangle rectangle in RemoveList)
                {
                    RectangleDestroy(rectangle);
                }
            }


            RectangleCreate();
        }

        private void checkLevel()
        {

            var score = Int32.Parse(levelScoreButtom.Text.Split(':')[1]);
            if (score >= NextLevelScore)
            {
                Level++;
               // Blinkness = Speed;
                this.getSettingsForLevel(Level);
                object sender = new object();
                RoutedEventArgs e = new RoutedEventArgs();
                Pause(sender, e);

                CurrentlevelButton.Text = "Level:" + Level;

                NextLevel.Visibility = Visibility.Visible;
                NextLevelDescription.Padding = new Thickness(0, this.ActualHeight / 3, 0, 0);
                NextLevelDescription.Height = this.ActualHeight;
                NextLevelDescription.Width = this.ActualWidth;
                NextLevelDescription.Text = "Level " + Level;
                //NextLevelButton.Padding = new Thickness(0, ScreenHeight / 2, 0, 0);
                //NextLevelButton.Height = ScreenHeight;
                //NextLevelButton.Width = ScreenWidth;
                NextLevelButton.Visibility = Visibility.Visible;
                PauseButton.Visibility = Visibility.Collapsed;





                lock (RectangleList)
                {
                    RectangleList.Clear();
                    GameArea.Children.Clear();
                }
               // 

            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void RectanglePop(object sender, RoutedEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            lock(RectangleList)
            {
                RectangleList.Remove(rectangle);
            }

            GameArea.Children.Remove(rectangle);
            if (levelColor == rectangle.Fill)
            {
                var score = Int32.Parse(levelScoreButtom.Text.Split(':')[1]);
                levelScoreButtom.Text = "Score:" + (score + PointLevel);
            }
            else
            {
                Die();
            }
                   
        }

        public void Die()
        {
            lives--;
            livesDisplay.Text = "Lives:" + lives;
            if (lives == 0)
            {
                object sender = new object();
                RoutedEventArgs e = new RoutedEventArgs();
                Pause(sender, e);
            PauseButton.Visibility = Visibility.Collapsed;
                PlayButton.Visibility = Visibility.Collapsed;
                GameOver.Visibility = Visibility.Visible;
                GameOverDescription.Padding = new Thickness(0, this.ActualHeight /2,0,0);
                GameOverDescription.Height = this.ActualHeight;
                GameOverDescription.Width = this.ActualWidth;
                //pop up game over
            }

        }

        public void RectangleCreate()
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Tapped += RectanglePop;

            Brush backgroud_color = colors[GenerateSize.Next(0, colors.Count())];
            rectangle.Fill = backgroud_color;

            rectangle.Width = GenerateSize.Next(RectangleWidthMin, RectangleWidthMax);
            rectangle.Height = GenerateSize.Next(RectangleHeightMin, RectangleHeightMax);

            double Left = (double)GenerateSize.Next(0, (int)(this.ActualWidth - rectangle.Width));
            double Top = (double)GenerateSize.Next(0, (int)(RectangleHeightMax - rectangle.Height));
            rectangle.Margin = new Thickness(Left, Top, 0, 0);

            Boolean Create = true;
            lock (RectangleList)
            {
                
                foreach (Rectangle otherRectangle in RectangleList)
                {
                    if( (otherRectangle.Margin.Top > (rectangle.Margin.Top + rectangle.Height)) ||
                        (otherRectangle.Margin.Left > (rectangle.Margin.Left + rectangle.Width)) ||
                        (rectangle.Margin.Top > (otherRectangle.Margin.Top + otherRectangle.Height)) ||
                        (rectangle.Margin.Left > (otherRectangle.Margin.Left + otherRectangle.Width))
                      )
                    {
                        // all ok!
                    }
                    else
                    {
                        Create = false;
                        break;
                    }
                }

                if (Create == true)
                {
                    RectangleList.AddLast(rectangle);
                    GameArea.Children.Add(rectangle);
                }
            }
        }
        private void RectangleDestroy(object sender)
        {
            Rectangle rectangle = sender as Rectangle;

            lock (RectangleList)
            {
                RectangleList.Remove(rectangle);
            }

            GameArea.Children.Remove(rectangle);
        }
    }
}