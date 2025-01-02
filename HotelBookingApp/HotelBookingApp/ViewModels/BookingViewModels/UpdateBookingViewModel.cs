using HotelBookingApp.Core;
using HotelBookingApp.Services;
using HotelBookingBL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelBookingApp.ViewModels.BookingViewModels
{
    public class UpdateBookingViewModel : ViewModelBase, IDisposable
    {
        private readonly AppManagerBL _appManagerBL;
        private readonly INavigationService _navigationService;
        private readonly BookingsListViewModel _bookingsListViewModel;
        private readonly IDataInitializationService _dataService;
        private Booking _currentBooking;


        public BookingViewModel BookingViewModel { get; private set; }

        public PaymentMethod[] PaymentMethods { get; }
        public int RoomNumber => _currentBooking.Room.RoomNumber;
        public string GuestFullName => $"{_currentBooking.Guest.FirstName} {_currentBooking.Guest.LastName}";
        public bool IsBookingPaid => _currentBooking.IsPaid && _currentBooking.Payment != null;
        public bool BookingNotPaid => !_currentBooking.IsPaid && _currentBooking.Payment == null;


        private bool _showPayment = false;
        public bool ShowPayment
        {
            get => _showPayment;
            set
            {
                _showPayment = value;
                OnPropertyChanged(nameof(ShowPayment));
            }
        }

        public ICommand UpdateBookingCommand { get; }
        public ICommand PayAndUpdateCommand { get; }

        public UpdateBookingViewModel(INavigationService navigationService, AppManagerBL appManagerBL,
            BookingsListViewModel bookingsListViewModel, IDataInitializationService dataService)
        {
            _appManagerBL = appManagerBL;
            _navigationService = navigationService;
            _dataService = dataService;
            _bookingsListViewModel = bookingsListViewModel;
            _bookingsListViewModel.BookingUpdate += OnBookingUpdate;

            BookingViewModel = new BookingViewModel();
            PaymentMethods = Enum.GetValues<PaymentMethod>().Cast<PaymentMethod>().ToArray();
            UpdateBookingCommand = new CommandBaseAsync(_ => UpdateBooking());
            PayAndUpdateCommand = new CommandBaseAsync(_ => PayAndUpdateBooking());
        }

        private void OnBookingUpdate(Booking booking)
        {
            _currentBooking = booking;
            BookingViewModel.Room = _currentBooking.Room;
            BookingViewModel.Payment = _currentBooking.Payment;

            // Repeat the process to pass the date validations, as it is reading the previous date's data
            BookingViewModel.CheckInDate = _currentBooking.CheckInDate;
            BookingViewModel.CheckOutDate = _currentBooking.CheckOutDate;

            BookingViewModel.CheckOutDate = _currentBooking.CheckOutDate;
            BookingViewModel.CheckInDate = _currentBooking.CheckInDate;
            BookingViewModel.TotalAmount = _currentBooking.TotalAmount;
            ShowPayment = false;
        }

        // If IsBookingPaid is true, then update the payment and the booking.
        private async Task PayAndUpdateBooking()
        {
            try
            {
                _currentBooking.CheckInDate = BookingViewModel.CheckInDate;
                _currentBooking.CheckOutDate = BookingViewModel.CheckOutDate;
                _currentBooking.TotalAmount = BookingViewModel.TotalAmount;

                await _appManagerBL.BookingBL.CompletePayment_UpdateBooking(_currentBooking);
                _navigationService.NavigateTo<BookingsListViewModel>();
            }
            catch (Exception ex)
            {
                BookingViewModel.ErrorMessage = ex.Message;
            }
        }


        // If BookingNotPaid is true, the user can choose to update the booking and pay the total amount needed.
        private async Task UpdateBooking()
        {
            try
            {
                _currentBooking.CheckInDate = BookingViewModel.CheckInDate;
                _currentBooking.CheckOutDate = BookingViewModel.CheckOutDate;
                _currentBooking.TotalAmount = BookingViewModel.TotalAmount;
                _currentBooking.IsPaid = ShowPayment;

                await _appManagerBL.BookingBL.UpdateBooking(_currentBooking, BookingViewModel.SelectedPaymentMethod);
                await _dataService.GetAllPaymentsAsync();
                _navigationService.NavigateTo<BookingsListViewModel>();
            }
            catch (Exception ex)
            {
                BookingViewModel.ErrorMessage = ex.Message;
            }
        }

        public void Dispose()
        {
            _bookingsListViewModel.BookingUpdate -= OnBookingUpdate; 
            GC.SuppressFinalize(this);
        }
    }
}
