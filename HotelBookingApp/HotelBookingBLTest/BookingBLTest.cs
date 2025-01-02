using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using HotelBookingBL;
using HotelBookingDTO;
using HotelBookingDAL;

namespace HotelBookingBLTest
{
    [TestClass]
    public class BookingBLTest
    {
        private Mock<IRepository<Booking>> _bookingDALMock;
        private Mock<IRepository<Payment>> _paymentDALMock;
        private BookingBL _booking;


        [TestInitialize]
        public void Setup()
        {
            // Create a mock for the IRepository
            _bookingDALMock = new Mock<IRepository<Booking>>();
            _paymentDALMock = new Mock<IRepository<Payment>>();

            _booking = new BookingBL(_bookingDALMock.Object, _paymentDALMock.Object);
        }


        [TestMethod, DataRow(10, 0, 10), DataRow(20, 0, 0), DataRow(25, 5, 0)]
        public void CalculateBookingNewPrice_Check_Success(int paymentAmount, int receive, int pay)
        {
            var payment = new Payment() { PaymentId = 1, Amount = paymentAmount};

            var (actualReceive, actualPay) = BookingBL.CalculateBookingNewPrice(totalAmount: 20m, payment);

            Assert.AreEqual(actualReceive, receive);
            Assert.AreEqual(actualPay, pay);
        }



        // 0 indicates to NOW, 1 indicates to Tomorrow and -1 Yesterday.
        [TestMethod, DataRow(0, 3), DataRow(-3, 1), DataRow(2, 6)]
        public async Task IsBookingDateValid_AlreadyBooked_ReturnsFalse(int daysOffsetCheckIn, int daysOffsetCheckOut)
        {
            var data = new List<Booking>()
            {
                new() { BookingId = 1, RoomId = 1, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(3) },
            };

            var booking = new Booking
            {
                RoomId = 1,
                BookingId = 2,
                CheckOutDate = DateTime.Now.AddDays(daysOffsetCheckOut),
                CheckInDate = DateTime.Now.AddDays(daysOffsetCheckIn)
            };

            _bookingDALMock.Setup(dal => dal.GetAllAsync()).ReturnsAsync(data);
            var actual = await _booking.IsBookingDateValid(booking);
            
            Assert.IsFalse(actual);
        }


        [TestMethod, DataRow(-6, -1), DataRow(3, 9), DataRow(6, 12)]
        public async Task IsBookingDateValid_NotBooked_ReturnsTrue(int daysOffsetCheckIn, int daysOffsetCheckOut)
        {
            var data = new List<Booking>()
            {
                new() { BookingId = 1, RoomId = 1, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(3) },
            };

            var booking = new Booking
            {
                RoomId = 1,
                BookingId = 2,
                CheckOutDate = DateTime.Now.AddDays(daysOffsetCheckOut),
                CheckInDate = DateTime.Now.AddDays(daysOffsetCheckIn)
            };

            _bookingDALMock.Setup(dal => dal.GetAllAsync()).ReturnsAsync(data);
            var actual = await _booking.IsBookingDateValid(booking);

            Assert.IsTrue(actual);
        }
    }
}
