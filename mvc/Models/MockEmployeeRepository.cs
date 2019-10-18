using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace testing1.Models
{

    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id=1, Name="marry", Department=Dept.HR,Email="fafa@gmail.com"},
                   new Employee(){Id=2, Name="fafa", Department=Dept.IT,Email="fa2afa@gmail.com"}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id= _employeeList.Max(x=>x.Id)+1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
           Employee employee= _employeeList.FirstOrDefault(x=>x.Id == id);
           if(employee != null)
           {
               _employeeList.Remove(employee);
           }
           return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(x=>x.Id==id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(x => x.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }

}