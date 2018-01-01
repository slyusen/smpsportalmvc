using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Controllers.Academics;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(6)]
        public string Code { get; set; }

        public CourseCategory Category { get; set; }

        public IEnumerable<Program> ProgramList { get; set; }

        public int ProgramId { get; set; }

        public IEnumerable<GradeLevel> GradeLevelList { get; set; }

        public int GradeLevelId { get; set; }

        public IEnumerable<SchoolClass> SchoolClassList { get; set; }

        public int SchoolClasId { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public string Heading { get; set; }

        public string UserType { get; set; }

        public int RegCourseId { get; set; }

        public IEnumerable<Course> RegCourseList { get; set; }

        public string Message { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<CoursesController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<CoursesController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }


        }

        public string Reg
        {
            get
            {
                Expression<Func<CoursesController, ActionResult>> register = (c => c.RegisterElectives(this));

                var action = register;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}