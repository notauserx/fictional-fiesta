using System;
using Xunit;

namespace PixelSort.Domain.Tests
{
    public class PixelSortViewModelTests
    {
        [Fact]
        public void Test_IsSorted_Is_False_Initially()
        {
            var vm = new PixelSortViewModel(4, 4);
            Assert.False(vm.ArePixelsSorted());
        }

        [Fact]
        public void Test_IsSorted_Is_False_After_GetBitmapSourceFromRandomData()
        {
            var vm = new PixelSortViewModel(4, 4);
            var _ = vm.GetBitmapSourceFromRandomData();
            Assert.False(vm.ArePixelsSorted());
        }

        [Fact]
        public void Test_IsSorted_Is_True_After_GetBitmapSourceFromSortedData()
        {
            var vm = new PixelSortViewModel(4, 4);
            var data = vm.GetBitmapSourceFromRandomData();
            var _ = vm.GetBitmapSourceFromSortedData();
            Assert.True(vm.ArePixelsSorted());
        }

        [Fact]
        public void Test_GetBitmapSourceFromSortedData_Throws_Exception_When_Colors_Is_Null()
        {
            var vm = new PixelSortViewModel(4,4);

            Assert.Throws<Exception>(() => vm.GetBitmapSourceFromSortedData());
        }

        
    }
}
