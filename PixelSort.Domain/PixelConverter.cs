﻿using System.Drawing;

namespace PixelSort.Domain
{
    public class PixelConverter
    {
        int height;
        int width;
        int pixelSize;
        public PixelConverter(int height, int width, int pixelSize)
        {
            this.height = height;
            this.width = width;
            this.pixelSize = pixelSize; 
        }
        /// <summary>
        /// Poputates an Array of bytes that represent the colors data as transposed 
        /// pixels..
        /// </summary>
        /// <param name="colorDatas"></param>
        /// <returns>An array of Pixel data</returns>
        public byte[] GetTransposedPixelsFromColors(Color[] colors)
        {
            byte[] pixelData = new byte[height * width * pixelSize];

            var row = 0;
            var col = 0;
            foreach(var color in colors)
            {
                if (row == height)
                {
                    row = 0;
                    col++;
                }
                var rotatedIndex = row * 4 * width + col;
                
                pixelData[rotatedIndex] = color.B; // B
                pixelData[rotatedIndex + width] = color.G; // G
                pixelData[rotatedIndex + 2 * width] = color.R; // R
                pixelData[rotatedIndex + 3 * width] = color.A; // A


                row++;
            }

            return pixelData;
        }

        public byte[] GetTransposedPixelsFromArgbColors(Color[] colors)
        {
            byte[] pixelData = new byte[height * width * pixelSize];

            var row = 0;
            var col = 0;
            foreach (var color in colors)
            {
                if (row == height)
                {
                    row = 0;
                    col++;
                }
                var rotatedIndex = row * width * pixelSize + col * pixelSize;

                pixelData[rotatedIndex]     = color.B; // B
                pixelData[rotatedIndex + 1] = color.G; // G
                pixelData[rotatedIndex + 2] = color.R; // R
                pixelData[rotatedIndex + 3] = color.A; // A


                row++;
            }

            return pixelData;
        }
    }
}
