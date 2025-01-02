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
    public class PaymentDALTest
    {
        private HotelBookingContext _context;
        private PaymentDAL _payment;
        private int _lastPaymentId; // Variable to store the ID of the last created payment.



        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Set environment variable for the entire test run
            Environment.SetEnvironmentVariable("HOTELBOOKINGAPP_DB_PROVIDER", "InMemory");
            _context = new HotelBookingContext();
            _payment = new PaymentDAL(_context);


            var guest = new Guest { GuestId = 1, FirstName = "First", LastName = "Last", Email = "first.last@test.se", PhoneNumber = "1234567890" };
            var room = new Room { RoomId = 1, RoomNumber = 111, IsAvailable = true, Price = 15m, RoomType = RoomType.Single };
            var booking = new Booking
            {
                BookingId = 1,
                GuestId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(5),
                TotalAmount = 10m,
                IsPaid = false,
                RoomId = 1
            };

            _context.Guests.Add(guest);
            _context.Rooms.Add(room);
            _context.Bookings.Add(booking);
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
            var payment = new Payment
            {
                BookingId = 1,
                PaymentDate = DateTime.Now,
                Amount = 10m,
                GuestId = 1,
                PaymentMethod = PaymentMethod.Cash,
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();
            _lastPaymentId = payment.PaymentId;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Payments.RemoveRange(_context.Payments);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllAsync_OnePayment_ReturnsPayments()
        {
            var actual = await _payment.GetAllAsync();
            Assert.That(actual.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task GetByIdAsync_ValidId_ReturnsOnePayment()
        {
            var actual = await _payment.GetByIdAsync(_lastPaymentId);

            Assert.Multiple(() =>
            {
                Assert.That(actual.PaymentMethod, Is.EqualTo(PaymentMethod.Cash));
                Assert.That(actual.Amount, Is.EqualTo(10m));
                Assert.That(actual.BookingId, Is.EqualTo(1));
                Assert.That(actual.GuestId, Is.EqualTo(1));
            });
        }


        [Test, TestCase("111"), TestCase("First"), TestCase("st")]
        public async Task FindAsync_OnePayment_ReturnsOneRow(string searchTerm)
        {
            var actual = await _payment.FindAsync(searchTerm);

            Assert.That(actual.Count(), Is.EqualTo(1));
        }


        [Test, TestCase("nan"), TestCase("FIR"), TestCase("LAST"), TestCase("234")]
        public async Task FindAsync_ZeroGuest_ReturnsZeroRow(string searchTerm)
        {
            var actual = await _payment.FindAsync(searchTerm);

            Assert.That(actual.Count(), Is.EqualTo(0));
        }
    }
}
