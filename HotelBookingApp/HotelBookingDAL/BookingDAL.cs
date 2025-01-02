using HotelBookingDAL.DbContexts;
using HotelBookingDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingDAL
{
    /// <summary>
    ///   Data Access Layer (DAL) for managing operations related to the Booking entity.
    ///   Implements the IRepository interface to provide CRUD operations.
    /// </summary>
    public class BookingDAL : IRepository<Booking>
    {
        private readonly HotelBookingContext _context;


        /// <summary>
        ///   Initializes a new instance of the BookingDAL class with the provided database context.
        /// </summary>
        /// <param name="hotelBookingContext"> The database context for the hotel booking system </param>
        public BookingDAL(HotelBookingContext hotelBookingContext)
        {
            _context = hotelBookingContext;
        }

        public async Task AddAsync(Booking entity, Payment? payment = null)
        {
            try
            {
                _context.Bookings.Add(entity);
                await _context.SaveChangesAsync();

                // Associate the payment with the booking
                if (entity.IsPaid && payment != null) 
                {
                    payment.BookingId = entity.BookingId;
                    _context.Payments.Add(payment);
                    await _context.SaveChangesAsync();

                    // Update the booking to include the PaymentId
                    entity.PaymentId = payment.PaymentId;
                    _context.Bookings.Update(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding a new booking to the database", ex);
            }
        }

        public async Task DeleteAsync(Booking entity)
        {
            try
            {
                _context.Bookings.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting the booking with booking ID {entity.BookingId} from the database", ex);
            }
        }


        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            try
            {
                return await _context.Bookings
                    .Include(b => b.Guest)
                    .Include(b => b.Room)
                    .Include(b => b.Payment)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving the bookings from the database", ex);
            }
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            try
            {
                Booking? booking = await _context.Bookings
                    .Include(b => b.Guest)
                    .Include(b => b.Room)
                    .Include(b => b.Payment)
                    .FirstOrDefaultAsync(b => b.BookingId == id);

                return booking is null ? throw new Exception("Booking not found") : booking;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while retrieving the specific booking with booking ID {id} from the database", ex);
            }
        }

        public async Task UpdateAsync(Booking entity, Payment? payment = null)
        {
            try
            {
                Booking? existingBooking = await _context.Bookings.FindAsync(entity.BookingId);

                if (existingBooking is null) throw new Exception("Booking is not found");

                existingBooking.CheckOutDate = entity.CheckOutDate;
                existingBooking.CheckInDate = entity.CheckInDate;
                existingBooking.TotalAmount = entity.TotalAmount;
                existingBooking.IsPaid = entity.IsPaid;

                // Associate the payment with the booking
                if (entity.IsPaid && payment != null)
                {
                    payment.BookingId = existingBooking.BookingId;
                    _context.Payments.Add(payment);
                    await _context.SaveChangesAsync();

                    // Update the booking to include the PaymentId
                    existingBooking.PaymentId = payment.PaymentId;
                }

                _context.Bookings.Update(existingBooking);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Booking>> FindAsync(string searchStr)
        {
            try
            {
                return await _context.Bookings
                    .Where(b => b.Room.RoomNumber.ToString() == searchStr ||
                        b.BookingId.ToString() == searchStr || 
                        b.Guest.FirstName.Contains(searchStr) || b.Guest.LastName.Contains(searchStr))
                    .Include(b => b.Guest)
                    .Include(b => b.Room)
                    .Include(b => b.Payment)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while searching for bookings with the search string: {searchStr}", ex);
            }
        }
    }
}
