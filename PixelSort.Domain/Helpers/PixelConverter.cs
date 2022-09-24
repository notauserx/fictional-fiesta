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

            pixelSize = 4; 
        }

        public byte[] GetTransposedPixelsFromArgbColors(Pixel[] pixels)
        {
            var pixelData = new byte[height * width * pixelSize];

            var row = 0;
            var col = 0;
            foreach (var pixel in pixels)
            {
                if (row == height)
                {
                    row = 0;
                    col++;
                }
                var rotatedIndex = row * width * pixelSize + col * pixelSize;

                pixelData[rotatedIndex]     = pixel.Blue;   // Blue
                pixelData[rotatedIndex + 1] = pixel.Green;  // Green
                pixelData[rotatedIndex + 2] = pixel.Red;    // Red
                pixelData[rotatedIndex + 3] = pixel.Alpha;  // Alpha


                row++;
            }

            return pixelData;
        }
    }
}
