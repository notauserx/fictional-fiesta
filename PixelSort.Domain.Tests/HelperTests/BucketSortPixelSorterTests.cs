using System.Linq;
using Xunit;

namespace PixelSort.Domain.Tests.HelperTests
{
    public class BucketSortPixelSorterTests
    {
        [Fact]
        public void Test_BucketSort_On_RandomPixels()
        {
            var randomPixels = new RandomPixelDataGenerator().GenerateRandomPixelData(100);

            var sorted = new BucketSortPixelSorter().GetSortedPixels(randomPixels);

            var expected = randomPixels.OrderBy(x => x.Hue);

            Assert.Equal(expected, sorted);
        }
    }
}
