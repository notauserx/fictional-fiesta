using System.Drawing;

namespace PixelSort.Domain
{
    public class PixelConverter
    {
        private int height;
        private int width;
        private int pixelSize;
        public PixelConverter(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.pixelSize = 4; 
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
