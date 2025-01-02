using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

using HotelBookingApp.Core;
using HotelBookingApp.Services;
using HotelBookingApp.ViewModels;
using HotelBookingApp.ViewModels.BookingViewModels;
using HotelBookingApp.ViewModels.GuestViewModels;
using HotelBookingApp.ViewModels.PaymentViewModels;
using HotelBookingApp.ViewModels.RoomViewModels;
using HotelBookingApp.Views;
using HotelBookingBL;

namespace HotelBookingApp
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
            ServicesConfigurations();
            _serviceProvider = _services.BuildServiceProvider();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // load the data
            var databaseData = _serviceProvider.GetRequiredService<IDataInitializationService>();
            await databaseData.InitializeAsync();

            // open the main window
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }


        /// <summary>
        ///  Register the services, (Viewmodels and navigation etc.)
        /// </summary>
        private void ServicesConfigurations()
        {
            _services.AddSingleton<INavigationService, NavigationService>();
            _services.AddSingleton<IDataInitializationService, DataInitializationService>();

            _services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            // register BL
            _services.AddSingleton<AppManagerBL>();

            // Register Viewmodels
            _services.AddSingleton<MainViewModel>();
            _services.AddSingleton<NavigationBarViewModel>();

            _services.AddSingleton<BookingsListViewModel>();
            _services.AddSingleton<AddBookingViewModel>();
            _services.AddSingleton<UpdateBookingViewModel>();
            _services.AddSingleton<BookingDetailsViewModel>();

            _services.AddSingleton<RoomsListViewModel>();
            _services.AddSingleton<AddRoomViewModel>();
            _services.AddSingleton<UpdateRoomViewModel>();
            _services.AddSingleton<RoomDetailsViewModel>();

            _services.AddSingleton<PaymentsListViewModel>();

            _services.AddSingleton<GuestsListViewModel>();
            _services.AddSingleton<AddGuestViewModel>();
            _services.AddSingleton<UpdateGuestViewModel>();
            _services.AddSingleton<GuestDetailsViewModel>();

            // Register ViewModels with NavigationService
            _services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType =>
            {
                return (ViewModelBase)serviceProvider.GetRequiredService(viewModelType);
            });
        }
    }
}
