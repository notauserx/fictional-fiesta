using Xunit;
namespace PixelSort.Domain.Tests.Services
{
    public class WriteableBitmapServiceTests
    {
        private static WriteableBitmapService GetBitmapService(int width, int height, int dpi)
        {
            var pixelConfiuration = new PixelConfiguration(width, height, 4, dpi);
            var bitmapService = new WriteableBitmapService(pixelConfiuration,
                new PixelConverter());
            return bitmapService;
        }

        [Theory]
        [InlineData(10, 10, 100)]
        [InlineData(20, 20, 400)]
        [InlineData(1, 1, 1)]
        public void Test_PixelCount(int width, int height, int expected)
        {
            var bitmapService = GetBitmapService(width, height, 96);

            Assert.Equal(expected, bitmapService.GetPixelCount());
        }

        

        [Fact]
        public void Test_WriteableBitmap_Is_Not_Null()
        {
            var bitmapService = GetBitmapService(3, 4, 96);

            Assert.NotNull(bitmapService.WriteableBitmap);
        }

        [Theory]
        [InlineData(10, 10, 10)]
        [InlineData(300, 400, 96)]
        [InlineData(15, 20, 32)]
        [InlineData(1, 70, 48)]

        public void WriteableBitmap_Is_Set_From_PixelConfiguration(int width, int height, int dpi)
        {
            var bitmapService = GetBitmapService(width, height, dpi);

            var wb = bitmapService.WriteableBitmap;
            Assert.Equal(width, wb.PixelWidth);
            Assert.Equal(height, wb.PixelHeight);
            Assert.Equal(dpi, wb.DpiX);
            Assert.Equal(dpi, wb.DpiY);

        }

    }
}
