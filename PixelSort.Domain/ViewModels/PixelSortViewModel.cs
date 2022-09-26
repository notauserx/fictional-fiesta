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


        public void UpdateBackBufferWithRandomPixelData()
        {
            uiTaskFactory.StartNew(() =>
            {
                var count = bitmapService.GetPixelCount();
                bitmapService.UpdateBackBuffer(pixelService.GenerateRandomPixelData(count));
            });
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
