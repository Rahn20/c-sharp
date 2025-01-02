using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using HotelBookingDAL;
using HotelBookingDTO;
using HotelBookingDAL.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingDALTest
{
    [TestFixture]
    public class BookingDALTest
    {
        private HotelBookingContext _context;
        private BookingDAL _booking;
        private int _lastBookingId; // Variable to store the ID of the last created booking.

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Set environment variable for the entire test run
            Environment.SetEnvironmentVariable("HOTELBOOKINGAPP_DB_PROVIDER", "InMemory");
            _context = new HotelBookingContext();
            _booking = new BookingDAL(_context);

            var guest = new Guest { GuestId = 1, FirstName = "First", LastName = "Last", Email = "first.last@test.se", PhoneNumber = "1234567890" };
            var room = new Room { RoomId = 1, RoomNumber = 111, IsAvailable = true, Price = 15m, RoomType = RoomType.Single };

            _context.Guests.Add(guest);
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Environment.SetEnvironmentVariable("HOTELBOOKINGAPP_DB_PROVIDER", string.Empty);
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


        [SetUp]
        public void Setup()
        {
            var booking = new Booking
            {
                GuestId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(5),
                TotalAmount = 10m,
                IsPaid = false,
                RoomId = 1
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();
            _lastBookingId = booking.BookingId;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Bookings.RemoveRange(_context.Bookings);
            _context.SaveChanges();
        }


        [Test]
        public async Task GetAllAsync_OneBooking_ReturnsBookings()
        {
            // Act
            var actual = await _booking.GetAllAsync();

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task AddAsync_BookingWithoutPayment_Success()
        {
            await _booking.AddAsync(new Booking
            {
                GuestId = 1,
                CheckInDate = DateTime.Now.AddDays(7),
                CheckOutDate = DateTime.Now.AddDays(10),
                TotalAmount = 60m,
                IsPaid = false,
                RoomId = 1
            });

            Assert.Multiple(() =>
            {
                Assert.That(_context.Bookings.Count(), Is.EqualTo(2));
                Assert.That(_context.Bookings.Last().TotalAmount, Is.EqualTo(60m));
            });
        }

        [Test]
        public async Task AddAsync_BookingWithPayment_Success()
        {
            // Arrange
            var payment = new Payment { PaymentDate = DateTime.Now, Amount = 80m, GuestId = 1, PaymentMethod = PaymentMethod.Cash};

            // Act
            await _booking.AddAsync(new Booking
            {
                GuestId = 1,
                CheckInDate = DateTime.Now.AddDays(7),
                CheckOutDate = DateTime.Now.AddDays(10),
                TotalAmount = 80m,
                IsPaid = true,
                RoomId = 1,
            }, payment);

            var lastBooking = await _context.Bookings.OrderBy(b => b.BookingId).LastOrDefaultAsync(); // Asynchronous Last

            Assert.Multiple(() =>
            {
                Assert.That(_context.Bookings.Count(), Is.EqualTo(2));
                Assert.That(lastBooking.TotalAmount, Is.EqualTo(80m));
                Assert.That(lastBooking.IsPaid, Is.True);
                Assert.That(lastBooking.Payment, Is.EqualTo(payment));
            });
        }


        [Test]
        public async Task UpdateAsync_BookingWithoutPayment_BookingUpdated()
        {
            await _booking.UpdateAsync(new Booking
            {
                BookingId = _lastBookingId,
                GuestId = 1,
                CheckInDate = DateTime.Now.AddDays(10),
                CheckOutDate = DateTime.Now.AddDays(20),
                TotalAmount = 50m,
                IsPaid = false,
                RoomId = 1
            });

            Assert.Multiple(() =>
            {
                Assert.That(_context.Bookings.Last().IsPaid, Is.False);
                Assert.That(_context.Bookings.Last().Payment, Is.Null);
                Assert.That(_context.Bookings.Last().TotalAmount, Is.EqualTo(50m));
            });
        }
    }
}
