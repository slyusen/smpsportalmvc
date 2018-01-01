using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class TeacherCourseViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }

        public string Heading { get; set; }

        public IEnumerable<TeacherList> Teachers { get; set; }

        public IEnumerable<Course> AllCourses { get; set; }

        public IEnumerable<TeacherCourseList> AssignedCourses { get; set; }
    }
}