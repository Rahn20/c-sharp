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
    ///   Data Access Layer (DAL) for managing operations related to the Payment entity.
    ///   Implements the IRepository interface to provide CRUD operations.
    /// </summary>
    public class PaymentDAL : IRepository<Payment>
    {
        private readonly HotelBookingContext _context;

        /// <summary>
        ///    Initializes a new instance of the PaymentDAL class with the provided database context.
        /// </summary>
        /// <param name="hotelBookingContext">The database context for the hotel booking system </param>
        public PaymentDAL(HotelBookingContext hotelBookingContext)
        {
            _context = hotelBookingContext;
        }


        public async Task AddAsync(Payment entity, Payment? payment = null)
        {
            try
            {
                _context.Payments.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding a new payment to the database", ex);
            }
        }

        public async Task DeleteAsync(Payment entity)
        {
            try
            {
                _context.Payments.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting the payment with payment ID {entity.PaymentId} from the database", ex);
            }
        }


        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            try
            {
                return await _context.Payments
                    .Include(p => p.Guest)
                    .Include(p => p.Booking)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving the payments from the database", ex);
            }
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            try
            {
                Payment? payment = await _context.Payments
                    .Include(p => p.Guest)
                    .Include(p => p.Booking)
                    .FirstOrDefaultAsync(p => p.PaymentId == id);

                return payment is null ? throw new Exception("Payment is not found") : payment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Payment entity, Payment? payment = null)
        {
            try
            {
                Payment? existingPayment = await _context.Payments.FindAsync(entity.PaymentId);

                if (existingPayment is null) throw new Exception("Payment is not found");

                existingPayment.PaymentMethod = entity.PaymentMethod;
                existingPayment.Amount = entity.Amount;
                existingPayment.PaymentUpdatedDate = DateTime.Now;

                _context.Payments.Update(existingPayment);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Payment>> FindAsync(string searchStr)
        {
            try
            {
                return await _context.Payments
                    .Where(p => p.Booking.Room.RoomNumber.ToString() == searchStr || 
                        p.Guest.FirstName.Contains(searchStr) || p.Guest.LastName.Contains(searchStr))
                    .Include(p => p.Guest)
                    .Include(p => p.Booking)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while searching for payments with the search string: {searchStr}", ex);
            }
        }
    }
}
