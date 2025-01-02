using HotelBookingDTO;
using System;
using System.Linq;
using System.Text;


namespace HotelBookingApp.Models
{
    public class RoomDTOConverter
    {
        public Room Room { get; }

        public string RoomNumber => Room.RoomNumber.ToString();
        public string RoomType => Room.RoomType.ToString();

        public string RoomAvailability => Room.IsAvailable ? "Available" : "Not Available";
       
        public bool IsAvailable => Room.IsAvailable;

        public string Price => Room.Price.ToString("F2");

        public string Description => Room.Description;

        public List<Booking> Bookings => (List<Booking>)Room.Bookings;

        public int TotalBookings => Room.Bookings.Count;

        public RoomDTOConverter(Room room)
        {
            Room = room;
        }
    }
}