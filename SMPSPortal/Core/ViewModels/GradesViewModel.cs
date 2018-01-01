using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class GradesViewModel
    {
        public int Id { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public string Heading { get; set; }

        public int Position { get; set; }

        public int ClassId { get; set; }

        public IEnumerable<SchoolClass> ClassList { get; set; }

        public int StudentId { get; set; }

        public IEnumerable<StudentList> StudentList { get; set; }

        public string UserRole { get; set; }

        public int YearTermId { get; set; }

        public IEnumerable<YearTermList> YearTermList { get; set; }
    }
}