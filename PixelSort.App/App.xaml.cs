using Microsoft.Extensions.DependencyInjection;
using PixelSort.Domain;
using System.Threading.Tasks;
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
            var services = new ServiceCollection();

            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IPixelConfiguraiton>
                (s => PixelConfiguration.DefaultConfiguration());


            services.AddSingleton<MainWindow>();
            services.AddSingleton<PixelSortViewModel>();
            services.AddSingleton<RandomPixelDataGenerator>();
            services.AddSingleton<PixelConverter>();
            services.AddSingleton(s => TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
