using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;

using PixelSort.Domain;

namespace PixelSort.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;

        public App()
        {
            serviceProvider = DefaultConfiguration.InitializeAndGet();

        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                var mainWindow = serviceProvider.GetService<MainWindow>();
                mainWindow.DataContext = serviceProvider.GetService<PixelSortViewModel>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Debug.WriteLine($"An unhandled exception just occurred: {e.Exception}");
            e.Handled = true;
        }
    }
}
