using System;
using System.Drawing;
using System.Threading.Tasks;

namespace PixelSort.Domain
{
    public class RandomPixelDataGenerator
    {
        private static readonly Random _random = new Random();
        private static readonly int BYTE_LIMIT = 256;


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
