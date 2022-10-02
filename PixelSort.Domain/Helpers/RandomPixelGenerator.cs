using System;
using System.Drawing;
using System.Linq;

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

        public Pixel[] GenerateRandomPixelData(int count, bool useColorClass = true)
        {
            var pixels = new Pixel[count];
            for (int i = 0; i < count; i++)
            {
                pixels[i] = useColorClass 
                    ? GetRandomPixel()
                    : GetRandomPixelFromRandomBytes();
            }
            return pixels;
        }

        public Pixel[] GenerateRandomPixelDataParallelly(int count, bool useColorClass = true)
        {
            return Enumerable.Range(0, count)
                .AsParallel()
                .Select(x => useColorClass
                    ? GetRandomPixel()
                    : GetRandomPixelFromRandomBytes()).ToArray();
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

        public Pixel GetRandomPixelFromRandomBytes()
        {
            return new Pixel(
                    (byte) getRandomByte(),
                    (byte)getRandomByte(),
                    (byte) getRandomByte());
        }

        private int getRandomByte()
        {
            return _random.Next(BYTE_LIMIT);
        }
    }
}
