using HotelBookingApp.Models;
using HotelBookingBL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Services
{
    /// <summary>
    ///  Interface for retrieving data from different entities such as rooms, payments, guests, and bookings.
    /// </summary>
    public interface IDataInitializationService
    {
        /// <summary>
        ///  Retrieves a list of rooms.
        /// </summary>
        ObservableCollection<RoomDTOConverter> Rooms { get; }

        /// <summary>
        ///  Retrieves a list of payments.
        /// </summary>
        ObservableCollection<PaymentDTOConverter> Payments { get; }

        /// <summary>
        ///  Retrieves a list of guests.
        /// </summary>
        ObservableCollection<GuestDTOConverter> Guests { get; }

        /// <summary>
        ///  Retrieves a list of bookings.
        /// </summary>
        ObservableCollection<BookingDTOConverter> Bookings { get; }


        /// <summary>
        ///   Retrieves all the necessary data asynchronously, for populating the collections of rooms, payments, guests, and bookings.
        /// </summary>
        /// <returns> A task representing the asynchronous initialization operation </returns>
        Task InitializeAsync();

        /// <summary>
        ///   Retrieves all payment data asynchronously and populates the corresponding collection.
        /// </summary>
        /// <returns> A task representing the asynchronous initialization operation </returns>
        Task GetAllPaymentsAsync();

        /// <summary>
        ///   Retrieves all room data asynchronously and populates the corresponding collection.
        /// </summary>
        /// <returns> A task representing the asynchronous initialization operation </returns>
        Task GetAllRoomsAsync();

        /// <summary>
        ///   Retrieves all guest data asynchronously and populates the corresponding collection.
        /// </summary>
        /// <returns> A task representing the asynchronous initialization operation </returns>
        Task GetAllGuestsAsync();

        /// <summary>
        ///   Retrieves all booking data asynchronously and populates the corresponding collection.
        /// </summary>
        /// <returns> A task representing the asynchronous initialization operation </returns>
        Task GetAllBookingsAsync();
    }
}
