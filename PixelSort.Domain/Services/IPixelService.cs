namespace PixelSort.Domain
{
    public interface IPixelService
    {
        bool ArePixelsEmpty();
        Pixel[] GenerateRandomPixelData(int count, bool useColorClass = true);
        Pixel[] GetSortedPixels();
        bool ArePixelsSorted();
    }
}
