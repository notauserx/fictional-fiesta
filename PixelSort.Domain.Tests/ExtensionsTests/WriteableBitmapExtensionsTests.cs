using System.Windows.Media.Imaging;
using Xunit;
namespace PixelSort.Domain.Tests.ExtensionsTests
{
    public class WriteableBitmapExtensionsTests
    {

        [Fact]
        public void Test_CopyBytesToBackBuffer_Copies_Bytes()
        {
            var wb = new WriteableBitmap(4, 4, 4, 4, System.Windows.Media.PixelFormats.Bgr32, null);


            var bytes = new byte[64];
            

            for(byte i = 0; i < bytes.Length; i++)
            {
                bytes[i] = i;
            }


            wb.CopyBytesToBackBuffer(bytes);
            wb.Lock();
            var backbuffer = wb.BackBuffer;

            unsafe
            {
                byte* backbufferPointer = (byte*)backbuffer.ToPointer();

                for(int i = 0; i < bytes.Length; i++)
                {
                    var item = backbufferPointer[i];
                    Assert.Equal(i, item);
                }

            }

            wb.Unlock();

        }
    }
}
