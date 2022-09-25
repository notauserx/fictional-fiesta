using System;
using System.Drawing;

namespace PixelSort.Domain
{
    public  class RandomPixelDataGenerator
    {
        private readonly Random _random;
        private readonly int BYTE_LIMIT = 256;

        public  RandomPixelDataGenerator() 
            : this(new Random()) { }

        public RandomPixelDataGenerator(Random random)
        {
            _random = random;
        }
        
        public Pixel[] GenerateRandomPixelData(int count)
        {
            var pixels = new Pixel[count];
            for (int i = 0; i < count; i++)
            {
                pixels[i] = getRandomPixel();
            }
            return pixels;
        }

        private Pixel getRandomPixel()
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
