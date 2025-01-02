using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingDTO
{
    /// <summary>
    ///   Represents different types of rooms available in the system.
    /// </summary>
    public enum RoomType
    {
        Single, Double, Suite
    }


    /// <summary>
    ///   Represents a room entity with details.
    /// </summary>
    public class Room
    {
        /// <summary>
        ///  Unique identifier for the room.
        /// </summary>
        [Key]
        public int RoomId { get; set; }


        /// <summary>
        ///   Room number, which must be between 1 and 3 digits.
        /// </summary>
        [Required, Range(1,3)]
        public int RoomNumber { get; set; }


        /// <summary>
        ///   Type of the room, which can be Single, Double, or Suite.
        /// </summary>
        [Required]
        public RoomType RoomType { get; set; }


        /// <summary>
        ///   Optional description of the room, limited to 200 characters.
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;


        /// <summary>
        ///   Indicates whether the room is currently available for booking.
        /// </summary>
        [Required]
        public bool IsAvailable { get; set; }


        /// <summary>
        ///   Price of the room, stored as a decimal with a precision of 18 and a scale of 2.
        /// </summary>
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }


        /// <summary>
        ///  Collection of bookings associated with the room.
        /// </summary>
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
