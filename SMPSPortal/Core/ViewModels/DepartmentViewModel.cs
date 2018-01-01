using System.Collections.Generic;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class DepartmentViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }

        public IEnumerable<Department> Departments { get; set; }

        public string Heading { get; set; }
    }
}