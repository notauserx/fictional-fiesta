using System;
using System.Drawing;
using System.Threading.Tasks;

namespace PixelSort.Domain
{
    public class RandomColorDataGenerator
    {
        private static readonly Random _random = new Random();
        private static int BYTE_LIMIT = 256;

        public static Color[] GenerateRandomColorData(int width, int height)
        {
            Color[] colors = new Color[width * height];
            var index = 0;
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    colors[index++] =
                        Color.FromArgb(
                            getRandomByte(),
                            getRandomByte(),
                            getRandomByte(),
                            getRandomByte());
                }
            }

            return colors;
        }

        public static Pixel[] GenerateRandomPixelData(int count)
        {
            var pixels = new Pixel[count];
            for (int i = 0; i < count; i++)
            {
                pixels[i] = getRandomPixel();
            }
            return pixels;
        }

        private static Pixel getRandomPixel()
        {
            return new Pixel(
                Color.FromArgb(
                    getRandomByte(),
                    getRandomByte(),
                    getRandomByte(),
                    getRandomByte()));
        }

        private static int getRandomByte()
        {
            return _random.Next(BYTE_LIMIT);
        }
    }
}
