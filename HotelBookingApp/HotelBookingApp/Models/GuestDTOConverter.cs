using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBookingApp.Models
{
    public class GuestDTOConverter
    {
        public Guest Guest { get; }

        public string GuestFullName => $"{Guest.FirstName} {Guest.LastName}";
        public int NumberOfBookings => Guest.Bookings.Count;
        public int NumberOfPayments => Guest.Payments.Count;

        public string Email => Guest.Email;
        public string PhoneNumber => Guest.PhoneNumber;

        public List<Booking> BookingsList => Guest.Bookings.ToList();

        public GuestDTOConverter(Guest guest)
        {
            Guest = guest;
        }
    }
}
