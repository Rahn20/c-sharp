using HotelBookingApp.Models;
using HotelBookingBL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Services
{
    public class DataInitializationService : IDataInitializationService
    {
        private readonly AppManagerBL _appManagerBL;

        public DataInitializationService(AppManagerBL appManagerBL)
        {
            _appManagerBL = appManagerBL;

            Rooms = new ObservableCollection<RoomDTOConverter>();
            Payments = new ObservableCollection<PaymentDTOConverter>();
            Guests = new ObservableCollection<GuestDTOConverter>();
            Bookings = new ObservableCollection<BookingDTOConverter>();
        }

        #region List properties
        public ObservableCollection<RoomDTOConverter> Rooms { get; }
        public ObservableCollection<PaymentDTOConverter> Payments { get; }
        public ObservableCollection<GuestDTOConverter> Guests { get; }
        public ObservableCollection<BookingDTOConverter> Bookings { get; }
        #endregion


        public async Task InitializeAsync()
        {
            //await Task.WhenAll(
            //    GetAllBookingsAsync(),
            //    GetAllGuestsAsync(),
            //    GetAllRoomsAsync(),
            //    GetAllPaymentsAsync()
            //);
            await GetAllBookingsAsync();
            await GetAllGuestsAsync();
            await GetAllRoomsAsync();
            await GetAllPaymentsAsync();  
        }

        public async Task GetAllBookingsAsync()
        {
            Bookings.Clear();

            List<Booking> allBookings = (List<Booking>)await _appManagerBL.BookingBL.GetAllBookings();

            allBookings.ForEach(booking =>
            {
                Bookings.Add(new BookingDTOConverter(booking));
            });
        }

        public async Task GetAllGuestsAsync()
        {
            Guests.Clear();
            List<Guest> allGuests = (List<Guest>)await _appManagerBL.GuestBL.GetAllGuests();

            allGuests.ForEach(guest =>
            {
                Guests.Add(new GuestDTOConverter(guest));
            });
        }

        public async Task GetAllPaymentsAsync()
        {
            Payments.Clear();
            List<Payment> allPayments = (List<Payment>)await _appManagerBL.PaymentBL.GetAllPayments();

            allPayments.ForEach(payment =>
            {
                Payments.Add(new PaymentDTOConverter(payment));
            });
        }

        public async Task GetAllRoomsAsync()
        {
            Rooms.Clear();
            List<Room> rooms = (List<Room>)await _appManagerBL.RoomBL.GetAllRooms();

            rooms.ForEach(room =>
            {
                var createRoom = new RoomDTOConverter(room);
                Rooms.Add(createRoom);
            });
        }
    }
}
