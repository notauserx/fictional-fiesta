using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public class PixelSortViewModel
    {
        private readonly WriteableBitmapService bitmapService;
        private readonly IPixelService pixelService;

        private readonly TaskFactory uiTaskFactory;

        public PixelSortViewModel(
            TaskScheduler taskScheduler,
            WriteableBitmapService writeableBitmapService,
            IPixelService pixelService)
        {
            uiTaskFactory = new TaskFactory(taskScheduler);
            this.pixelService = pixelService;
            this.bitmapService = writeableBitmapService;

        }

        public BitmapSource WriteableBitmap { get => bitmapService.WriteableBitmap; }

        public bool ArePixelsSorted() => pixelService.ArePixelsSorted();
        public bool ArePixelsEmpty() => pixelService.ArePixelsEmpty();


        public async Task<bool> UpdateBackBufferWithRandomPixelData()
        {
            var count = bitmapService.GetPixelCount();

            await Task.Factory.StartNew(() => pixelService.GenerateRandomPixelData(count))
                .ContinueWith(t =>
                {
                    uiTaskFactory.StartNew(() =>
                            bitmapService.UpdateBackBuffer(t.Result));
                });
            return await Task.FromResult(true);
        }

        public void UpdateBackBufferWithSortedPixelData()
        {
            if (pixelService.ArePixelsEmpty())
            {
                throw new Exception("Can not sort pixels before pixels are generated.");
            }

            uiTaskFactory.StartNew(() =>
                bitmapService.UpdateBackBuffer(pixelService.GetSortedPixels()));
        }
    }
}
