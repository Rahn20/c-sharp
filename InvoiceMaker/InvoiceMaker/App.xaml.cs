using System;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

using InvoiceMaker.ViewModels;
using InvoiceMaker.Services;
using InvoiceMaker.Core;

namespace InvoiceMaker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceCollection _services;


        public App()
        {
            _services = new ServiceCollection();
            ConfigureServices();
            _serviceProvider = _services.BuildServiceProvider();
        }

        /// <summary>
        ///   Register the services.
        ///   AddTransient: A new instance of the service is created every time it's requested.
        ///   AddScoped: A new instance of the service is created once per scope (per request or operation).
        ///   AddSingleton: Ensures only one instance is created and reused across the entire application.
        /// </summary>
        private void ConfigureServices()
        {
            _services.AddSingleton<INavigationService, NavigationService>();
            _services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            // Register the Manager, it needs to be shared.
            _services.AddSingleton<InvoiceManager>();

            // Register ViewModels as Singletons.
            _services.AddSingleton<MainViewModel>();
            _services.AddSingleton<HeaderViewModel>();
            _services.AddSingleton<InvoiceContentViewModel>();
            //_services.AddScoped<HeaderViewModel>();

            // Register ViewModels with NavigationService
            _services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType =>
            {
                return (ViewModelBase)serviceProvider.GetRequiredService(viewModelType);
            });
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }

}
