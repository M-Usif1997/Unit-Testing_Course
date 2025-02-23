using NUnit.Framework;
using TestNinja.Fundamentals;


namespace UnitTests
{

    [TestFixture]
    public class MathTests
    {

        private Math _math;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _math = new Math();
        }
        [Test]
        [Ignore ("Not needed now")]
        public void Add_WhenCalled_ReturnsTheSumOfArguments()
        {

            // Act
            var result = _math.Add(1, 2);
            // Assert
            Assert.That(result == 3);
        }
        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {

            // Act
            var result = _math.Max(a, b);
            // Assert
            Assert.That(result == expectedResult);
        }


        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnsOddNumbersUpToLimit()
        {

            // Act
            var result = _math.GetOddNumbers(5);
            // Assert
            Assert.That(result, Is.EquivalentTo(new[] { 5,3,1 }));

            Assert.That(result,Is.Ordered);
            Assert.That(result, Is.Unique);
        }


    }
}
