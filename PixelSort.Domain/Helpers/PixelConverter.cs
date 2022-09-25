namespace PixelSort.Domain
{
    public class PixelConverter
    {
        private readonly IPixelConfiguraiton config;

        public PixelConverter(IPixelConfiguraiton pixelConfiguraiton)
        {
            config = pixelConfiguraiton;
        }

        public byte[] GetTransposedPixelsFromArgbColors(Pixel[] pixels)
        {
            var pixelData = new byte[config.Height * config.Width * config.PixelSize];

            var row = 0;
            var col = 0;
            foreach (var pixel in pixels)
            {
                if (row == config.Height)
                {
                    row = 0;
                    col++;
                }
                var rotatedIndex = row * config.Width * config.PixelSize + col * config.PixelSize;

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
