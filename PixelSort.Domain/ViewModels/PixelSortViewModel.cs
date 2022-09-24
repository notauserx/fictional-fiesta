using System;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public class PixelSortViewModel
    {
        private int width;
        private int height;
        private int dpi;
        private int pixelSize;
        private Pixel[] pixels;

        public PixelSortViewModel(int width, int height)
        {
            this.width = width;
            this.height = height;
            dpi = 96;
            pixelSize = 4;
        }

        public BitmapSource GetBitmapSourceFromRandomData()
        {
            pixels = RandomColorDataGenerator.GenerateRandomPixelData(width * height);
            return generateBitmapSourceFromColors(pixels);
        }

        public bool IsColorsEmpty() => pixels is null;

        public BitmapSource GetBitmapSourceFromSortedData()
        {
            if(pixels is null)
            {
                throw new Exception("Cannot sort data befor data is set");
            }

            // TODO :: if colors are already sorted, not need to sort again.
            //Array.Sort(colors, (a, b) => a.GetHue().CompareTo(b.GetHue()));
            var sorted = pixels.OrderBy(x => x.Hue).ToArray();

            return generateBitmapSourceFromColors(sorted);
        }

        private BitmapSource generateBitmapSourceFromColors(Pixel[] pixels)
        {
            byte[] pixelData = new PixelConverter(width, height)
                .GetTransposedPixelsFromArgbColors(pixels);

            return BitmapSource.Create(width, height, dpi, dpi,
                System.Windows.Media.PixelFormats.Bgr32, null, pixelData, width * pixelSize);
        }
    }
}
