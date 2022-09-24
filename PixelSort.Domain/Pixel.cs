
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
            Hue = BitConverter.ToUInt32(BitConverter.GetBytes(color.GetHue()), 0);
        }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Alpha { get; set; }

        public uint Hue { get; set; }
    }
}
