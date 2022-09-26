using Microsoft.Extensions.DependencyInjection;
using System.Windows;

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
                mainWindow.Show();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
