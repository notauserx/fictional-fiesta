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
        PixelSortViewModel pixelSortViewModel;

        public MainWindow()
        {
            pixelSortViewModel = new PixelSortViewModel(450, 300);
            InitializeComponent();
        }

        private void RandomColorGeneratorButton_Click(object sender, RoutedEventArgs e)
        {
            
            PixelImage.Source = pixelSortViewModel.GetBitmapSourceFromRandomData();
        }


        private void SortColorsButton_Click(object sender, RoutedEventArgs e)
        {
            if(pixelSortViewModel.ArePixelsEmpty())
            {
                // TODO :: display error message to the user.
            } 
            else
            {
                PixelImage.Source = pixelSortViewModel.GetBitmapSourceFromSortedData();
            }

        }

    }
}
