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

        public PixelSortViewModel(int width, int height, TaskScheduler taskScheduler = null)
        {
            dpi = 96;
            this.width = width;
            this.height = height;

            wb = new WriteableBitmap(width, height, dpi, dpi, PixelFormats.Bgr32, null);
            taskScheduler ??= TaskScheduler.FromCurrentSynchronizationContext();

            uiTaskFactory = new TaskFactory(taskScheduler);
        }

        

        public WriteableBitmap WriteableBitmap { get => wb; }
        public bool ArePixelsEmpty() => pixels is null;
        public bool ArePixelsSorted() => isSorted;

        public void UpdateBackBufferWithRandomPixelData()
        {
            uiTaskFactory.StartNew(() =>            
                copyBytesToBackBuffer(getRandomPixelBytes())
            );
        }

        public void UpdateBackBufferWithSortedPixelData()
        {
            if (pixels is null)
            {
                throw new Exception("Can not sort pixels before pixels are generated.");
            }
            
            uiTaskFactory.StartNew(() =>
                copyBytesToBackBuffer(getSortedPixelBytes())
            );
        }

        private void copyBytesToBackBuffer(byte[] buffer)
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

        private byte[] getRandomPixelBytes()
        {
            pixels = RandomColorDataGenerator.GenerateRandomPixelData(width * height);
            isSorted = false;

            return new PixelConverter(width, height)
                .GetTransposedPixelsFromArgbColors(pixels);
        }

        private byte[] getSortedPixelBytes()
        {
            

            if (isSorted is false)
            {
                SortPixels();
                isSorted = true;
            }

            return new PixelConverter(width, height)
                .GetTransposedPixelsFromArgbColors(pixels);
        }

        private void SortPixels()
        {
            pixels = pixels.OrderBy(x => x.Hue).ToArray();
        }
    }
}
