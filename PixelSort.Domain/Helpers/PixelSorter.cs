using System;
using System.Collections.Generic;
using System.Linq;

namespace PixelSort.Domain
{
    public interface IPixelSorter
    {
        Pixel[] GetSortedPixels(Pixel[] pixels);
    }

    public class LinqOrderByPixelSorter : IPixelSorter
    {
        public Pixel[] GetSortedPixels(Pixel[] pixels)
        {
            return pixels.OrderBy(x => x.Hue).ToArray();
        }
    }

    public class ArraySortPixelSorter : IPixelSorter
    {
        public Pixel[] GetSortedPixels(Pixel[] pixels)
        {
            Array.Sort(pixels, (p1, p2) => p1.Hue.CompareTo(p2.Hue));
            return pixels;
        }
    }

    public class BucketSortPixelSorter : IPixelSorter
    {
        public Pixel[] GetSortedPixels(Pixel[] pixels)
        {
            var pixelBuckets = PutPixelsInBuckets(pixels, 36000);
            

            return SortPixelsInBuckets(pixelBuckets).SelectMany(s => s).ToArray();
        }

        public List<Pixel>[] SortPixelsInBuckets(List<Pixel>[] pixelBuckets)
        {
            
            foreach(var pixelBucket in pixelBuckets)
            {
                pixelBucket.Sort((a, b) => a.Hue.CompareTo(b.Hue));
            }

            return pixelBuckets;
        }

        public List<Pixel>[] PutPixelsInBuckets(Pixel[] pixels, int numberOfBuckets)
        {
            List<Pixel>[] pixelBuckets = new List<Pixel>[numberOfBuckets];

            for (int i = 0; i < numberOfBuckets; i++)
                pixelBuckets[i] = new List<Pixel>();

            foreach (var pixel in pixels)
            {
                var bucketIndex = (int)(pixel.Hue * 100);
                pixelBuckets[bucketIndex].Add(pixel);
            }
            
            return pixelBuckets;
        }
    }

}
