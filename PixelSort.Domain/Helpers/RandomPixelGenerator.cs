using System;
using System.Drawing;

namespace PixelSort.Domain
{
    public  class RandomPixelGenerator
    {
        private readonly Random _random;
        private readonly int BYTE_LIMIT = 256;

        public  RandomPixelGenerator() 
            : this(new Random()) { }

        public RandomPixelGenerator(Random random)
        {
            _random = random;
        }
        
        public Pixel[] GenerateRandomPixelData(int count)
        {
            var pixels = new Pixel[count];
            for (int i = 0; i < count; i++)
            {
                pixels[i] = GetRandomPixel();
            }
            return pixels;
        }

        public Pixel GetRandomPixel()
        {
            return new Pixel(
                Color.FromArgb(
                    getRandomByte(),
                    getRandomByte(),
                    getRandomByte(),
                    getRandomByte()));
        }

        private int getRandomByte()
        {
            return _random.Next(BYTE_LIMIT);
        }
    }
}
