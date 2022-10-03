using System.Windows;
using PixelSort.Domain;

namespace PixelSort.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PixelSortViewModel pixelSortViewModel;

        public MainWindow(PixelSortViewModel pixelSortViewModel)
        {
            this.pixelSortViewModel = pixelSortViewModel;
            InitializeComponent();
        }

        private async void RandomPixelGeneratorButton_Click(object sender, RoutedEventArgs e)
        {
            RandomPixelGeneratorButton.IsEnabled = false;
            var result = await pixelSortViewModel.UpdateBackBufferWithRandomPixelData();
            if (result)
            {
                PixelImage.Source = pixelSortViewModel.WriteableBitmap;
                RandomPixelGeneratorButton.IsEnabled = true;

            }
        }

        private async void SortPixelsButton_Click(object sender, RoutedEventArgs e)
        {
            if (pixelSortViewModel.ArePixelsEmpty())
            {
                // TODO :: display error message to the user.
            }
            else if (pixelSortViewModel.ArePixelsSorted())
            {
                // TODO :: display message to the user.
            }
            else
            {
                SortPixelsButton.IsEnabled = false;
                var result = await pixelSortViewModel.UpdateBackBufferWithSortedPixelData();
                if(result)
                {
                    SortPixelsButton.IsEnabled = true;
                }
            }

        }
    }
}
