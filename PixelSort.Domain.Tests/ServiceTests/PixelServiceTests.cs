using Xunit;

namespace PixelSort.Domain.Tests.Services
{
    public class PixelServiceTests
    {
        private IPixelService GetPixelService()
        {
            IPixelService service = new PixelService(
                new RandomPixelGenerator(),
                new BucketSortPixelSorter());

            return service;
        }


        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(1)]
        [InlineData(0)]
        public void Test_Random_Pixel_Generation_Size(int count)
        {
            var service = GetPixelService();

            var randomPixels = service.GenerateRandomPixelData(count);

            Assert.Equal(count, randomPixels.Length);

        }

        [Fact]
        public void Test_Pixels_Are_Empty_Initially()
        {
            var service = GetPixelService();

            Assert.True(service.ArePixelsEmpty());
        }

        [Fact]
        public void Test_Pixels_Are_Not_Empty_After_GenerateRandomPixelData()
        {
            var service = GetPixelService();

            Assert.True(service.ArePixelsEmpty());

            var _ = service.GenerateRandomPixelData(10);
            Assert.False(service.ArePixelsEmpty());

        }

        [Fact]
        public void Test_Pixels_Are_Not_Sorted_After_GenerateRandomPixelData()
        {
            var service = GetPixelService();

            Assert.True(service.ArePixelsEmpty());

            service.GenerateRandomPixelData(10);
            Assert.False(service.ArePixelsSorted());

        }

        [Fact]
        public void Test_Pixels_Are_Sorted_After_GetSortedPixels()
        {
            var service = GetPixelService();

            Assert.True(service.ArePixelsEmpty());

            service.GenerateRandomPixelData(10);
            Assert.False(service.ArePixelsSorted());

            service.GetSortedPixels();
            Assert.True(service.ArePixelsSorted());

        }

        [Fact]
        public void Test_Pixels_Are_Not_Sorted_When_GenerateRandomPixelData_is_called_again()
        {
            var service = GetPixelService();

            Assert.True(service.ArePixelsEmpty());

            service.GenerateRandomPixelData(10);
            Assert.False(service.ArePixelsSorted());

            service.GetSortedPixels();
            Assert.True(service.ArePixelsSorted());

            service.GenerateRandomPixelData(10);
            Assert.False(service.ArePixelsSorted());
        }
    }
}
