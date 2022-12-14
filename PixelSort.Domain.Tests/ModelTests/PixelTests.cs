using System.Drawing;
using Xunit;

namespace PixelSort.Domain.Tests.ModelTests
{
    public class PixelTests
    {
        [Fact]
        public void Test_Pixel_RedValue()
        {
            var color = Color.FromArgb(0, 11, 0, 0);
            var pixel  = new Pixel(color);

            Assert.Equal(11, pixel.Red);
        }

        [Fact]
        public void Test_Pixel_GreenValue()
        {
            var color = Color.FromArgb(0, 0, 11, 0);
            var pixel = new Pixel(color);

            Assert.Equal(11, pixel.Green);
        }

        [Fact]
        public void Test_Pixel_BlueValue()
        {
            var color = Color.FromArgb(0, 0, 0, 11);
            var pixel = new Pixel(color);

            Assert.Equal(11, pixel.Blue);
        }

        [Fact]
        public void Test_Pixel_AlphaValue()
        {
            var color = Color.FromArgb(11, 0, 0, 0);
            var pixel = new Pixel(color);

            Assert.Equal(11, pixel.Alpha);
        }


        [Theory]
        [InlineData(255, 255, 255, 255)]
        [InlineData(255, 255, 255, 0  )]
        [InlineData(255, 255, 0  , 255)]
        [InlineData(255, 0  , 255, 255)]
        [InlineData(0  , 255, 255, 255)]
        [InlineData(0  , 0  , 0  , 0  )]
        [InlineData(100, 100, 100, 100)]
        public void Test_Pixel_HueValue_SameAs_GetHue(
            int a, int r, int g, int b)
        {
            var color = Color.FromArgb(a, r, g, b);
            var expected = color.GetHue();
            var pixel = new Pixel(color);

            Assert.Equal(expected, pixel.Hue);
        }

        [Theory]
        [InlineData(255, 255, 255, 255)]
        [InlineData(255, 255, 255, 0)]
        [InlineData(255, 255, 0, 255)]
        [InlineData(255, 0, 255, 255)]
        [InlineData(0, 255, 255, 255)]
        [InlineData(0, 0, 0, 0)]
        [InlineData(100, 100, 100, 100)]
        public void Test_Hue_is_between_0_and_360(int a, int r, int g, int b)
        {
            var color = Color.FromArgb(a, r, g, b);
            var expected = color.GetHue();

            Assert.True(expected >= 0);
            Assert.True(expected <= 360f);
        }
    }
}
