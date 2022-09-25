using System;
using System.Drawing;

namespace PixelSort.Domain
{
    public class Pixel
    {
        public Pixel(Color color)
        {
            Red = color.R;
            Green = color.G;
            Blue = color.B;
            Alpha = color.A;
            // TODO :: can we defer this calculation when pixels are getting generated
            // randomly, we only need this for sorting.
            Hue = color.GetHue();
        }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Alpha { get; set; }

        public float Hue { get; set; }
    }
}
