using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingDTO
{
    /// <summary>
    ///  Represents a booking entity with details.
    /// </summary>
    public class Booking
    {
        /// <summary>
        ///  Unique identifier for the booking.
        /// </summary>
        [Key]
        public int BookingId { get; set; }


        /// <summary>
        ///   Date and time of check-in for the booking.
        /// </summary>
        [Required]
        public DateTime CheckInDate { get; set; }


        /// <summary>
        ///   Date and time of check-out for the booking.
        /// </summary>
        [Required]
        public DateTime CheckOutDate { get; set; }


        /// <summary>
        ///   Indicates whether the booking has been paid for. Defaults to false.
        /// </summary>
        [Required]
        public bool IsPaid { get; set; } = false;


        /// <summary>
        ///   Total amount for the booking, stored as a decimal with a precision of 18 and a scale of 2.
        /// </summary>
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }


        /// <summary>
        ///   Identifier for the room associated with the booking. (relation)
        /// </summary>
        [Required]
        public int RoomId { get; set; }

        /// <summary>
        ///   Room entity associated with the booking.
        /// </summary>
        public Room Room { get; set; }

        /// <summary>
        ///   Identifier for the guest associated with the booking. (relation)
        /// </summary>
        [Required]
        public int GuestId { get; set; }

        /// <summary>
        ///   Guest entity associated with the booking.
        /// </summary>
        public Guest Guest { get; set; }


        /// <summary>
        ///   Identifier for the payment associated with the booking, if any. (relation)
        /// </summary>
        public int? PaymentId { get; set; }

        /// <summary>
        ///   Payment entity associated with the booking, if applicable.
        /// </summary>
        public Payment? Payment { get; set; }
    }
}
