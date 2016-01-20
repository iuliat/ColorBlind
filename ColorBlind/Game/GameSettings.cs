using System.Collections.Generic;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Controls;

namespace ColorBlind
{
    public sealed partial class GamePage : Page
    {
        //        public readonly DisplayOrientations InitialOrientation = DisplayProperties.CurrentOrientation;

        private readonly int SlownessDefault = 20;
        private readonly int BlinknessDefault = 1;
        private int Slowness = 32;
        private int Blinkness = 1;
       private readonly int HardshipTime = 10000;

      //  public readonly int thisHeight = 640;
     //   public readonly int thisWidth = 400;
        public int RectangleHeightMax
        {
            get
            {
                return System.Math.Min(BarHeightTop, BarHeightBottom);
            }
            private set { }
        }
        public readonly int RectangleHeightMin = 50;
        public readonly int RectangleWidthMax = 200;
        public readonly int RectangleWidthMin = 40;
        public readonly int BarHeightTop = 85;
        public readonly int BarHeightBottom = 85;

        List<Windows.UI.Xaml.Media.SolidColorBrush> colors = new List<Windows.UI.Xaml.Media.SolidColorBrush>();
    }
}
