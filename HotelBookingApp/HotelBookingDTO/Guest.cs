using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HotelBookingDTO
{
    /// <summary>
    ///   Represents a guest entity with details.
    /// </summary>
    public class Guest
    {
        /// <summary>
        ///   Unique identifier for the guest.
        /// </summary>
        [Key]
        public int GuestId { get; set; }


        /// <summary>
        ///   First name of the guest, limited to a maximum of 30 characters.
        /// </summary>
        [Required, MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;


        /// <summary>
        ///  Last name of the guest, limited to a maximum of 30 characters.
        /// </summary>
        [Required, MaxLength(30)]
        public string LastName { get; set; } = string.Empty;


        /// <summary>
        ///   Email address of the guest. Must be a valid email format.
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;


        /// <summary>
        ///  Phone number of the guest, limited to a maximum of 10 characters.
        /// </summary>
        [Required, MaxLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;


        /// <summary>
        ///   Collection of bookings made by the guest.
        /// </summary>
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();


        /// <summary>
        ///  Collection of payments made by the guest.
        /// </summary>
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
