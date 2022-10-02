
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2006/21H2/November2021Update)
Intel Core i5-4590 CPU 3.30GHz (Haswell), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.302
  [Host]     : .NET Core 3.1.26 (CoreCLR 4.700.22.26002, CoreFX 4.700.22.26801), X64 RyuJIT AVX2
  DefaultJob : .NET Core 3.1.26 (CoreCLR 4.700.22.26002, CoreFX 4.700.22.26801), X64 RyuJIT AVX2


                                Method |   count |       Mean |     Error |    StdDev |      Gen0 |      Gen1 |      Gen2 |   Allocated |
-------------------------------------- |-------- |-----------:|----------:|----------:|----------:|----------:|----------:|------------:|
           **GeneratePixelDataFromColors** |  **120000** |  **11.207 ms** | **0.0657 ms** | **0.0615 ms** |  **187.5000** |  **187.5000** |  **187.5000** |   **937.84 KB** |
            GeneratePixelDataFromBytes |  120000 |   7.380 ms | 0.0452 ms | 0.0401 ms |  218.7500 |  218.7500 |  179.6875 |   937.83 KB |
 GeneratePixelDataFromColorsParallelly |  120000 |  17.420 ms | 0.2754 ms | 0.2441 ms |  968.7500 |  906.2500 |  687.5000 |  3128.77 KB |
  GeneratePixelDataFromBytesParallelly |  120000 |  13.113 ms | 0.1876 ms | 0.1663 ms | 1062.5000 |  984.3750 |  843.7500 |  3518.18 KB |
           **GeneratePixelDataFromColors** |  **786432** |  **73.931 ms** | **0.7055 ms** | **0.6599 ms** |  **428.5714** |  **428.5714** |         **-** |   **6144.5 KB** |
            GeneratePixelDataFromBytes |  786432 |  45.961 ms | 0.4499 ms | 0.3988 ms |  583.3333 |  583.3333 |  166.6667 |  6144.33 KB |
 GeneratePixelDataFromColorsParallelly |  786432 | 114.051 ms | 2.2321 ms | 2.6572 ms | 3000.0000 | 3000.0000 | 1000.0000 | 22535.55 KB |
  GeneratePixelDataFromBytesParallelly |  786432 |  87.087 ms | 1.4655 ms | 1.4393 ms | 2571.4286 | 2571.4286 | 1142.8571 | 22535.54 KB |
           **GeneratePixelDataFromColors** | **2073600** | **195.927 ms** | **3.6429 ms** | **3.5778 ms** |         **-** |         **-** |         **-** | **16200.76 KB** |
            GeneratePixelDataFromBytes | 2073600 | 126.255 ms | 1.2582 ms | 1.1769 ms |         - |         - |         - | 16200.65 KB |
 GeneratePixelDataFromColorsParallelly | 2073600 | 296.356 ms | 5.8063 ms | 8.5108 ms | 3000.0000 | 3000.0000 |  500.0000 | 53071.65 KB |
  GeneratePixelDataFromBytesParallelly | 2073600 | 229.170 ms | 4.4925 ms | 6.4431 ms | 3000.0000 | 3000.0000 | 1333.3333 | 62629.02 KB |
