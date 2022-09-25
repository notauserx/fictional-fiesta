using System;
using Xunit;
using Xunit.Abstractions;

namespace PixelSort.Domain.Tests.Benchmarks
{
    public class SortingBenchMarks
    {
        private readonly ITestOutputHelper output;

        public SortingBenchMarks(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData(400, 300)]
        [InlineData(1024, 768)]
        [InlineData(1920, 1080)]
        public void Compare_Sorting_Times(int width, int height)
        {
            var randomPixels = new RandomPixelDataGenerator().GenerateRandomPixelData(width * height);


            var timeForBucketSortScaled100 = getElapsedMiliSeconds(() =>
                _ = new BucketSortPixelSorter().GetSortedPixels(randomPixels));

            output.WriteLine($"Bucket sort scaled 100 Time for {width} and {height} = {timeForBucketSortScaled100}");

            var timeForBucketSortScaled10 = getElapsedMiliSeconds(() =>
                _ = new BucketSortPixelSorter(10).GetSortedPixels(randomPixels));

            output.WriteLine($"Bucket sort scaled 10 Time for {width} and {height} = {timeForBucketSortScaled10}");

            var timeForBucketSortScaled1 = getElapsedMiliSeconds(() =>
                _ = new BucketSortPixelSorter(1).GetSortedPixels(randomPixels));

            output.WriteLine($"Bucket sort scaled 1 Time for {width} and {height} = {timeForBucketSortScaled1}");

            var timeForLinqSort = getElapsedMiliSeconds(() =>
                _ = new LinqOrderByPixelSorter().GetSortedPixels(randomPixels));

            output.WriteLine($"Linq orderby sort Time for {width} and {height} = {timeForLinqSort}");

            var timeForArraySort = getElapsedMiliSeconds(() =>
                _ = new ArraySortPixelSorter().GetSortedPixels(randomPixels));

            output.WriteLine($"Array.Sort sort Time for {width} and {height} = {timeForArraySort}");

        }

        private long getElapsedMiliSeconds(Action action)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            action();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

    }
}
