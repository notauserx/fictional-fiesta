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
        int width = 450;
        int height = 300;
        int stride;
        double dpi = 96;

        BitmapSource bmpSource;
        public MainWindow()
        {
            stride = width * 4;
            InitializeComponent();
            colors = RandomColorDataGenerator.GenerateRandomColorData(height * width);
            bmpSource = generateBitmapSourceFromColors(colors);


        }

        private void RandomColorGeneratorButton_Click(object sender, RoutedEventArgs e)
        {
            PixelImage.Source = bmpSource;
        }

        private BitmapSource generateBitmapSourceFromColors(Color[] colors)
        {
            byte[] pixelData = new byte[height * stride];

            var index = 0;
            foreach (var color in colors)
            {
                pixelData[index++] = color.B; // B
                pixelData[index++] = color.G; // G
                pixelData[index++] = color.R; // R
                pixelData[index++] = color.A; // A

            }

            return BitmapSource.Create(width, height, dpi, dpi,
                System.Windows.Media.PixelFormats.Bgr32, null, pixelData, stride);
        }

        private void SortColorsButton_Click(object sender, RoutedEventArgs e)
        {
            var sortedColors = ColorSorter.GetSortedColors(colors);
            var sortedBmpSource = generateBitmapSourceFromColors(sortedColors);

            PixelImage.Source = sortedBmpSource;
        }
    }
}
