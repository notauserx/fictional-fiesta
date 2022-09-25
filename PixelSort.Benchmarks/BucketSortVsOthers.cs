using BenchmarkDotNet.Attributes;
using PixelSort.Domain;
using System.Collections.Generic;

namespace PixelSort.Benchmarks
{
    public class BucketSortVsOthers
    {

        public IEnumerable<Pixel[]> Data()
        {
            yield return
                new RandomPixelDataGenerator().GenerateRandomPixelData(400 * 300);
            yield return
                new RandomPixelDataGenerator().GenerateRandomPixelData(1024 * 768);
            yield return
                new RandomPixelDataGenerator().GenerateRandomPixelData(1920 * 1080);

        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Pixel[] BucketSort(Pixel[] pixels)
        {

            var sorter = new BucketSortPixelSorter();
            return sorter.GetSortedPixels(pixels);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Pixel[] ArraySort(Pixel[] pixels)
        {
            var sorter = new ArraySortPixelSorter();
            return sorter.GetSortedPixels(pixels);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Pixel[] LinqSort(Pixel[] pixels)
        {
            var sorter = new LinqOrderByPixelSorter();
            return sorter.GetSortedPixels(pixels);
        }
    }
}
