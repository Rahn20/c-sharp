using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBookingApp.Models
{
    public class PaymentDTOConverter
    {
        public Payment Payment { get; }

        public string Amount => Payment.Amount.ToString("F2");
        public string PaymentDate => Payment.PaymentDate.ToString("g");
        public string PaymentMethod => Payment.PaymentMethod.ToString();
        public string BookingNumber => Payment.BookingId.ToString();
        public string GuestFullName => $"{Payment.Guest.FirstName} {Payment.Guest.LastName}";
        public string RoomNumber => Payment.Booking.Room.RoomNumber.ToString();
        public string LastPaymentUpdated => Payment.PaymentUpdatedDate != null ? Payment.PaymentUpdatedDate.ToString() : "-";

        public PaymentDTOConverter(Payment payment) 
        { 
            Payment = payment;
        }
    }
}
