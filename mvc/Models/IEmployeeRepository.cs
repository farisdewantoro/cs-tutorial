using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace testing1.Models
{

    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        
        IEnumerable<Employee> GetAllEmployee();
        Employee Add(Employee employee);
        Employee Update(Employee employeeChanges);
        Employee Delete(int id);
    } 
    
}