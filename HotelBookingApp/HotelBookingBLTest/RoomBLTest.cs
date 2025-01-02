using HotelBookingBL;
using HotelBookingDAL;
using HotelBookingDTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingBLTest
{

    [TestClass]
    public class RoomBLTest
    {
        private Mock<IRepository<Room>> _roomDALMock;
        private RoomBL _room;


        [TestInitialize]
        public void Setup()
        {
            _roomDALMock = new Mock<IRepository<Room>>();
            _room = new RoomBL(_roomDALMock.Object);
        }


        [TestMethod, DataRow(111, true), DataRow(123, false)]
        public async Task IsRoomNumberValid_Check_ReturnsBool(int roomNr, bool expectedResult)
        {
            var data = new List<Room>()
            {
                new Room { RoomNumber = 222 }, new Room { RoomNumber = 123 },
            };

            _roomDALMock.Setup(dal => dal.GetAllAsync()).ReturnsAsync(data);
            var actual = await _room.IsRoomNumberValid(roomNr);
            Assert.AreEqual(actual, expectedResult);
        }
    }
}
