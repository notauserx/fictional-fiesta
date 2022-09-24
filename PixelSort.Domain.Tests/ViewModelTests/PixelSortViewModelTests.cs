using System;
using System.Threading.Tasks;
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
        public void Test_IsSorted_Is_False_After_UpdateBackBufferWithRandomPixelData()
        {
            var taskScheduler = new DeterministicTaskScheduler();
            var vm = new PixelSortViewModel(4, 4, taskScheduler);
            vm.UpdateBackBufferWithRandomPixelData();

            taskScheduler.RunTasksUntilIdle();


            Assert.False(vm.ArePixelsSorted());
        }

        [Fact]
        public void Test_IsSorted_Is_True_After_GetBitmapSourceFromSortedData()
        {
            var taskScheduler = new DeterministicTaskScheduler();
            var vm = new PixelSortViewModel(4, 4, taskScheduler);
            
            vm.UpdateBackBufferWithRandomPixelData();
            taskScheduler.RunTasksUntilIdle();

            vm.UpdateBackBufferWithSortedPixelData();
            taskScheduler.RunTasksUntilIdle();

            Assert.True(vm.ArePixelsSorted());
        }

        [Fact]
        public void Test_GetBitmapSourceFromSortedData_Throws_Exception_When_Colors_Is_Null()
        {
            var taskScheduler = new DeterministicTaskScheduler();
            var vm = new PixelSortViewModel(4,4, taskScheduler);

            Assert.Throws<Exception>(() => vm.UpdateBackBufferWithSortedPixelData());
        }

        
    }
}
