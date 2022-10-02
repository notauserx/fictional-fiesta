using BenchmarkDotNet.Running;
using System;

namespace PixelSort.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<BucketSortVsOthers>();
            var summary = BenchmarkRunner.Run<PixelFromColorsvsPixelFromBytes>();
        }
    }
}
