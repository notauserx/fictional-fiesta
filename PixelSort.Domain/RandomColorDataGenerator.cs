using System;
using System.Drawing;

namespace PixelSort.Domain
{
    public class RandomColorDataGenerator
    {
        private static readonly Random _random = new Random();
        private static int BYTE_LIMIT = 256;

        public static Color[] GenerateRandomColorData(int size)
        {
            Color[] colors = new Color[size];

            for (int i = 0; i < size; i++)
            {
                colors[i] = Color.FromArgb(
                    getRandomByte(),
                    getRandomByte(),
                    getRandomByte(),
                    getRandomByte());
            }

            return colors;
        }

        private static int getRandomByte()
        {
            return _random.Next(BYTE_LIMIT);
        }
    }
}
