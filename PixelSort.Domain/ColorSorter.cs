using System;
using System.Drawing;

namespace PixelSort.Domain
{
    public class ColorSorter
    {
        public static Color[] GetSortedColors(Color[] colors)
        {
            Array.Sort(colors, CompareColors);
            return colors;
        }

        private static int CompareColors(Color c1, Color c2)
        {
            return (int) (c1.GetHue() - c2.GetHue());
        }
    }
}
