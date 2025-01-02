using HotelBookingApp.Core;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
using HotelBookingBL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelBookingApp.ViewModels.BookingViewModels
{
    public class BookingsListViewModel : ViewModelBase
    {
        private readonly AppManagerBL _appManagerBL;
        private readonly IDataInitializationService _dataService;

        public ObservableCollection<BookingDTOConverter> BookingsList => _dataService.Bookings;
        public int RegisteredBookings => _dataService.Bookings.Count;


        public string _searchBox = string.Empty;
        public string SearchBox
        {
            get => _searchBox;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _ = _dataService.GetAllBookingsAsync();
                }

                _searchBox = value;
                OnPropertyChanged(nameof(SearchBox));
            }
        }

        #region Commands
        public ICommand UpdateBookingCommand { get; }
        public ICommand ViewBookingCommand { get; }
        public ICommand RemoveBookingCommand { get; }
        public ICommand SearchByIdOrGuestOrRoomCommand { get; }
        #endregion

        public BookingsListViewModel(INavigationService navigationService, IDataInitializationService dataService,
            AppManagerBL appManagerBL)
        {
            _appManagerBL = appManagerBL;
            _dataService = dataService;

            UpdateBookingCommand = new CommandBase((object parameter) =>
            {
                navigationService.NavigateTo<UpdateBookingViewModel>();
                BookingUpdate?.Invoke(parameter as Booking);
            });
            ViewBookingCommand = new CommandBase((object paramter) =>
            {
                navigationService.NavigateTo<BookingDetailsViewModel>();
                BookingView?.Invoke(paramter as BookingDTOConverter);
            });

            RemoveBookingCommand = new CommandBaseAsync(RemoveBooking);
            SearchByIdOrGuestOrRoomCommand = new CommandBaseAsync(_ => SearchBooking());
        }

        public event Action<Booking> BookingUpdate;
        public event Action<BookingDTOConverter> BookingView;

        private async Task RemoveBooking(object arg)
        {
            try
            {
                if (arg is BookingDTOConverter booking)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"Are you sure you want to remove the booking {booking.BookingNumber}? Removing it means deleting its payment if it has been paid!", "Warning",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        await _appManagerBL.BookingBL.DeleteBooking(booking.Booking);
                        await _dataService.GetAllPaymentsAsync();
                        BookingsList.Remove(booking);
                        OnPropertyChanged(nameof(RegisteredBookings));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing guest: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task SearchBooking()
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchBox))
                {
                    var bookings = await _appManagerBL.BookingBL.FilterBookings(SearchBox);
                    BookingsList.Clear();

                    foreach (var booking in bookings)
                    {
                        BookingsList.Add(new BookingDTOConverter(booking));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong filtering bookings by bookings id or guest name or room number: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
