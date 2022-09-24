using System.Drawing;
using Xunit;

namespace PixelSort.Domain.Tests
{
    public class PixelConverterTests
    {
        [Fact]
        public void Test_GetTransposedPixelsFromArgbColors_Arranges_Pixels_Correctly()
        {
            var converter = new PixelConverter(3, 4, 4);

            var colors = new Color[]
            {
                Color.Aqua, Color.Aqua, Color.Aqua,
                Color.Blue, Color.Blue, Color.Blue,
                Color.Orange,Color.Orange,Color.Orange,
                Color.Orchid, Color.Orchid,Color.Orchid,
            };

            var actual = converter.GetTransposedPixelsFromArgbColors(colors);

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

            for(var i = 0; i< expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }

        }
    }
}
