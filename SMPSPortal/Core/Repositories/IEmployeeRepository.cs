using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Repositories
{
   public interface IEmployeeRepository
   {
       Employee GetEmployeeById(string employeeId);
       int GetLastEmployeeCount();
       IEnumerable<Employee> GetAllEmployees();
       IEnumerable<Employee> GetAllActiveEmployees();
       IEnumerable<Employee> GetAllTeachers();
       IEnumerable<Employee> GetAllConfirmedEmployees();
       IEnumerable<Employee> GetAllEmployeesByType(EmployeeType type);
       IEnumerable<Employee> GetAllConfirmedEmployeesByType(EmployeeType type);
       IEnumerable<Employee> GetAllEmployeesByDepartment(int departmentId);
       IEnumerable<Employee> GetAllEmployeesByDesignation(int designationId);
       IEnumerable<Employee> GetAllEmployeesByCategory(EmployeeCategory category);
       void Add(Employee emp);
       void Remove(Employee emp);
       string Get8Digits();
   }
}
