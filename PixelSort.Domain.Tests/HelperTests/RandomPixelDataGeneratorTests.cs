using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PixelSort.Domain.Tests.HelperTests
{
    public class RandomPixelDataGeneratorTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(1)]
        [InlineData(0)]
        public void Test_Random_Pixel_Size(int count)
        {
            var generator = new RandomPixelDataGenerator();

            Assert.Equal(count, generator.GenerateRandomPixelData(count).Length);
        }
    }
}
