using AutomobileLibrary.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AutomobileWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;
        public App()
        {
            // Config for DependencyInjection (DI)
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(typeof(ICarRepository), typeof(CarRepository));
            services.AddSingleton<MainWindow>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var windownCarManagement = serviceProvider.GetService<MainWindow>();
            windownCarManagement.Show();
        }
    }

}
