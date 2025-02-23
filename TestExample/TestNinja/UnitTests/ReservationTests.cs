
using NUnit.Framework;
using TestNinja.Fundamentals;


namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();
            var user = new User { IsAdmin = true };
            // Act
            var result = reservation.CanBeCancelledBy(user);
            // Assert
            Assert.That(result== true);

        }


        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            // Arrange
            var user = new User();
            var reservation = new Reservation { MadeBy = user };
            // Act
            var result = reservation.CanBeCancelledBy(user);
            // Assert
            Assert.That(result == true);
        }


        [Test]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            // Arrange
            var reservation = new Reservation();
            var user = new User();
            // Act
            var result = reservation.CanBeCancelledBy(user);
            // Assert
            Assert.That(result == false);

        }
    }
}
