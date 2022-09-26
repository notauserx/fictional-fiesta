namespace PixelSort.Domain
{
    public interface IPixelService
    {
        bool ArePixelsEmpty();
        Pixel[] GenerateRandomPixelData(int count);
        Pixel[] GetSortedPixels();
        bool ArePixelsSorted();
    }
    public class PixelService : IPixelService
    {
        private readonly RandomPixelDataGenerator randomPixelGenerator;
        private readonly IPixelSorter pixelSorter;

        private Pixel[] pixels;
        private bool isSorted;

        public PixelService(RandomPixelDataGenerator randomPixelDataGenerator, IPixelSorter pixelSorter)
        {
            randomPixelGenerator = randomPixelDataGenerator;
            this.pixelSorter = pixelSorter;

            isSorted = false;
        }

        public bool ArePixelsEmpty() => pixels is null;

        public bool ArePixelsSorted()
        {
            return isSorted;
        }

        public Pixel[] GenerateRandomPixelData(int count)
        {
            isSorted = false;
            pixels = new Pixel[count];
            for (int i = 0; i < count; i++)
            {
                pixels[i] = randomPixelGenerator.GetRandomPixel();
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
