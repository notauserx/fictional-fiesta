using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
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


        public PixelSortViewModel(
            IPixelConfiguraiton config,
            TaskScheduler taskScheduler,
            RandomPixelDataGenerator randomPixelDataGenerator,
            PixelConverter pixelConverter)
        {
            dpi = config.Dpi;
            width = config.Width;
            height = config.Height;

            wb = new WriteableBitmap(width, height, dpi, dpi, PixelFormats.Bgr32, null);

            uiTaskFactory = new TaskFactory(taskScheduler);
            this.randomPixelDataGenerator = randomPixelDataGenerator;
            this.pixelConverter = pixelConverter;
        }



        public WriteableBitmap WriteableBitmap { get => wb; }

        public bool ArePixelsEmpty() => pixels is null;

        public bool ArePixelsSorted() => isSorted;


        public void UpdateBackBufferWithRandomPixelData()
        {
            uiTaskFactory.StartNew(() =>            
                CopyBytesToBackBuffer(GetRandomPixelBytes())
            );
        }

        public void UpdateBackBufferWithSortedPixelData()
        {
            if (pixels is null)
            {
                throw new Exception("Can not sort pixels before pixels are generated.");
            }
            
            uiTaskFactory.StartNew(() =>
                CopyBytesToBackBuffer(GetSortedPixelBytes())
            );
        }


        private void CopyBytesToBackBuffer(byte[] buffer)
        {
            // lock on UI thread
            wb.Lock();

            // we have to copy this because it must only be accessed from the UI thread
            IntPtr backBuffer = wb.BackBuffer;

            System.Diagnostics.Debug.Assert(backBuffer != IntPtr.Zero);

            // update on this thread
            Marshal.Copy(buffer, 0, backBuffer, buffer.Length);

            wb.AddDirtyRect(
                new Int32Rect(0, 0,
                wb.PixelWidth,
                wb.PixelHeight));

            wb.Unlock();
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
            pixels = pixels.OrderBy(x => x.Hue).ToArray();
        }
    }
}
