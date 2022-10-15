using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public class PixelSortViewModel
    {
        private readonly WriteableBitmapService bitmapService;
        private readonly IPixelService pixelService;

        private readonly TaskFactory uiTaskFactory;

        public ICommand GenerateRandomPixelsCommand { get; private set; }
        public ICommand SortPixelsCommand { get; private set; }

        public PixelSortViewModel(
            TaskScheduler taskScheduler,
            WriteableBitmapService writeableBitmapService,
            IPixelService pixelService)
        {
            uiTaskFactory = new TaskFactory(taskScheduler);
            this.pixelService = pixelService;
            this.bitmapService = writeableBitmapService;

            GenerateRandomPixelsCommand = new PixelsCommandHandler(UpdateBackBufferWithRandomPixelData);
            SortPixelsCommand = new PixelsCommandHandler(UpdateBackBufferWithSortedPixelData);
        }

        public BitmapSource WriteableBitmap 
        { 
            get => bitmapService.WriteableBitmap; 
        }

        public bool ArePixelsSorted() => pixelService.ArePixelsSorted();
        public bool ArePixelsEmpty() => pixelService.ArePixelsEmpty();


        public void UpdateBackBufferWithRandomPixelData()
        {
            var count = bitmapService.GetPixelCount();

            Task.Factory.StartNew(() =>
            {
                return pixelService.GenerateRandomPixelData(count);
            }).ContinueWith(t =>
            {
                uiTaskFactory.StartNew(() =>
                {
                    bitmapService.UpdateBackBuffer(t.Result);
                });

            });
        }

        public void UpdateBackBufferWithSortedPixelData()
        {
            if (pixelService.ArePixelsEmpty())
            {
                return;
            }

            Task.Factory.StartNew(() =>
            {
                return pixelService.GetSortedPixels();
            }).ContinueWith(t =>
            {
                uiTaskFactory.StartNew(() =>
                {
                    bitmapService.UpdateBackBuffer(t.Result);
                });

            });

        }
    }
}
