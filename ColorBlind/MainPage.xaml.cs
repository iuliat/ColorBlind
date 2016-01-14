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
        Boolean PotSaCeapa = false;
       // public Windows.UI.Color c = new Windows.UI.Color();
        
        Random random = new Random(DateTime.Now.Millisecond);
  
        List<Windows.UI.Xaml.Media.SolidColorBrush> colors = new List<Windows.UI.Xaml.Media.SolidColorBrush>();
        LinkedList<Button> AllTheButtons = new LinkedList<Button>();
        //public StackPanel panel;
        Timer Ceapa = null;

        async private void CallMeCeapa(object state)
        {
            
            if(PotSaCeapa)
            {
                Page Victim = state as MainPage;
                await Victim.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, UpdateBula);
            }
        }

        //input ex: #dcdcdc
        public static Windows.UI.Xaml.Media.SolidColorBrush GetColorFromHex(string hexaColor)
        {
            return new Windows.UI.Xaml.Media.SolidColorBrush(
                Windows.UI.Color.FromArgb(
                    255,
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16)
                )
            );
        }

        public void generateColors()
        {
            Windows.UI.Xaml.Media.SolidColorBrush red = GetColorFromHex("#FFD60A0A");
            Windows.UI.Xaml.Media.SolidColorBrush yellow = GetColorFromHex("#FFFFF824");
            Windows.UI.Xaml.Media.SolidColorBrush purple = GetColorFromHex("#FF9C2167");
            Windows.UI.Xaml.Media.SolidColorBrush blue = GetColorFromHex("#FF0F538E");

            Windows.UI.Xaml.Media.SolidColorBrush green = GetColorFromHex("#FF005710");

            colors.Add(red);
            colors.Add(yellow);
            colors.Add(purple);
            colors.Add(blue);
            colors.Add(green);
        }

        void UpdateBula()
        {
            int length = AllTheButtons.Count();
            if (length != 0)
            {
                for (int i = length - 1; i >= 0; i--)
                {
                    Button b = AllTheButtons.ElementAt(i);
                    double y = b.Margin.Top + 1;
                    if (y <= 500)
                    {
                       // b.Content = "New " + y;
                        // 
                        b.Margin = new Thickness(b.Margin.Left, y, 0, 0);
                    }
                    else if(y>500)
                    {
                        //RoutedEventArgs e = null;
                        //button_MyClick(b, e);
                        //generate_obj();
                        grid.Children.Remove(b);

                    }
                }

            }
            //foreach (Button b in AllTheButtons)
            //{

            //    double y = b.Margin.Top + 1;
            //    if (y <= 500)
            //    {
            //        b.Content = "New ";
            //        b.Margin = new Thickness(b.Margin.Left, y, 0, 0);//try this if you use grid
            //                                                   //                        y++;
            //                                                   //                        b.UpdateLayout();

            //        // grid.LayoutUpdated += button_LayoutUpdated;
            //    }
            //    else
            //    {

            //        panel.Children.Remove(b);
            //        generate_obj();
            //    }
            //}
        }

        public MainPage()
        {
            Ceapa = new Timer(CallMeCeapa, this, 0, 30);
            generateColors();

            //panel = new StackPanel();
            //panel.Orientation = Orientation.Vertical;
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void button_MyClick(object sender, RoutedEventArgs e)
        {
            Button Whom = sender as Button;
            grid.Children.Remove(Whom);
            textBlock.Text = Whom.Content.ToString();
            //generate_obj();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            PotSaCeapa = false;
            {
                int i;
                for (i = 0; i < 5; i++)
                {
                    generate_obj();
                }
            };
            PotSaCeapa = true;
        }

        //public void generate_obj(double positionX, double positionY) {
        public void generate_obj()
        {
            Button btn = new Button() { Content = "Button " };
            btn.Click += button_MyClick;

            Brush backgroud_color = colors[random.Next(0, colors.Count())];
            btn.Background = backgroud_color;
            btn.Foreground = backgroud_color;
            

            btn.Width = random.Next(1, 10) * 20;
            btn.Height = random.Next(1, 10) * 10;
            double positionX = (double)random.Next(0, (int)(grid.ActualWidth - btn.Width));
            double positionY = (double)random.Next(-100, -10);
            btn.Margin = new Thickness(positionX, positionY, 0, 0);//try this if you use grid

            //int length = AllTheButtons.Count();
            //Boolean good = false;
            //foreach (Button b in AllTheButtons)
            //{
            // // check for button to not overlap the other existent buttons
            //}
            
            AllTheButtons.AddFirst(btn);
            grid.Children.Add(btn);
        }


    }
}
