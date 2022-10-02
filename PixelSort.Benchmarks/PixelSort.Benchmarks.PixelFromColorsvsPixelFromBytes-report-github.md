``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2006/21H2/November2021Update)
Intel Core i5-4590 CPU 3.30GHz (Haswell), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.302
  [Host]     : .NET Core 3.1.26 (CoreCLR 4.700.22.26002, CoreFX 4.700.22.26801), X64 RyuJIT AVX2
  DefaultJob : .NET Core 3.1.26 (CoreCLR 4.700.22.26002, CoreFX 4.700.22.26801), X64 RyuJIT AVX2


```
|                                Method |   count |       Mean |     Error |    StdDev |      Gen0 |      Gen1 |      Gen2 |   Allocated |
|-------------------------------------- |-------- |-----------:|----------:|----------:|----------:|----------:|----------:|------------:|
|           **GeneratePixelDataFromColors** |  **120000** |  **11.028 ms** | **0.0713 ms** | **0.0667 ms** |  **265.6250** |  **265.6250** |  **187.5000** |   **937.83 KB** |
|            GeneratePixelDataFromBytes |  120000 |   7.369 ms | 0.0255 ms | 0.0239 ms |  226.5625 |  226.5625 |  187.5000 |   937.88 KB |
| GeneratePixelDataFromColorsParallelly |  120000 |  17.469 ms | 0.2725 ms | 0.2549 ms | 1687.5000 | 1093.7500 |  750.0000 |  3304.78 KB |
|  GeneratePixelDataFromBytesParallelly |  120000 |  13.406 ms | 0.2524 ms | 0.2361 ms | 1578.1250 | 1187.5000 |  828.1250 |  3573.44 KB |
|           **GeneratePixelDataFromColors** |  **786432** |  **72.677 ms** | **0.5211 ms** | **0.4874 ms** |  **428.5714** |  **428.5714** |         **-** |  **6144.51 KB** |
|            GeneratePixelDataFromBytes |  786432 |  45.511 ms | 0.4931 ms | 0.4612 ms |  583.3333 |  583.3333 |  166.6667 |  6144.33 KB |
| GeneratePixelDataFromColorsParallelly |  786432 | 114.703 ms | 2.2142 ms | 2.0712 ms | 2800.0000 | 2600.0000 | 1400.0000 | 22535.79 KB |
|  GeneratePixelDataFromBytesParallelly |  786432 |  87.289 ms | 1.7177 ms | 1.9092 ms | 2666.6667 | 2666.6667 | 1000.0000 | 22535.55 KB |
|           **GeneratePixelDataFromColors** | **2073600** | **186.908 ms** | **1.1563 ms** | **1.0251 ms** |         **-** |         **-** |         **-** | **16200.76 KB** |
|            GeneratePixelDataFromBytes | 2073600 | 118.198 ms | 0.8683 ms | 0.8122 ms |  200.0000 |  200.0000 |  200.0000 | 16200.68 KB |
| GeneratePixelDataFromColorsParallelly | 2073600 | 299.488 ms | 5.8503 ms | 6.5026 ms | 1000.0000 | 1000.0000 | 1000.0000 | 61263.71 KB |
|  GeneratePixelDataFromBytesParallelly | 2073600 | 229.646 ms | 3.9176 ms | 3.6646 ms | 1333.3333 | 1333.3333 | 1333.3333 | 62631.43 KB |
