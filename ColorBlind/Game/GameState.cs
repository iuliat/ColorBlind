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

        private void Reset(object sender, RoutedEventArgs e)
        {
            Stop(sender, e);
            Start(sender, e);
        }
    }
}
