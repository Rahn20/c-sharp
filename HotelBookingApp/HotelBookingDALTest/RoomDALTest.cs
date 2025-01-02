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
    public class RoomDALTest
    {
        private HotelBookingContext _context;
        private RoomDAL _room;
        private int _lastRoomId; // Variable to store the ID of the last created room.


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Set environment variable for the entire test run
            Environment.SetEnvironmentVariable("HOTELBOOKINGAPP_DB_PROVIDER", "InMemory");
            _context = new HotelBookingContext();
            _room = new RoomDAL(_context);
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
            var room = new Room
            {
                RoomNumber = 111,
                IsAvailable = true,
                Price = 15m,
                RoomType = RoomType.Single,
            };

            _context.Rooms.Add(room);
            _context.SaveChanges();
            _lastRoomId = room.RoomId;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Rooms.RemoveRange(_context.Rooms);
            _context.SaveChanges();
        }


        [Test]
        public async Task GetAllAsync_OneRoom_ReturnsRooms()
        {
            // Act
            var actual = await _room.GetAllAsync();

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task GetByIdAsync_ValidId_ReturnsOneRoom()
        {
            var actual = await _room.GetByIdAsync(_lastRoomId);

            Assert.Multiple(() =>
            {
                Assert.That(actual.RoomNumber, Is.EqualTo(111));
                Assert.That(actual.Price, Is.EqualTo(15m));
                Assert.That(actual.IsAvailable, Is.True);
            });
        }


        [Test]
        [TestCase(0), TestCase(-1), TestCase(1000)]
        public void GetByIdAsync_InvalidId_ShouldThrowException(int roomId)
        {
            Assert.ThrowsAsync<Exception>(async () => await _room.GetByIdAsync(roomId));
        }


        [Test]
        public async Task AddAsync_OneRoom_Success()
        {
            await _room.AddAsync(new Room
            {
                RoomNumber = 222,
                IsAvailable = false,
                Price = 20m,
                RoomType = RoomType.Double,
            });

            Assert.That(_context.Rooms.Count(), Is.EqualTo(2));
        }


        [Test]
        public void AddAsync_RoomAlreadyExisted_Fail()
        {
            var newRoom = new Room
            {
                RoomNumber = 111,   // room number. already in the dataabse
                IsAvailable = true,
                Price = 20m,
                RoomType = RoomType.Double,
            };

            Assert.Multiple(() =>
            {
                var excep = Assert.ThrowsAsync<Exception>(async () => await _room.AddAsync(newRoom));
                Assert.That(_context.Rooms.Count(), Is.EqualTo(1));
                Assert.That(excep.Message, Is.EqualTo("The room number already esists in the database"));
            });
        }


        [Test]
        public async Task DeleteAsync_OneRoom_RoomDeleted()
        {
            // Retrieves the last room using tracking
            var roomToDelete = _context.Rooms.OrderBy(r => r.RoomId).Last();

            await _room.DeleteAsync(roomToDelete);
            Assert.That(_context.Rooms.Count(), Is.EqualTo(0));
        }
    }
}
