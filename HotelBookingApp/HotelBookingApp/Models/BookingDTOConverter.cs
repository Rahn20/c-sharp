using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBookingApp.Models
{
    public class BookingDTOConverter
    {
        public Booking Booking { get; }

        #region Booking info
        public string BookingNumber => Booking.BookingId.ToString();
        public string CheckInDate => Booking.CheckInDate.ToString("g");
        public string CheckOutDate => Booking.CheckOutDate.ToString("g");
        public string IsPaid => Booking.IsPaid ? "Paid" : "Not Paid";
        public decimal TotalAmount => Booking.TotalAmount;
        public int TotalDays => (Booking.CheckOutDate - Booking.CheckInDate).Days;
        #endregion

        #region Guest info
        public string GuestFullName => $"{Booking.Guest.FirstName} {Booking.Guest.LastName}";
        public string GuestEmail => Booking.Guest.Email;
        public string GuestPhone => Booking.Guest.PhoneNumber;
        #endregion

        #region Room and payment info
        public string RoomNumber => Booking.Room.RoomNumber.ToString();
        public string RoomType => Booking.Room.RoomType.ToString();
        public string RoomPrice => Booking.Room.Price.ToString();
        public string PaymentMethod => Booking.Payment != null ? Booking.Payment.PaymentMethod.ToString() : "-";
        #endregion


        public BookingDTOConverter(Booking booking)
        {
            Booking = booking;
        }
    }
}