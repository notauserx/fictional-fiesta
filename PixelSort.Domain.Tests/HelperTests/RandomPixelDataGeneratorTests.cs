using Xunit;

namespace PixelSort.Domain.Tests.HelperTests
{
    public class RandomPixelDataGeneratorTests
    {
        [Fact]
        public void Test_Random_Pixel_Is_Not_Null()
        {
            var generator = new RandomPixelGenerator();

            var pixel = generator.GetRandomPixel();

            Assert.NotNull(pixel);
        }
    }
}
