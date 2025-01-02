using HotelBookingDAL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingBL
{
    /// <summary>
    ///  Business Logic Layer (BL) for managing booking operations. 
    ///  Provides methods for retrieving, adding, updating, deleting bookings, and handling payments.
    /// </summary>
    public class BookingBL
    {
        private readonly IRepository<Booking> _booking;
        private readonly IRepository<Payment> _payment;

        public BookingBL(IRepository<Booking> repository, IRepository<Payment> payment)
        {
            _booking = repository;
            _payment = payment;
        }


        /// <summary>
        ///   Retrieves all bookings from the repository.
        /// </summary>
        /// <returns> A task that represents the asynchronous operation, containing the list of all bookings </returns>
        public async Task<IEnumerable<Booking>> GetAllBookings() => await _booking.GetAllAsync();


        /// <summary>
        ///   Adds a new booking to the repository, along with creating a payment if the booking is paid.
        /// </summary>
        /// <param name="booking"> The booking entity to add </param>
        /// <param name="guest"> The guest associated with the booking </param>
        /// <param name="paymentMethod"> The method of payment for the booking </param>
        /// <returns> A task that represents the asynchronous operation, containing the added booking </returns>
        /// <exception cref="Exception"> Thrown if the room is not available or the booking dates are invalid </exception>
        public async Task<Booking> AddBooking(Booking booking, Guest guest, PaymentMethod paymentMethod)
        {
            if (!booking.Room.IsAvailable) throw new Exception("The selected room is not available");

            if (!await IsBookingDateValid(booking))
            {
                throw new Exception($"Check-in date must be before check-out date, or the selected date has already been booked for this room {booking.Room.RoomNumber}");
            }

            Payment? newPayment = null;

            if (booking.IsPaid)
            {
                newPayment = new Payment
                {
                    PaymentMethod = paymentMethod,
                    Amount = booking.TotalAmount,
                    GuestId = guest.GuestId,
                };
            }

            booking.GuestId = guest.GuestId;
            await _booking.AddAsync(booking, newPayment);
            return booking;
        }


        /// <summary>
        ///   Checks if the booking's dates are valid (not conflicting with any existing bookings for the same room).
        /// </summary>
        /// <param name="booking"> The booking entity to check </param>
        /// <returns>A task containing a boolean indicating if the booking's dates are valid (true) or invalid (false) </returns>
        public async Task<bool> IsBookingDateValid(Booking booking)
        {
            var allBookings = await _booking.GetAllAsync();

            if (allBookings.Any(b => b.RoomId == booking.RoomId && b.BookingId != booking.BookingId &&
                !(booking.CheckOutDate <= b.CheckInDate || booking.CheckInDate >= b.CheckOutDate)))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        ///   Updates an existing booking in the repository and creates a new payment if the booking is paid.
        /// </summary>
        /// <param name="booking"> The booking entity with updated details </param>
        /// <param name="paymentMethod"> The payment method for the booking </param>
        /// <returns> A task that represents the asynchronous operation </returns>
        /// <exception cref="Exception"> Thrown if the guest has already paid or the booking's dates are invalid </exception>
        public async Task UpdateBooking(Booking booking, PaymentMethod paymentMethod)
        {
            if (booking.Payment != null) throw new Exception("The guest has already paid the total price!");

            if (!await IsBookingDateValid(booking))
            {
                throw new Exception("Check-in date must be before check-out date, or the selected date has already been booked");
            }

            Payment? newPayment = null;

            if (booking.IsPaid && booking.Payment == null)
            {
                newPayment = new Payment
                {
                    PaymentMethod = paymentMethod,
                    Amount = booking.TotalAmount,
                    GuestId = booking.GuestId,
                };
            }

            await _booking.UpdateAsync(booking, newPayment);
        }


        /// <summary>
        ///  Completes the payment for a booking and updates the booking and payment records.
        /// </summary>
        /// <param name="booking"> The booking entity for which the payment is to be completed/updated </param>
        /// <returns> A task that represents the asynchronous operation </returns>
        /// <exception cref="Exception"> Thrown if no payment has been made or the booking's dates are invalid </exception>
        public async Task CompletePayment_UpdateBooking(Booking booking)
        {
            if (booking.Payment == null && !booking.IsPaid) throw new Exception("There is no payment made by the guest to update for this booking");

            if (!await IsBookingDateValid(booking))
            {
                throw new Exception("Check-in date must be before check-out date, or the selected date has already been booked");
            }

            // set the new amount
            booking.Payment.Amount = booking.TotalAmount;

            await _payment.UpdateAsync(booking.Payment);
            await _booking.UpdateAsync(booking, null);
        }


        /// <summary>
        ///  Calculates the new price for a booking, determining the amount to be paid or refunded.
        /// </summary>
        /// <param name="totalAmount"> The total amount for the booking </param>
        /// <param name="payment"> The payment entity associated with the booking </param>
        /// <returns> A tuple containing the amount to receive and the amount to pay </returns>
        public static (decimal Receive, decimal Pay) CalculateBookingNewPrice(decimal totalAmount, Payment payment)
        {
            decimal pay = totalAmount >= payment.Amount ? (totalAmount - payment.Amount) : 0;
            decimal receive = totalAmount <= payment.Amount ? (payment.Amount - totalAmount) : 0;
            return (receive, pay);
        }

        /// <summary>
        ///  Deletes a booking from the repository.
        /// </summary>
        /// <param name="booking"> The booking entity to delete </param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public async Task DeleteBooking(Booking booking) => await _booking.DeleteAsync(booking);


        /// <summary>
        ///  Filters bookings based on a search string. Searching for either the guest's full name, 
        ///  the exact booking id or the exact room number matching the provided text.
        /// </summary>
        /// <param name="searchTxt"> The search text to filter bookings </param>
        /// <returns> A task that represents the asynchronous operation, containing the filtered list of bookings.</returns>
        public async Task<IEnumerable<Booking>> FilterBookings(string searchTxt) => await _booking.FindAsync(searchTxt);
    }
}