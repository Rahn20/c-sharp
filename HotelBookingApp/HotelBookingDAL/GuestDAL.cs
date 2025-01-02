using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingDAL.DbContexts;
using HotelBookingDTO;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingDAL
{
    /// <summary>
    ///   Data Access Layer (DAL) for managing operations related to the Guest entity.
    ///   Implements the IRepository interface to provide CRUD operations.
    /// </summary>
    public class GuestDAL : IRepository<Guest>
    {
        private readonly HotelBookingContext _context;


        /// <summary>
        ///   Initializes a new instance of the GuestDAL class with the provided database context.
        /// </summary>
        /// <param name="hotelBookingContext">The database context for the hotel booking system </param>
        public GuestDAL(HotelBookingContext hotelBookingContext)
        {
            _context = hotelBookingContext;
        }


        public async Task AddAsync(Guest entity, Payment? payment = null)
        {
            try
            {
                _context.Guests.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception("Error while adding a new guest to the database", ex);
            }
        }

        public async Task DeleteAsync(Guest entity)
        {
            try
            {
                // Explicitly load related Bookings
                _context.Entry(entity).Collection(g => g.Bookings).Load();
                _context.Entry(entity).Collection(g => g.Payments).Load();

                // Manually delete related entities
                _context.Payments.RemoveRange(entity.Payments);
                _context.Bookings.RemoveRange(entity.Bookings);

                _context.Guests.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting the guest with guest ID {entity.GuestId} from the database", ex);
            }
        }

        public async Task<IEnumerable<Guest>> GetAllAsync()
        {
            try
            {
                return await _context.Guests
                    .Include(g => g.Bookings)
                    .Include(g => g.Payments)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving the guests from the database", ex);
            }
        }

        public async Task<Guest> GetByIdAsync(int id)
        {
            try
            {
                Guest? guest = await _context.Guests
                    .Include(g => g.Bookings)
                    .Include(g => g.Payments)
                    .FirstOrDefaultAsync(g => g.GuestId == id);

                return guest is null ? throw new Exception("Guest not found") : guest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Guest entity, Payment? payment = null)
        {
            try
            {
                Guest? existingGuest = await _context.Guests.FindAsync(entity.GuestId) ?? throw new Exception("Guest is not found");
                existingGuest.PhoneNumber = entity.PhoneNumber;
                existingGuest.FirstName = entity.FirstName;
                existingGuest.LastName = entity.LastName;

                _context.Guests.Update(existingGuest);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<IEnumerable<Guest>> FindAsync(string searchStr)
        {
            try
            {
                return await _context.Guests
                    .Where(g => g.FirstName.Contains(searchStr) || g.LastName.Contains(searchStr))
                    .Include(g => g.Bookings)
                    .Include(g => g.Payments)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while searching for guests with the search string: {searchStr}", ex);
            }
        }
    }
}
