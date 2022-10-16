using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public class WriteableBitmapService 
    {
        private readonly IPixelConfiguraiton config;
        private readonly PixelConverter pixelConverter;

        private WriteableBitmap wb;

        public WriteableBitmapService(IPixelConfiguraiton config, PixelConverter pixelConverter)
        {
            this.config = config;
            this.pixelConverter = pixelConverter;

            wb = new WriteableBitmap(config.Width, config.Height, config.Dpi, config.Dpi, 
                PixelFormats.Bgr32, null);

        }

        public BitmapSource WriteableBitmap
        { 
            get => wb; 
        }


        public int GetPixelCount()
        {
            return wb.PixelWidth * wb.PixelHeight;
        }

        public void UpdateBackBuffer(Pixel[] pixels)
        {
            wb.CopyBytesToBackBuffer(
                pixelConverter
                .GetTransposedPixelsFromArgbColors(pixels, config));
        }

    }
}
