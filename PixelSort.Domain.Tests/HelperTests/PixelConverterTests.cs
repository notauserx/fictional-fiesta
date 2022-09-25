using System.Drawing;
using System.Linq;
using Xunit;

namespace PixelSort.Domain.Tests
{
    public class PixelConverterTests
    {
        [Theory]
        [InlineData(1, 1, 4)]
        [InlineData(1, 2, 8)]
        [InlineData(2, 4, 32)]
        [InlineData(30, 40, 4800)]
        public void Test_PixelArraySize_From_GetTransposedPixelsFromArgbColors(
            int width, int height, int expectedSize)
        {
            var converter = new PixelConverter(new PixelConfiguration(width, height, 4, 96));

            var colors = new Color[width * height];
            var pixels = colors.Select(c => new Pixel(c)).ToArray();


            for (var i = 0; i < width * height; i++)
            {
                colors[i] = Color.AliceBlue;
            }

            var actual = converter.GetTransposedPixelsFromArgbColors(pixels);

            Assert.Equal(expectedSize, actual.Length);
        }

        [Fact]
        public void Test_GetTransposedPixelsFromArgbColors_Arranges_Pixels_Correctly()
        {
            var converter = new PixelConverter(new PixelConfiguration(4, 3, 4, 96));

            var colors = new Color[]
            {
                Color.Aqua, Color.Aqua, Color.Aqua,
                Color.Blue, Color.Blue, Color.Blue,
                Color.Orange,Color.Orange,Color.Orange,
                Color.Orchid, Color.Orchid,Color.Orchid,
            };

            var pixels = colors.Select(c => new Pixel(c)).ToArray();

            var actual = converter.GetTransposedPixelsFromArgbColors(pixels);

            var expected = new byte[]
            {
                Color.Aqua.B,Color.Aqua.G,Color.Aqua.R,Color.Aqua.A,
                Color.Blue.B,Color.Blue.G,Color.Blue.R,Color.Blue.A,
                Color.Orange.B, Color.Orange.G, Color.Orange.R,Color.Orange.A,
                Color.Orchid.B,Color.Orchid.G,Color.Orchid.R,Color.Orchid.A,


                Color.Aqua.B,Color.Aqua.G,Color.Aqua.R,Color.Aqua.A,
                Color.Blue.B,Color.Blue.G,Color.Blue.R,Color.Blue.A,
                Color.Orange.B, Color.Orange.G, Color.Orange.R,Color.Orange.A,
                Color.Orchid.B,Color.Orchid.G,Color.Orchid.R,Color.Orchid.A,

                Color.Aqua.B,Color.Aqua.G,Color.Aqua.R,Color.Aqua.A,
                Color.Blue.B,Color.Blue.G,Color.Blue.R,Color.Blue.A,
                Color.Orange.B, Color.Orange.G, Color.Orange.R,Color.Orange.A,
                Color.Orchid.B,Color.Orchid.G,Color.Orchid.R,Color.Orchid.A,
            };

            Assert.Equal(expected, actual);

        }
    }
}
