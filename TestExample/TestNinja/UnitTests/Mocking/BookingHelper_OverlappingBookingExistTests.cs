using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace UnitTests.Mocking
{


    [TestFixture]
    public class BookingHelper_OverlappingBookingExistTests
    {

        private Mock<IBookingRepository> _bookingRepository;
        private Booking _bookingObject;

        [SetUp]
        public void SetUp()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _bookingObject = new Booking
            {
                Id = 2,
                ArrivalDate = DateTime.Now.AddDays(-1),
                DepartureDate = DateTime.Now.AddDays(2),
                Reference = "a"
            };

            //arrange in Database
            _bookingRepository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
              _bookingObject
            }.AsQueryable());
        }
        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnsEmptyString()
        {
          

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = DateTime.Now.AddDays(-2),
                DepartureDate = DateTime.Now.AddDays(6),
            }, _bookingRepository.Object);

            Assert.That(result, Is.Empty);

        }

        [Test]
        public void BookingStartsBeforeAnFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBookingReference()
        {


            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = DateTime.Now.AddDays(-2),
                DepartureDate = DateTime.Now.AddDays(1),
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_bookingObject.Reference));

        }
    }
}
