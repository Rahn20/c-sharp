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
    public class GuestDALTest
    {
        private HotelBookingContext _context;
        private GuestDAL _guest;
        private int _lastGuestId; // Variable to store the ID of the last created guest.

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Set environment variable for the entire test run
            Environment.SetEnvironmentVariable("HOTELBOOKINGAPP_DB_PROVIDER", "InMemory");
            _context = new HotelBookingContext();
            _guest = new GuestDAL(_context);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Environment.SetEnvironmentVariable("HOTELBOOKINGAPP_DB_PROVIDER", string.Empty);
            _context.Dispose();
        }


        [SetUp]
        public void Setup() 
        {
            var guest = new Guest
            {
                FirstName = "First name",
                LastName = "Last name",
                Email = "first.last@test.se",
                PhoneNumber = "1234567890",
            };

            _context.Guests.Add(guest);
            _context.SaveChanges();
            _lastGuestId = guest.GuestId;
        }

        [TearDown]
        public void TearDown() 
        {
            // Remove all guests from the DbSet
            _context.Guests.RemoveRange(_context.Guests);

            // Save changes to persist the deletion
            _context.SaveChanges();
            //_context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Guests]");
        }


        [Test]
        public async Task GetAllAsync_OneGuest_ReturnsAllGuests()
        {
            var actual = await _guest.GetAllAsync();
            Assert.That(actual.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task GetByIdAsync_ValidId_ReturnsOneGuest()
        {
            var actual = await _guest.GetByIdAsync(_lastGuestId);

            Assert.Multiple(() =>
            {
                Assert.That(actual.FirstName, Is.EqualTo("First name"));
                Assert.That(actual.Email, Is.EqualTo("first.last@test.se"));
            });
        }


        [Test]
        [TestCase(0), TestCase(-1), TestCase(1000)]
        public void GetByIdAsync_InvalidId_ShouldThrowException(int guestId)
        {
            Assert.ThrowsAsync<Exception>(async () => await _guest.GetByIdAsync(guestId));
        }


        [Test]
        public async Task AddAsync_OneGuest_Success()
        {
            await _guest.AddAsync(new Guest
            {
                FirstName = "Second name",
                LastName = "Second name",
                Email = "Second.last@test.se",
                PhoneNumber = "0888999777",
            });

            Assert.That(_context.Guests.Count(), Is.EqualTo(2));
        }


        [Test, TestCase("name"), TestCase("First"), TestCase("ast")]
        public async Task FindAsync_OneGuest_ReturnsOneRow(string searchTerm)
        {
            var actual = await _guest.FindAsync(searchTerm);

            Assert.That(actual.Count(), Is.EqualTo(1));
        }


        [Test, TestCase("Name"), TestCase("FIR"), TestCase("LAST")]
        public async Task FindAsync_ZeroGuest_ReturnsZeroRow(string searchTerm)
        {
            var actual = await _guest.FindAsync(searchTerm);

            Assert.That(actual.Count(), Is.EqualTo(0));
        }


        [Test]
        public async Task UpdateAsync_OneGuest_GuestUpdated()
        {
            await _guest.UpdateAsync(new Guest
            {
                GuestId = _lastGuestId,
                FirstName = "Updated first",
                LastName = "Updated last",
                PhoneNumber = "0888999777",
            });

            Assert.Multiple(() =>
            {
                Assert.That(_context.Guests.Last().FirstName, Is.EqualTo("Updated first"));
                Assert.That(_context.Guests.Last().LastName, Is.EqualTo("Updated last"));
            });
        }
    }
}
