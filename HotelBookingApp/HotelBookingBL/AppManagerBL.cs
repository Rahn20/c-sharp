using HotelBookingDAL;
using HotelBookingDAL.DbContexts;
using System;
using System.Linq;
using System.Text;

namespace HotelBookingBL
{
    /// <summary>
    ///  Business Logic Layer (BL) for managing the overall application operations. It acts as a manager that handles 
    ///  different aspects of the hotel booking system by providing access to other BLL for guests, payments, rooms, and bookings.
    /// </summary>
    public class AppManagerBL
    {
        public GuestBL GuestBL { get; }
        public PaymentBL PaymentBL { get; }
        public RoomBL RoomBL { get; }
        public BookingBL BookingBL { get; }


        /// <summary>
        ///   Initializes a new instance of the AppManagerBL class. It creates and wires up all the business logic layers 
        ///   and data access layers (DAL) using a shared DbContext instance.
        /// </summary>
        public AppManagerBL() 
        {
            // a single DbContext instance for related operations within a unit of work.
            HotelBookingContext hotelBookingContext = new HotelBookingContext();

            // Initialize the data access layers (DAL) with the shared context.
            GuestDAL guestDAL = new GuestDAL(hotelBookingContext);
            RoomDAL roomDAL = new RoomDAL(hotelBookingContext);
            PaymentDAL paymentDAL = new PaymentDAL(hotelBookingContext);
            BookingDAL bookingDAL = new BookingDAL(hotelBookingContext);

            // Initialize the business logic layers (BL) with the corresponding DAL.
            GuestBL = new GuestBL(guestDAL);
            PaymentBL = new PaymentBL(paymentDAL);
            RoomBL = new RoomBL(roomDAL);
            BookingBL = new BookingBL(bookingDAL, paymentDAL);
        }
    }
}
