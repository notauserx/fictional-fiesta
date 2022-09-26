using Microsoft.Extensions.DependencyInjection;
using PixelSort.Domain;
using System.Threading.Tasks;

namespace PixelSort.App
{
    internal static class DefaultConfiguration
    {
        internal static ServiceProvider InitializeAndGet()
        {
            var serviceCollection = new ServiceCollection();

            InitializePixelConfiguration(serviceCollection);
            InitializeHelpers(serviceCollection);
            InitializeServices(serviceCollection);
            InitializeTaskScheduler(serviceCollection);
            InitializeViewModels(serviceCollection);

            InitializeMainWindow(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        private static void InitializePixelConfiguration(ServiceCollection services)
        {
            services.AddSingleton<IPixelConfiguraiton>
                (s => PixelConfiguration.HighPixelConfiguration());
        }
        private static void InitializeHelpers(ServiceCollection services)
        {
            services.AddScoped<IPixelSorter, BucketSortPixelSorter>();
            services.AddSingleton<RandomPixelDataGenerator>();
            services.AddSingleton<PixelConverter>();
        }

        private static void InitializeServices(ServiceCollection services)
        {
            services.AddScoped<IPixelService, PixelService>();
            services.AddSingleton<WriteableBitmapService>();

        }

        private static void InitializeTaskScheduler(ServiceCollection services)
        {

            services.AddSingleton(s => TaskScheduler.FromCurrentSynchronizationContext());
        }

        private static void InitializeViewModels(ServiceCollection services)
        {
            services.AddSingleton<PixelSortViewModel>();

        }

        private static void InitializeMainWindow(ServiceCollection services) =>
            services.AddSingleton<MainWindow>();
    }
}
