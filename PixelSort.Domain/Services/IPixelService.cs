namespace PixelSort.Domain
{
    public interface IPixelService
    {
        bool ArePixelsEmpty();
        Pixel[] GenerateRandomPixelData(int count);
        Pixel[] GetSortedPixels();
        bool ArePixelsSorted();
    }
}
