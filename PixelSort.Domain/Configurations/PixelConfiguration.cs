namespace PixelSort.Domain
{
    public class PixelConfiguration : IPixelConfiguraiton
    {
        public PixelConfiguration(int width, int height, int pixelSize, int dpi)
        {
            Width = width;
            Height = height;
            PixelSize = pixelSize;
            Dpi = dpi;
        }

        public int Width { get; }
        
        public int Height { get; }

        public int PixelSize { get; }

        public int Dpi { get; }

        public static PixelConfiguration DefaultConfiguration()
        {
            return new PixelConfiguration(1024, 768, 4, 96);
        }
    }
}
