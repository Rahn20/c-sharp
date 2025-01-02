using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingDTO
{
    /// <summary>
    ///   Represents different payments method available for transactions.
    /// </summary>
    public enum PaymentMethod
    {
        CreditCard,
        PayPal, 
        Cash, 
        GiftCard, 
        DigitalWallet
    }

    /// <summary>
    ///   Represents a payment entity with details.
    /// </summary>
    public class Payment
    {
        /// <summary>
        ///   Unique identifier for the payment.
        /// </summary>
        [Key]
        public int PaymentId { get; set; }


        /// <summary>
        ///   Amount of the payment, stored as a decimal with a precision of 18 and a scale of 2.
        /// </summary>
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }


        /// <summary>
        ///   Date and time when the payment was made. Defaults to the current date and time.
        /// </summary>
        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;


        /// <summary>
        ///   Date and time when the payment was last updated, if applicable.
        /// </summary>
        public DateTime? PaymentUpdatedDate { get; set; }


        /// <summary>
        ///   Method used for the payment, such as CreditCard, PayPal, or Cash.
        /// </summary>
        [Required]
        public PaymentMethod PaymentMethod { get; set; }


        /// <summary>
        ///   Identifier for the guest associated with the payment. (relation)
        /// </summary>
        [Required]
        public int GuestId { get; set; }

        /// <summary>
        ///   Guest entity associated with the payment.
        /// </summary>
        public Guest Guest { get; set; }


        /// <summary>
        ///   Identifier for the booking associated with the payment. (relation)
        /// </summary>
        [Required]
        public int BookingId { get; set; }

        /// <summary>
        ///   Booking entity associated with the payment.
        /// </summary>
        public Booking Booking { get; set; }
    }
}
