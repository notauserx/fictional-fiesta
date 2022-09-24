using System;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public class PixelSortViewModel
    {
        private Pixel[] pixels;

        private int width;
        private int height;
        private int dpi;
        private int pixelSize;
        private bool isSorted;

        public PixelSortViewModel(int width, int height)
        {
            this.width = width;
            this.height = height;
            dpi = 96;
            pixelSize = 4;
        }

        public bool ArePixelsEmpty() => pixels is null;
        public bool ArePixelsSorted() => isSorted;

        public BitmapSource GetBitmapSourceFromRandomData()
        {
            pixels = RandomColorDataGenerator.GenerateRandomPixelData(width * height);
            isSorted = false;
            
            return generateBitmapSourceFromColors(pixels);
        }

        public BitmapSource GetBitmapSourceFromSortedData()
        {
            if (pixels is null)
            {
                throw new Exception("Can not sort pixels before pixels are generated.");
            }

            if (isSorted is false)
            {
                SortPixels();
                isSorted = true;
            }

            return generateBitmapSourceFromColors(pixels);
        }

        private void SortPixels()
        {
            pixels = pixels.OrderBy(x => x.Hue).ToArray();
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
