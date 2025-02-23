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
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _employeeStorage;

        [SetUp]
        public void SetUp()
        {
            _employeeStorage = new Mock<IEmployeeStorage>();
        }
        [Test]

        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromTheDb()
        {
            var controller = new EmployeeController(_employeeStorage.Object);
            controller.DeleteEmployeeById(1);

            _employeeStorage.Verify(s => s.RemoveEmployeeById(1));
          
        }
    }
}
