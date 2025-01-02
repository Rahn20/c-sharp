using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using HotelBookingBL;
using HotelBookingDTO;
using HotelBookingDAL;

namespace HotelBookingBLTest
{
    [TestClass]
    public class GuestBLTest
    {
        private Mock<IRepository<Guest>> _guestDALMock;
        private GuestBL _guest;


        [TestInitialize]
        public void Setup()
        {
            // Create a mock for the IRepository
            _guestDALMock = new Mock<IRepository<Guest>>();

            // Inject the mock into the GuestBL
            _guest = new GuestBL(_guestDALMock.Object);
        }


        // This is the parameterized test method
        [TestMethod, DataRow("user@example"), DataRow("user_example12@"), DataRow("@example.com"), DataRow("test.example@sub.ok.com")]
        public async Task IsEmailValid_Invalid_ReturnsFalse(string email)
        {
            // Arrange
            _guestDALMock.Setup(dal => dal.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Guest>());

            // Act
            var actual = await _guest.IsEmailValid(email);

            Assert.IsFalse(actual);
        }


        [TestMethod, DataRow("user@example.com"), DataRow("user_test@example.se")]
        public async Task IsEmailValid_EmailAlreadyExists_ReturnsFalse(string email)
        {
            var data = new List<Guest>() 
            { 
                new() { Email = "user@example.com" }, new Guest { Email = "user_test@example.se" },
            };

            _guestDALMock.Setup(dal => dal.GetAllAsync()).ReturnsAsync(data);
            Assert.IsFalse(await _guest.IsEmailValid(email));
        }


        [TestMethod, DataRow("user12@example.com"), DataRow("user.test@example.se")]
        public async Task IsEmailValid_Valid_ReturnsTrue(string email)
        {
            var data = new List<Guest>()
            {
                new() { Email = "user@example.com" }, new Guest { Email = "user_test@example.se" },
            };

            _guestDALMock.Setup(dal => dal.GetAllAsync()).ReturnsAsync(data);
            Assert.IsTrue(await _guest.IsEmailValid(email));
        }



        [TestMethod, DataRow("7777777777", false), DataRow("012345", false), DataRow("0123456789", true), DataRow("0111111111", true)]
        public void IsPhoneNumberValid_ReturnsBool(string phoneNr, bool expectedResult)
        {
            var actual = GuestBL.IsPhoneNumberValid(phoneNr);
            Assert.AreEqual(actual, expectedResult);
        }
    }
}
