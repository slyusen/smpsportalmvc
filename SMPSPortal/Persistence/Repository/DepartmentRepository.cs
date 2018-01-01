using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    { 

      IApplicationDbContext _context;
    

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
       
    }

    public Department GetDepartmentById(int departmentId)
    {
        return _context.Departments.SingleOrDefault(d => d.Id == departmentId);
    }

    public IEnumerable<Department> GetAllDepartments()
    {
        return _context.Departments.ToList();
    }

    public Department GetDepartmentsByTitle(string title)
    {
        return _context.Departments.SingleOrDefault(d => d.Title == title);
    }
   
    public void Add(Department department)
    {
        _context.Departments.Add(department);
    }
    public void Update(Department department)
    {
        var dept = _context.Departments.SingleOrDefault(p => p.Id == department.Id);
        dept.Abbreviation = department.Abbreviation;
        dept.Title = department.Title;
    }

    public void Remove(Department department)
    {
        _context.Departments.Remove(department);
    }
}
}