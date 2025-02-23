using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace UnitTests
{

    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        [TestCase(30)]
        [TestCase(15)]
        public void GetOutput_InputIsDivisibleBy3And5_ReturnFizzBuzz(int number)
        {

            // Act
            var result = FizzBuzz.GetOutput(number);
            // Assert
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }


        [Test]
        [TestCase(18)]
        public void GetOutput_InputIsDivisibleBy3_ReturnFizz(int number)
        {

            // Act
            var result = FizzBuzz.GetOutput(number);
            // Assert
            Assert.That(result, Is.EqualTo("Fizz"));
        }



        [Test]
        [TestCase(5)]
        [TestCase(20)]
        public void GetOutput_InputIsDivisibleBy5_ReturnFizz(int number)
        {

            // Act
            var result = FizzBuzz.GetOutput(number);
            // Assert
            Assert.That(result, Is.EqualTo("Buzz"));
        }



        [Test]
        [TestCase(2)]
        [TestCase(4)]
        public void GetOutput_InputIsNotDivisibleBy3or5_ReturnTheSameNumber(int number)
        {

            // Act
            var result = FizzBuzz.GetOutput(number);
            // Assert
            Assert.That(result, Is.EqualTo($"{number}"));
        }



    }
}
