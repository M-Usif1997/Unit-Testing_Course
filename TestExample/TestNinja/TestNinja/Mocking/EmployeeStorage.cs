using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IEmployeeStorage
    {
        void RemoveEmployeeById(int id);
    }

    public class EmployeeStorage : IEmployeeStorage
    {
        private EmployeeContext _employeeContext;

        public EmployeeStorage()
        {
            _employeeContext = new EmployeeContext();
        }

        public void RemoveEmployeeById(int id)
        {
            var employee = _employeeContext.Employees.Find(id);
            if (employee == null) return;

              _employeeContext.Employees.Remove(employee);
                _employeeContext.SaveChanges();
            

        }


    }
}
