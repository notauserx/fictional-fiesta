# Pixel Sort
![Line coverage](badge_linecoverage.png)

A Proof of concept WPF application that generates random pixels and then sorts them based on the hue value.

This project was given to me as a take home coding test from a software company.

![](demo.gif)

# Building from source

#### With visual studio

1. Open the solution in Visual studio.
2. Restore nuget packges
3. Build and run

#### With dotnet CLI

Verify you have dotnet cli by typing 

```powershell
dotnet
```

From the solution's root directory

1. Resotre the packages
```powershell
dotnet restore
```

2. Build the source

```powershell
dotnet build
```

3. Optionally, you can run the tests 

```powershell
dotnet test
```

4. Run the app with

```powershell
dotnet run --project .\PixelSort.App\
```

# Overview

The solution is divided into three projects

- PixelSort.App
	- contains the wpf application
- PixelSort.Domain
	- houses the application logic
- PixelSort.Doamin.Tests
	- xUnit project that contains the test for PixelSort.Domain
	- View the code coverage report [here](code-coverage-summary.md)


# Design decisions

- I have used WritableBitmapSource to generate the image source. This class has a back buffer where the pixel informations are populated

- I have uses a TashFactory class to run the methods that generate random pixels and sorts them.

- I have used Microsoft.Extensions.DependencyInjection nuget package to set up DI containers.

- xUnit was used as a the unit testing framework

- The PixelSortviewModel acts as a controller for the view.

- The Pixel class is the model. 

- The randomly generated pixels are created from first creating random RGBA values that is used to create a Color class. Then Pixels are created from a Color class instance. This has the benefit of using GetHue() method on the Color class to calculate the Hue value.

- The GetHue() methods returns Hue as Float type. Hue is a value from 0 degree to 360 degree. Knowing this information, I have used bucket sort to implement the sorting. On the benchmarks it clearly outsmarts the comparison based sorting algorithms. 
	
- I have run some benchmarks, look [here](BucketSortVsOthers-report-github.md)

- The Color Sorting button click handler first checks if the pixels are populated before attempting to sort them.

- If the pixels are already sorted, clicking the Color Sorting button will not sort the pixels again.

- I have noticed the sorted image to be flipped 90 degrees to the left. To render this transformation, the PixelConverter class arranges the bytes from the sorted pixels in column first manner. The GetTransposedPixelsFromArgbColors method does this job.

- Pixelconfiguration class has two static methods DefaultConfiguration and HighPixelConfiguration that sets up the resolution for the image.

# Running the benchmarks

- Set PixelSort.Benchmarks as startup project in Visual studio.
- Use Release configuration.
- Start the project with ctrl + F5
- The results will be in the following directory

```
PixelSort.Benchmarks\bin\Release\netcoreapp3.1\BenchmarkDotNet.Artifacts\results
```

# Generating coverage report

- Make sure dotnet-reportgenerator-globaltool is installed details [here](https://www.nuget.org/packages/dotnet-reportgenerator-globaltool)
- Genrate coverate report using coverlet
```powershell
dotnet test --collect:"XPlat Code Coverage"
```
- run the following, specify the path to the .xml 
```powershell
 reportgenerator "-reports:*.xml" "-targetdir:C:\report" -reporttypes:MarkdownSummary
```

# TODO 

- Add logging

- Currently when generating random pixels, the Hue values are also getting generated. Find an implementation where this calculation can be deferred or offloaded to a seperate thread.

- Use a priority queue to store items inside a bucket. Will require the projects to be updated to .NET 6.0 since it has introduced the Priority Queue class.

# License

MIT license 

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

