namespace PixelSort.Domain
{
    public class PixelService : IPixelService
    {
        private readonly RandomPixelGenerator randomPixelGenerator;
        private readonly IPixelSorter pixelSorter;

        private Pixel[] pixels;
        private bool isSorted;

        public PixelService(RandomPixelGenerator randomPixelGenerator, IPixelSorter pixelSorter)
        {
            this.randomPixelGenerator = randomPixelGenerator;
            this.pixelSorter = pixelSorter;

            isSorted = false;
        }

        public bool ArePixelsEmpty() => pixels is null;

        public bool ArePixelsSorted() => isSorted;

        public Pixel[] GenerateRandomPixelData(int count, bool useColorClass = true)
        {
            isSorted = false;
            pixels = new Pixel[count];
            for (int i = 0; i < count; i++)
            {
                pixels[i] = useColorClass 
                    ? randomPixelGenerator.GetRandomPixel()
                    : randomPixelGenerator.GetRandomPixelFromRandomBytes();
            }
            return pixels;
        }

        public Pixel[] GetSortedPixels()
        {
            isSorted = true;
            return pixelSorter.GetSortedPixels(pixels);
        }
    }
}
