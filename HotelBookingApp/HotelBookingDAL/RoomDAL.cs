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
    ///   Data Access Layer (DAL) for managing operations related to the Room entity.
    ///   Implements the IRepository interface to provide CRUD operations.
    /// </summary>
    public class RoomDAL : IRepository<Room>
    {
        private readonly HotelBookingContext _context;


        /// <summary>
        ///   Initializes a new instance of the RoomDAL class with the provided database context.
        /// </summary>
        /// <param name="hotelBookingContext"> The database context for the hotel booking system </param>
        public RoomDAL(HotelBookingContext hotelBookingContext)
        {
            _context = hotelBookingContext;
        }


        public async Task AddAsync(Room entity, Payment? payment = null)
        {
            try
            {
                bool validateRoom = await _context.Rooms.AnyAsync(r => r.RoomNumber == entity.RoomNumber);

                if (validateRoom) throw new Exception("The room number already esists in the database");
                _context.Rooms.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(Room entity)
        {
            try
            {
                // Explicitly load related Bookings
                _context.Entry(entity).Collection(r => r.Bookings).Load();

                // Manually delete related entities
                _context.Bookings.RemoveRange(entity.Bookings);

                _context.Rooms.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting the room with room ID {entity.RoomId} from the database", ex);
            }
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            try
            {
                return await _context.Rooms
                    .Include(r => r.Bookings)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving the rooms from the database", ex);
            }
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            try
            {
                Room? room = await _context.Rooms
                    .Include(r => r.Bookings)
                    .FirstOrDefaultAsync(r => r.RoomId == id);

                return room is null ? throw new Exception("Room not found") : room;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Room entity, Payment? payment = null)
        {
            try
            {
                Room? existingRoom = await _context.Rooms.FindAsync(entity.RoomId) ?? throw new Exception("Room is not found");
                existingRoom.Description = entity.Description;
                existingRoom.Price = entity.Price;
                existingRoom.IsAvailable = entity.IsAvailable;
                existingRoom.RoomType = entity.RoomType;

                _context.Rooms.Update(existingRoom);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while updating the room with room ID {entity.RoomId} in the database.", ex);
            }
        }

        public async Task<IEnumerable<Room>> FindAsync(string searchStr)
        {
            throw new NotImplementedException();
        }
    }
}
