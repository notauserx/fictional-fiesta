using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;
using PixelSort.Domain;

namespace PixelSort.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Color[] colors;
        int width = 400;
        int height = 300;
        int stride;
        double dpi = 96;

        BitmapSource bmpSource;
        public MainWindow()
        {
            stride = width * 4;
            InitializeComponent();
        }

        private void RandomColorGeneratorButton_Click(object sender, RoutedEventArgs e)
        {
            colors = RandomColorDataGenerator.GenerateRandomColorData(width, height);
            bmpSource = generateBitmapSourceFromColors(colors);
            PixelImage.Source = bmpSource;
        }

        private BitmapSource generateBitmapSourceFromColors(Color[] colors)
        {
            byte[] pixelData = new PixelConverter(height, width)
                .GetTransposedPixelsFromArgbColors(colors);

            

            return BitmapSource.Create(width, height, dpi, dpi,
                System.Windows.Media.PixelFormats.Bgr32, null, pixelData, width* 4);
        }

        private void SortColorsButton_Click(object sender, RoutedEventArgs e)
        {
            var sortedColors = ColorSorter.GetSortedColors(colors);
            var sortedBmpSource = generateBitmapSourceFromColors(sortedColors);

            PixelImage.Source = sortedBmpSource;
        }

    }
}
