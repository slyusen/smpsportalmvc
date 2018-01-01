using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class AcademicResultsViewModel
    {
        public int Id { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public string Heading { get; set; }

        public int ClassId { get; set; }

        public IEnumerable<SchoolClass> ClassList { get; set; }
    }
}