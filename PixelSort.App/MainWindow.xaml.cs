﻿using System.Windows;
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

        private void RandomPixelGeneratorButton_Click(object sender, RoutedEventArgs e)
        {
            pixelSortViewModel.UpdateBackBufferWithRandomPixelData();
            PixelImage.Source = pixelSortViewModel.WriteableBitmap;
        }

        private void SortPixelsButton_Click(object sender, RoutedEventArgs e)
        {
            if (pixelSortViewModel.ArePixelsEmpty())
            {
                // TODO :: display error message to the user.
            }
            else
            {
                pixelSortViewModel.UpdateBackBufferWithSortedPixelData();
            }

        }
    }
}
