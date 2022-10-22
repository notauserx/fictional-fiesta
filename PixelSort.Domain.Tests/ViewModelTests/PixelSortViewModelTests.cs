using System;
using System.Threading.Tasks;
using Xunit;

namespace PixelSort.Domain.Tests
{
    public class PixelSortViewModelTests
    {
        private PixelSortViewModel getViewModel(int width, int height, TaskScheduler taskScheduler)
        {
            var pixelConfiuration = new PixelConfiguration(width, height, 4, 96);
            var pixelService = new PixelService(
                new RandomPixelGenerator(),
                 new BucketSortPixelSorter());

            var bitmapService = new WriteableBitmapService(pixelConfiuration,
                new PixelConverter());

            return new PixelSortViewModel(
                 taskScheduler,
                 bitmapService,
                 pixelService);
        }
        [Fact]
        public void Test_IsSorted_Is_False_Initially()
        {
            var taskScheduler = new DeterministicTaskScheduler();

            var vm = getViewModel(4, 4, taskScheduler);

            Assert.False(vm.ArePixelsSorted());
        }

        [Fact]
        public void Test_Constructor_creates_WritableBitMap()
        {
            var taskScheduler = new DeterministicTaskScheduler();

            var vm = getViewModel(4, 4, taskScheduler);

            Assert.NotNull(vm.WriteableBitmap);
        }

        [Fact]
        public void Test_IsSorted_Is_False_After_UpdateBackBufferWithRandomPixelData()
        {
            var taskScheduler = new DeterministicTaskScheduler();

            var vm = getViewModel(4, 4, taskScheduler);

            vm.UpdateBackBufferWithRandomPixelData();
            taskScheduler.RunTasksUntilIdle();

            Assert.False(vm.ArePixelsSorted());
        }

    }
}
