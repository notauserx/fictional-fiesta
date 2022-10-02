using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using PixelSort.Domain;
using System.Collections.Generic;

namespace PixelSort.Benchmarks
{
    [MemoryDiagnoser]
    [MarkdownExporter, AsciiDocExporter, RPlotExporter]
    public class PixelFromColorsvsPixelFromBytes
    {
        IEnumerable<int> data;
        public PixelFromColorsvsPixelFromBytes()
        {
            data = new List<int>
            {
                 400 * 300,
                1024 * 768,
                1920 * 1080
            };
        }
        public IEnumerable<int> Data() => data;
        
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Pixel[] GeneratePixelDataFromColors(int count)
        {
            var generator = new RandomPixelGenerator();
            return generator.GenerateRandomPixelData(count);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Pixel[] GeneratePixelDataFromBytes(int count)
        {
            var generator = new RandomPixelGenerator();
            return generator.GenerateRandomPixelData(count, false);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Pixel[] GeneratePixelDataFromColorsParallelly(int count)
        {
            var generator = new RandomPixelGenerator();
            return generator.GenerateRandomPixelDataParallelly(count);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Pixel[] GeneratePixelDataFromBytesParallelly(int count)
        {
            var generator = new RandomPixelGenerator();
            return generator.GenerateRandomPixelDataParallelly(count, false);
        }
    }
}
