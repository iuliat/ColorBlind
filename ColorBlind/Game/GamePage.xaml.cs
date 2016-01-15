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

        private void DrawCallback()
        {
            LinkedList<Rectangle> RemoveList = new LinkedList<Rectangle>();

            lock (RectangleList)
            {
                foreach (Rectangle rectangle in RectangleList)
                {
                    double y = rectangle.Margin.Top + Blinkness;

                    if (rectangle.Margin.Top > ScreenHeight)
                    {
                        RemoveList.AddLast(rectangle);
                    }
                    else
                    {
                        rectangle.Margin = new Thickness(rectangle.Margin.Left, y, 0, 0);
                    }
                }

                foreach (Rectangle rectangle in RemoveList)
                {
                    /*GameArea.Children.Remove(rectangle);
                    RectangleList.Remove(rectangle);*/
                    RectangleDestroy(rectangle);
                }
            }


            RectangleCreate();
        }

        public GamePage()
        {
            this.generateColors();

            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
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
            int totalScore = 0;

            foreach(Button colorScore in ScoreBoard.Children)
            {
                if(colorScore.Background.Equals(rectangle.Fill))
                {
                    if(colorScore.FontStyle == Windows.UI.Text.FontStyle.Oblique)
                    {
                        colorScore.Content = "" + (Int32.Parse(colorScore.Content.ToString()) + 10);
                    }
                    else
                    {
                        colorScore.Content = "" + (Int32.Parse(colorScore.Content.ToString()) - 20);
                    }

                    totalScore = totalScore + Int32.Parse(colorScore.Content.ToString());
                }
                else
                {

                    if(colorScore.Background == colorTotal)
                    {
                        colorScore.Content = "" + totalScore;
                    }
                    else
                    {
                        totalScore = totalScore + Int32.Parse(colorScore.Content.ToString());
                    }
                }
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

            double Left = (double)GenerateSize.Next(0, (int)(ScreenWidth - rectangle.Width));
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
            int totalScore = 0;

            foreach (Button colorScore in ScoreBoard.Children)
            {
                if (colorScore.Background.Equals(rectangle.Fill))
                {
                    if (colorScore.FontStyle == Windows.UI.Text.FontStyle.Oblique)
                    {
                        colorScore.Content = "" + (Int32.Parse(colorScore.Content.ToString()) - 50);
                    }
                    else
                    {
                        colorScore.Content = "" + (Int32.Parse(colorScore.Content.ToString()) + 1);
                    }

                    totalScore = totalScore + Int32.Parse(colorScore.Content.ToString());
                }
                else
                {

                    if (colorScore.Background == colorTotal)
                    {
                        colorScore.Content = "" + totalScore;
                    }
                    else
                    {
                        totalScore = totalScore + Int32.Parse(colorScore.Content.ToString());
                    }
                }
            }
        }
    }
}
