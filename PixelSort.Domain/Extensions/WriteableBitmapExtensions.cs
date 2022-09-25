using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public static class WriteableBitmapExtensions
    {
        public static void CopyBytesToBackBuffer(this WriteableBitmap wb, byte[] buffer)
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
    }
}
