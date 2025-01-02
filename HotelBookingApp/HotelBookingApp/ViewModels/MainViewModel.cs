using HotelBookingApp.Core;
using HotelBookingApp.Services;
using HotelBookingApp.ViewModels.BookingViewModels;
using HotelBookingBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBookingApp.ViewModels
{
    public class MainViewModel : ViewModelBase, IDisposable
    {
        private readonly INavigationService _navigationService;

        public NavigationBarViewModel NavigationBarViewModel { get; }
        public ViewModelBase CurrentViewModel => _navigationService.CurrentViewModel;

        public MainViewModel(INavigationService navigationService, NavigationBarViewModel navViewModel)
        {
            _navigationService = navigationService;
            _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigationBarViewModel = navViewModel;

            // Set the current viewModel to bookings list home Page
            _navigationService.NavigateTo<BookingsListViewModel>();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void Dispose()
        {
            _navigationService.CurrentViewModelChanged -= OnCurrentViewModelChanged;
            GC.SuppressFinalize(this);  // Violates rule
        }
    }
}
