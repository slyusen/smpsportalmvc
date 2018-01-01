using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
    public interface IDepartmentRepository
    {
        Department GetDepartmentById(int departmentId);

        IEnumerable<Department> GetAllDepartments();

        Department GetDepartmentsByTitle(string title);

        void Add(Department department);

        void Remove(Department department);

        void Update(Department department);

    }
}