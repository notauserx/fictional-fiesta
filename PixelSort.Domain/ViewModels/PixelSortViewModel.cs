using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public class PixelSortViewModel
    {
        readonly WriteableBitmap wb;
        private Pixel[] pixels;

        private int width;
        private int height;
        private int dpi;
        private bool isSorted;

        private readonly TaskFactory uiTaskFactory;
        private readonly RandomPixelDataGenerator randomPixelDataGenerator;

        private readonly PixelConverter pixelConverter;
        private readonly IPixelSorter pixelSorter;


        public PixelSortViewModel(
            IPixelConfiguraiton config,
            TaskScheduler taskScheduler,
            RandomPixelDataGenerator randomPixelDataGenerator,
            IPixelSorter pixelSorter,
            PixelConverter pixelConverter)
        {
            dpi = config.Dpi;
            width = config.Width;
            height = config.Height;

            wb = new WriteableBitmap(width, height, dpi, dpi, PixelFormats.Bgr32, null);

            uiTaskFactory = new TaskFactory(taskScheduler);
            this.randomPixelDataGenerator = randomPixelDataGenerator;
            this.pixelSorter = pixelSorter;
            this.pixelConverter = pixelConverter;

        }



        public WriteableBitmap WriteableBitmap { get => wb; }

        public bool ArePixelsEmpty() => pixels is null;

        public bool ArePixelsSorted() => isSorted;


        public void UpdateBackBufferWithRandomPixelData()
        {
            uiTaskFactory.StartNew(() =>            
                wb.CopyBytesToBackBuffer(GetRandomPixelBytes())
            );
        }

        public void UpdateBackBufferWithSortedPixelData()
        {
            if (pixels is null)
            {
                throw new Exception("Can not sort pixels before pixels are generated.");
            }
            
            uiTaskFactory.StartNew(() =>
                wb.CopyBytesToBackBuffer(GetSortedPixelBytes())
            );
        }
        

        private byte[] GetRandomPixelBytes()
        {
            pixels = randomPixelDataGenerator.GenerateRandomPixelData(width * height);
            isSorted = false;

            return pixelConverter
                .GetTransposedPixelsFromArgbColors(pixels);
        }

        private byte[] GetSortedPixelBytes()
        {
            if (isSorted is false)
            {
                SortPixels();
                isSorted = true;
            }

            return pixelConverter
                .GetTransposedPixelsFromArgbColors(pixels);
        }

        private void SortPixels()
        {
            pixels = pixelSorter.GetSortedPixels(pixels);
        }
    }
}
