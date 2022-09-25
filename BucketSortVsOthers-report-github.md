``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2006/21H2/November2021Update)
Intel Core i5-4590 CPU 3.30GHz (Haswell), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.302
  [Host]     : .NET Core 3.1.26 (CoreCLR 4.700.22.26002, CoreFX 4.700.22.26801), X64 RyuJIT AVX2
  DefaultJob : .NET Core 3.1.26 (CoreCLR 4.700.22.26002, CoreFX 4.700.22.26801), X64 RyuJIT AVX2


```
|              Method |         pixels |        Mean |     Error |    StdDev |
|-------------------- |--------------- |------------:|----------:|----------:|
| **BucketSortScaled100** |  **Pixel[120000]** |    **28.37 ms** |  **0.562 ms** |  **0.770 ms** |
|  BucketSortScaled10 |  Pixel[120000] |    15.25 ms |  0.193 ms |  0.180 ms |
|   BucketSortScaled1 |  Pixel[120000] |    16.43 ms |  0.322 ms |  0.407 ms |
|           ArraySort |  Pixel[120000] |    12.47 ms |  0.243 ms |  0.260 ms |
|            LinqSort |  Pixel[120000] |    37.91 ms |  0.399 ms |  0.374 ms |
| **BucketSortScaled100** | **Pixel[2073600]** |   **379.82 ms** |  **7.524 ms** | **11.934 ms** |
|  BucketSortScaled10 | Pixel[2073600] |   350.90 ms |  6.770 ms |  7.244 ms |
|   BucketSortScaled1 | Pixel[2073600] |   437.53 ms |  8.165 ms |  7.638 ms |
|           ArraySort | Pixel[2073600] |   605.05 ms | 10.957 ms |  9.713 ms |
|            LinqSort | Pixel[2073600] | 1,003.80 ms | 19.552 ms | 18.289 ms |
| **BucketSortScaled100** |  **Pixel[786432]** |   **162.10 ms** |  **3.142 ms** |  **4.085 ms** |
|  BucketSortScaled10 |  Pixel[786432] |   116.69 ms |  1.571 ms |  1.393 ms |
|   BucketSortScaled1 |  Pixel[786432] |   159.20 ms |  1.787 ms |  1.395 ms |
|           ArraySort |  Pixel[786432] |   214.44 ms |  4.590 ms | 13.532 ms |
|            LinqSort |  Pixel[786432] |   320.78 ms |  6.373 ms | 11.492 ms |
