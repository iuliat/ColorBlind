using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Collections;
using System.Threading;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace ColorBlind
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Random random = new Random(DateTime.Now.Millisecond);

        Color red = new Color("#FFD60A0A");
        Color yellow = new Color("#FFFFF824");
        Color purple = new Color("#FF9C2167");
        Color blue = new Color("#FF0F538E");
        Color green = new Color("#FF40B90A");

        List<Color> colors = new List<Color>();
        List<Button> AllTheButtons = new List<Button>();


        public MainPage()
        {
            colors.Add(red);
            colors.Add(yellow);
            colors.Add(purple);
            colors.Add(blue);
            colors.Add(green);

            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void button_MyClick(object sender, RoutedEventArgs e)
        {
            Button Whom = sender as Button;


            grid.Children.Remove(Whom);
            textBlock.Text = Whom.Content.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /*Dispatcher.BeginInvoke(() =>*/
            {
                int i;
                for (i = 0; i < 5; i++)
                {

                    generate_obj();
                }

                //var c = panel.Children;
                foreach (Button b in AllTheButtons)
                {
                    double y = 0;
                    while (y <= 100)
                    {
                        b.Content = "new" + y;
                        b.Margin = new Thickness(40, y, 0, 0);//try this if you use grid
                        y++;
                    }
                }

                //       Grid.SetRow(control, i);
                //    Grid.SetColumn(control, j);
                // TODO: Add event handler implementation here.
            };
        }

        //public void generate_obj(double positionX, double positionY) {
        public void generate_obj()
        {
            Button btn = new Button() { Content = "Button " };
            AllTheButtons.Add(btn);
            btn.Click += button_MyClick;
            //btn.Width = 130 ;
            //btn.Height = 66;
            btn.Width = random.Next(1, 10) * 20;
            btn.Height = random.Next(1, 10) * 10;

            //Thickness margin = btn.Margin;         
            double positionX = (double)random.Next(0, (int)(grid.ActualWidth - btn.Width));
            //margin.Left = positionX;
            //margin.Right = 0;
            //margin.Bottom = 0;
            //margin.Top = 20;
            //btn.Margin = margin;
            btn.Margin = new Thickness(positionX, 0, 0, 0);//try this if you use grid
            

            grid.Children.Add(btn);
            btn.LayoutUpdated += button_LayoutUpdated;

        }

        private void button_LayoutUpdated(object sender, object e)
        {
            double y = 0;
            //while (y < 5)
            //{
            //    Button btn = new Button() { Content = "Buttonsssssssssssss " };
            //    grid.Children.Add(btn);
            //    y++;
            //}
            Button Whom = sender as Button;


            //grid.Children.Remove(Whom);
            textBlock.Text = Whom.Content.ToString() + y.ToString();
            y++;
        }
    }
}
