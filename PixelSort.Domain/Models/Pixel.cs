using System.Drawing;

namespace PixelSort.Domain
{
    public struct Pixel
    {
        public Pixel(byte red, byte green, byte blue, byte alpha = 255)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
            Hue = 0f;
            Hue = GetHue();

        }
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
        public byte Red { get; private set; }
        public byte Green { get; private set; }
        public byte Blue { get; private set; }  
        public byte Alpha { get; private set; }

        public float Hue { get; private set; }


        private float GetHue()
        {

            if (Red == Green && Green == Blue)
                return 0f;

            MinMaxRgb(out int min, out int max, Red, Green, Blue);

            float delta = max - min;
            float hue;

            if (Red == max)
                hue = (Green - Blue) / delta;
            else if (Green == max)
                hue = (Blue - Red) / delta + 2f;
            else
                hue = (Red - Green) / delta + 4f;

            hue *= 60f;
            if (hue < 0f)
                hue += 360f;

            return hue;
        }

        private static void MinMaxRgb(out int min, out int max, int r, int g, int b)
        {
            if (r > g)
            {
                max = r;
                min = g;
            }
            else
            {
                max = g;
                min = r;
            }
            if (b > max)
            {
                max = b;
            }
            else if (b < min)
            {
                min = b;
            }
        }
    }
}
