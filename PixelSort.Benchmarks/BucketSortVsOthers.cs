using BenchmarkDotNet.Attributes;
using PixelSort.Domain;
using System.Collections.Generic;

namespace PixelSort.Benchmarks
{
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    public class BucketSortVsOthers
    {
        IEnumerable<Pixel[]> data;
        public BucketSortVsOthers()
        {
            data = new List<Pixel[]>()
            {
                new RandomPixelGenerator().GenerateRandomPixelData(400  * 300),
                new RandomPixelGenerator().GenerateRandomPixelData(1024 * 768),
                new RandomPixelGenerator().GenerateRandomPixelData(1920 * 1080)
            };
        }

        public IEnumerable<Pixel[]> Data() => data;

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Pixel[] BucketSortScaled100(Pixel[] pixels)
        {

            var sorter = new BucketSortPixelSorter();
            return sorter.GetSortedPixels(pixels);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Pixel[] BucketSortScaled10(Pixel[] pixels)
        {

            var sorter = new BucketSortPixelSorter(10);
            return sorter.GetSortedPixels(pixels);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Pixel[] BucketSortScaled1(Pixel[] pixels)
        {

            var sorter = new BucketSortPixelSorter(1);
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
