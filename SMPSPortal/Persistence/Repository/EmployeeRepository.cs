using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;
using SmpsPortal.Core.ViewModels;
using Syncfusion.XlsIO;

namespace SmpsPortal.Persistence.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private IApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Employee GetEmployeeById(string employeeId)
        {
            return _context.Employees.SingleOrDefault(
                e =>
                e.Id == employeeId);
        }

        public string Get8Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D8}", random);
        }

        public int GetLastEmployeeCount()
        {
           
            var emp = _context.Employees.ToList();

            if (emp.Count == 0)
                return 0;
            else
            {
               
                return emp.Count + 1;
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            
            return _context.Employees
                .ToList();
        }
        public IEnumerable<Employee> GetAllActiveEmployees()
        {
            
            return _context.Employees.Where(
                e =>
                e.IsActive)
                .ToList();
        }
       
       
        public IEnumerable<Employee> GetAllTeachers()
        {
            return _context.Employees.Where(
                e =>
                e.EmployeeCategory == EmployeeCategory.Teaching)
                .ToList();
        }

        public IEnumerable<Employee> GetAllConfirmedEmployees()
        {
            
            return _context.Employees.Where(
                e =>
                e.IsConfirmed)
                .ToList();
        }

       

        public IEnumerable<Employee> GetAllEmployeesByType(EmployeeType type)
        {
           
            return _context.Employees.Where(
                e =>
                e.EmployeeType == type)
                .ToList();
        }

        public IEnumerable<Employee> GetAllConfirmedEmployeesByType(EmployeeType type)
        {
           
            return _context.Employees.Where(
                e =>
                e.EmployeeType == type &&
                e.IsConfirmed)
                .ToList();
        }

       
        
        public IEnumerable<Employee> GetAllEmployeesByDepartment(int departmentId)
        {
            return _context.Employees.Where(
                e =>
                e.DepartmentId == departmentId)
                .ToList();
        }
       
        public IEnumerable<Employee> GetAllEmployeesByDesignation(int designationId)
        {
            
            return _context.Employees.Where(
                e =>
                e.DesignationId == designationId)
                .ToList();
        }
       

        public IEnumerable<Employee> GetAllEmployeesByCategory(EmployeeCategory category)
        {
            
            return _context.Employees.Where(
                e =>
                e.EmployeeCategory == category)
                .ToList();
        }
        
        public void Add(Employee emp)
        {
            _context.Employees.Add(emp);
        }

        public void Remove(Employee emp)
        {
            _context.Employees.Remove(emp);
        }
    }
}