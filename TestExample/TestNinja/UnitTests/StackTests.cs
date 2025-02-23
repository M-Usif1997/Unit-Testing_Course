using NUnit.Framework;
using System;

using TestNinja.Fundamentals;

namespace UnitTests
{
    [TestFixture]
    public class StackTests
    {

        private Stack<string> _stack;
        [SetUp]
        public void SetUp()
        {
            // Arrange
            _stack = new Stack<string>();
        }


        /*--------------------------------------------- Push -------------------------------------------------*/
        [Test]
        public void Push_ArgsIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidArg_PushValueToStack()
        {
            _stack.Push("a");
            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_EmptyStack_ReTurnZero()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));

        }


        /*----------------------------------------------- Pop -------------------------------------------------*/
        [Test]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }
        [Test]
        public void Pop_StackWithValues_ReturnValueOTheTop()
        {
            //Arange
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            //Act
            var result = _stack.Pop();

            //Assert
            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Pop_StackWithValues_RemoveValueFromTop()
        {
            //Arange
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            //Act
            var result = _stack.Pop();

            //Assert
            Assert.That(_stack.Count, Is.EqualTo(2));
        }

        /*------------------------------------------------ Peek -------------------------------------------------*/
        [Test]
        public void Peek_EmptyStack_ThrowInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek__StackWithValues_ReturnValueOTheTop()
        {
            //Arange
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            //Act
            var result = _stack.Peek();

            //Assert
            Assert.That(result, Is.EqualTo("c"));
        }


        [Test]
        public void Peek_StackWithValues_DoesNotRemoveValueFromTop()
        {
            //Arange
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            //Act
            var result = _stack.Peek();

            //Assert
            Assert.That(_stack.Count, Is.EqualTo(3));
        }
    }
}
