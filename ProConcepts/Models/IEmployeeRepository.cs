    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProConcepts.Models
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
        void AddEmployee(Employee employee);
        bool EditEmployee(Employee employee);
        bool DeleteEmployee(int id);
        List<Department> GetDepartments();
    }
}
