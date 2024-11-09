using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
//
using MediaPlaylist.Core;
using MediaPlaylist.ViewModels;
using MediaPlaylist.Services;
using MediaPlaylist.ViewModels.PlaylistViewModels;
using MediaPlaylist.ViewModels.MediaViewModels;
using MediaPlaylistBL;

namespace MediaPlaylist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceCollection _services;


        public App() : base()
        {
            _services = new ServiceCollection();
            ConfigureServices();
            _serviceProvider = _services.BuildServiceProvider();
        }

        /// <summary>
        ///  Register the services, (Viewmodels and navigation etc.)
        /// </summary>
        private void ConfigureServices()
        {
            _services.AddSingleton<INavigationService, NavigationService>();
            _services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            // register the bucniss logic layer libraray (BLL)
            _services.AddSingleton<ApplicationManager>();

            // Register ViewModels
            _services.AddSingleton<MainViewModel>();
            _services.AddSingleton<NavigationBarViewModel>();
            _services.AddSingleton<StartPageViewModel>();

            _services.AddSingleton<AddPlaylistViewModel>();
            _services.AddSingleton<PlaylistDetailsViewModel>();
            _services.AddSingleton<UpdatePlaylistViewModel>();
            //_services.AddTransient<PlaylistDetailsViewModel>();
            _services.AddSingleton<AddMediaViewModel>();
            _services.AddSingleton<UpdateMediaViewModel>();

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
