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

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(x=>x.Id==id);
        }


    }

}