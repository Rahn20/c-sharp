using HotelBookingApp.Core;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
using HotelBookingApp.ViewModels.GuestViewModels;
using HotelBookingApp.ViewModels.PaymentViewModels;
using HotelBookingApp.ViewModels.RoomViewModels;
using HotelBookingBL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelBookingApp.ViewModels.BookingViewModels
{
    public class AddBookingViewModel : ViewModelBase, IDisposable
    {
        private readonly AppManagerBL _appManagerBL;
        private readonly INavigationService _navigationService;
        private readonly RoomsListViewModel _roomsListViewModel;
        private readonly GuestsListViewModel _guestListViewModel;
        private readonly BookingsListViewModel _bookingListViewModel;
        private readonly PaymentsListViewModel _paymentListViewModel;
        private Room _currentRoom;


        public BookingViewModel BookingViewModel { get; }
        public PaymentMethod[] PaymentMethods { get; }
        public ObservableCollection<GuestDTOConverter> GuestList => _guestListViewModel.GuestList;
        public int RoomNumber => _currentRoom.RoomNumber;
        public string RoomPrice => $"{_currentRoom.Price} $";
        public string SelectedGuestName { get; set; }

        public string AddBookingButtonTxt => ShowPayment ? "Pay and Make Reservation" : "Make Reservation";


        private Guest _selectedGuest;
        public Guest SelectedGuest
        {
            get => _selectedGuest;
            set 
            {
                _selectedGuest = value;
                OnPropertyChanged(nameof(SelectedGuest));

                SelectedGuestName = $"{value.FirstName} {value.LastName}";
                OnPropertyChanged(nameof(SelectedGuestName));
            }
        }

        private bool _showPayment = false;
        public bool ShowPayment
        {
            get => _showPayment;
            set
            {
                _showPayment = value;
                OnPropertyChanged(nameof(ShowPayment));
                OnPropertyChanged(nameof(AddBookingButtonTxt));
            }
        }

        public ICommand MakeReservationCommand { get; }

        public AddBookingViewModel(INavigationService navigationService, AppManagerBL appManagerBL,
            RoomsListViewModel roomsListViewModel, GuestsListViewModel guests, BookingsListViewModel bookingsListVM,
            PaymentsListViewModel paymentsListViewModel)
        {
            _appManagerBL = appManagerBL;
            _navigationService = navigationService;
            _roomsListViewModel = roomsListViewModel;
            _guestListViewModel = guests;
            _bookingListViewModel = bookingsListVM;
            _paymentListViewModel = paymentsListViewModel;

            _roomsListViewModel.AddBooking += OnAddBooking;

            BookingViewModel = new BookingViewModel();
            PaymentMethods = Enum.GetValues<PaymentMethod>().Cast<PaymentMethod>().ToArray();
            MakeReservationCommand = new CommandBaseAsync(_  => AddBooking());
        }

        private void OnAddBooking(Room room)
        {
            _currentRoom = room;
            BookingViewModel.Room = room;

            // clear the incputs
            //BookingViewModel.TotalAmount = 0;
            BookingViewModel.CheckOutDate = DateTime.Now.AddDays(7);
            BookingViewModel.CheckInDate = DateTime.Now;
            ShowPayment = false;
        }

        private async Task AddBooking()
        {
            try
            {
                if (SelectedGuest is null)
                {
                    BookingViewModel.ErrorMessage = "The guest is not selected!";
                    return;
                }

                Booking newBooking = new Booking
                {
                    Room = _currentRoom,
                    RoomId = _currentRoom.RoomId,
                    CheckOutDate = BookingViewModel.CheckOutDate,
                    CheckInDate = BookingViewModel.CheckInDate,
                    IsPaid = ShowPayment,
                    TotalAmount = BookingViewModel.TotalAmount,
                };

               await _appManagerBL.BookingBL.AddBooking(
                    booking: newBooking,
                    guest: SelectedGuest, 
                    paymentMethod: BookingViewModel.SelectedPaymentMethod);

                // get the new payment
                if (ShowPayment && newBooking.Payment != null)
                {
                    _paymentListViewModel.PaymentList.Add(new PaymentDTOConverter(newBooking.Payment));
                }

                _bookingListViewModel.BookingsList.Add(new BookingDTOConverter(newBooking));
                _navigationService.NavigateTo<RoomsListViewModel>();
            }
            catch (Exception ex) 
            {
                BookingViewModel.ErrorMessage = ex.Message;
            }
        }

        public void Dispose()
        {
           _roomsListViewModel.AddBooking -= OnAddBooking;
            GC.SuppressFinalize(this);
        }
    }
}
