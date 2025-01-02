using System;
using System.Windows.Input;

using HotelBookingApp.Core;
using HotelBookingApp.Services;
using HotelBookingApp.ViewModels.BookingViewModels;
using HotelBookingApp.ViewModels.GuestViewModels;
using HotelBookingApp.ViewModels.PaymentViewModels;
using HotelBookingApp.ViewModels.RoomViewModels;

namespace HotelBookingApp.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand BookingNavigateCommand { get; }
        public ICommand RoomNavigateCommand { get; }
        public ICommand PaymentNavigateCommand { get; }
        public ICommand GuestNavigateCommand { get; }

        public NavigationBarViewModel(INavigationService navigationService)
        {
            BookingNavigateCommand = new CommandBase(_ => navigationService.NavigateTo<BookingsListViewModel>());
            RoomNavigateCommand = new CommandBase(_ => navigationService.NavigateTo<RoomsListViewModel>());
            PaymentNavigateCommand = new CommandBase(_ => navigationService.NavigateTo<PaymentsListViewModel>());
            GuestNavigateCommand = new CommandBase(_ => navigationService.NavigateTo<GuestsListViewModel>());
        }
    }
}
