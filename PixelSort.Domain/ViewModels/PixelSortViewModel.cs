﻿using System;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public class PixelSortViewModel
    {
        private int width;
        private int height;
        private int dpi;
        private int pixelSize;
        private Color[] colors;
        public PixelSortViewModel(int width, int height)
        {
            this.width = width;
            this.height = height;
            dpi = 96;
            pixelSize = 4;
        }

        public BitmapSource GetBitmapSourceFromRandomData()
        {
            colors = RandomColorDataGenerator.GenerateRandomColorData(width, height);
            return generateBitmapSourceFromColors(colors);
        }

        public bool IsColorsEmpty() => colors is null;

        public BitmapSource GetBitmapSourceFromSortedData()
        {
            if(colors is null)
            {
                throw new Exception("Cannot sort data befor data is set");
            }
            var sortedColors = ColorSorter.GetSortedColors(colors);
            return generateBitmapSourceFromColors(sortedColors);
        }

        private BitmapSource generateBitmapSourceFromColors(Color[] colors)
        {
            byte[] pixelData = new PixelConverter(height, width)
                .GetTransposedPixelsFromArgbColors(colors);

            return BitmapSource.Create(width, height, dpi, dpi,
                System.Windows.Media.PixelFormats.Bgr32, null, pixelData, width * pixelSize);
        }
    }
}
