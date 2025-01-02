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
    ///   Business Logic Layer (BL) for managing room operations. 
    ///   Provides methods for retrieving, adding, updating, and deleting rooms.
    /// </summary>
    public class RoomBL
    {
        private readonly IRepository<Room> _room;

        public RoomBL(IRepository<Room> repository)
        {
            _room = repository;
        }


        /// <summary>
        ///  Retrieves all rooms from the repository.
        /// </summary>
        /// <returns> A task that represents the asynchronous operation, containing the list of all rooms </returns>
        public async Task<IEnumerable<Room>> GetAllRooms() => await _room.GetAllAsync();


        /// <summary>
        ///    Retrieves a room by its ID from the repository.
        /// </summary>
        /// <param name="roomId"> The ID of the room to retrieve </param>
        /// <returns> the room with the specified ID </returns>
        /// <exception cref="Exception"> Thrown when the room ID is less than or equal to zero </exception>
        public async Task<Room> GetRoomById(int roomId)
        {
            if (roomId <= 0) throw new Exception($"ID must be greater than zero, RoomId: {roomId}");

            return await _room.GetByIdAsync(roomId);
        }


        /// <summary>
        ///   Adds a new room to the repository.
        /// </summary>
        /// <param name="room">The room entity to add.</param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public async Task AddRoom(Room room) => await _room.AddAsync(room);


        /// <summary>
        ///   Checks if the given room number is valid (thats not already in use by another room).
        /// </summary>
        /// <param name="roomNumber"> The room number to check for uniqueness. </param>
        /// <returns> A boolean indicating if the room number is valid </returns>
        public async Task<bool> IsRoomNumberValid(int roomNumber)
        {
            var allRooms = await _room.GetAllAsync();
            return !allRooms.Any(room => room.RoomNumber == roomNumber);
        }

        /// <summary>
        ///   Updates the details of an existing room in the repository.
        /// </summary>
        /// <param name="room"> The room entity with updated details </param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public async Task UpdateRoom(Room room) => await _room.UpdateAsync(room);

        /// <summary>
        ///   Deletes a room from the repository.
        /// </summary>
        /// <param name="room"> The room entity to delete </param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public async Task DeleteRoom(Room room) => await _room.DeleteAsync(room);
    }
}
