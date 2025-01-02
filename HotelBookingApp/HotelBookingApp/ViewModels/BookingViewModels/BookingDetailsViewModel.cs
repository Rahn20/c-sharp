using HotelBookingApp.Core;
using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.ViewModels.BookingViewModels
{
    public class BookingDetailsViewModel : ViewModelBase, IDisposable
    {
        private readonly BookingsListViewModel _bookingsListViewModel;

        public BookingDTOConverter CurrentBooking { get; private set; }
        public string BookingsNumber => $"Booking Number: {CurrentBooking.BookingNumber}";


        public BookingDetailsViewModel(BookingsListViewModel bookingsListViewModel) 
        { 
            _bookingsListViewModel = bookingsListViewModel;
            _bookingsListViewModel.BookingView += OnViewBooking;
        }

        private void OnViewBooking(BookingDTOConverter BookingConverter)
        {
            CurrentBooking = BookingConverter;
        }

        public void Dispose()
        {
            _bookingsListViewModel.BookingView -= OnViewBooking;
            GC.SuppressFinalize(this);
        }
    }
}
