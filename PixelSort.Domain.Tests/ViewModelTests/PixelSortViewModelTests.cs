using System;
using Xunit;

namespace PixelSort.Domain.Tests
{
    public class PixelSortViewModelTests
    {
        [Fact]
        public void Test_GetBitmapSourceFromSortedData_Throws_Exception_When_Colors_Is_Null()
        {
            var vm = new PixelSortViewModel(4,4);

            Assert.Throws<Exception>(() => vm.GetBitmapSourceFromSortedData());
        }
    }
}
