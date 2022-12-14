using System.Linq;
using Xunit;

namespace PixelSort.Domain.Tests.HelperTests
{
    public class BucketSortPixelSorterTests
    {
        [Fact]
        public void Test_BucketSort_On_RandomPixels()
        {
            var randomPixels = new RandomPixelGenerator().GenerateRandomPixelData(100);

            var sorted = new BucketSortPixelSorter().GetSortedPixels(randomPixels);

            var expected = randomPixels.OrderBy(x => x.Hue);

            Assert.Equal(expected, sorted);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(50)]
        [InlineData(10)]
        [InlineData(1)]
        public void Test_BucketSort_With_ScalingFactors(int scalingFactor)
        {
            var randomPixels = new RandomPixelGenerator().GenerateRandomPixelData(100);

            var sorted = new BucketSortPixelSorter(scalingFactor).GetSortedPixels(randomPixels);

            var expected = randomPixels.OrderBy(x => x.Hue);

            Assert.Equal(expected, sorted);
        }

    }
}
