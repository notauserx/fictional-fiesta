using System;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public class PixelSortViewModel
    {
        readonly WriteableBitmap wb;

        private int width;
        private int height;
        private int dpi;

        private readonly TaskFactory uiTaskFactory;
        private readonly PixelConverter pixelConverter;
        
        private readonly IPixelService pixelService;


        public PixelSortViewModel(
            IPixelConfiguraiton config,
            TaskScheduler taskScheduler,
            PixelConverter pixelConverter,
            IPixelService pixelService)
        {
            dpi = config.Dpi;
            width = config.Width;
            height = config.Height;

            wb = new WriteableBitmap(width, height, dpi, dpi, PixelFormats.Bgr32, null);

            uiTaskFactory = new TaskFactory(taskScheduler);
            this.pixelService = pixelService;
            this.pixelConverter = pixelConverter;

        }



        public WriteableBitmap WriteableBitmap { get => wb; }

        public bool ArePixelsSorted() => pixelService.ArePixelsSorted();
        public bool ArePixelsEmpty() => pixelService.ArePixelsEmpty();


        public void UpdateBackBufferWithRandomPixelData()
        {
            uiTaskFactory.StartNew(() =>            
                wb.CopyBytesToBackBuffer(GetRandomPixelBytes())
            );
        }

        public void UpdateBackBufferWithSortedPixelData()
        {
            if (pixelService.ArePixelsEmpty())
            {
                throw new Exception("Can not sort pixels before pixels are generated.");
            }
            
            uiTaskFactory.StartNew(() =>
                wb.CopyBytesToBackBuffer(GetSortedPixelBytes())
            );
        }
        

        private byte[] GetRandomPixelBytes()
        {
            return pixelConverter
                .GetTransposedPixelsFromArgbColors(
                    pixelService.GenerateRandomPixelData(width * height));
        }

        private byte[] GetSortedPixelBytes()
        {
            return pixelConverter
                .GetTransposedPixelsFromArgbColors(pixelService.GetSortedPixels());
        }

       
    }
}
