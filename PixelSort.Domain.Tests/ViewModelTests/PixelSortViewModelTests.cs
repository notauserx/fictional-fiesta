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
        public async Task Test_IsSorted_Is_False_After_UpdateBackBufferWithRandomPixelData()
        {
            var taskScheduler = new DeterministicTaskScheduler();

            var vm = getViewModel(4, 4, taskScheduler);

            var result = await vm.UpdateBackBufferWithRandomPixelData();

            Assert.True(result);

            taskScheduler.RunTasksUntilIdle();

            Assert.False(vm.ArePixelsSorted());
        }

        [Fact]
        public async Task Test_IsSorted_Is_True_After_GetBitmapSourceFromSortedData()
        {
            var taskScheduler = new DeterministicTaskScheduler();

            var vm = getViewModel(4, 4, taskScheduler);

            var randomTask = await vm.UpdateBackBufferWithRandomPixelData();
            taskScheduler.RunTasksUntilIdle();

            var sortTask = await vm.UpdateBackBufferWithSortedPixelData();
            taskScheduler.RunTasksUntilIdle();

            Assert.True(vm.ArePixelsSorted());

        }

        [Fact]
        public async Task Test_GetBitmapSourceFromSortedData_Throws_Exception_When_Colors_Is_Null()
        {
            var taskScheduler = new DeterministicTaskScheduler();

            var vm = getViewModel(4, 4, taskScheduler);

            await Assert.ThrowsAsync<Exception>(async () =>  await vm.UpdateBackBufferWithSortedPixelData());
        }


    }
}
