using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Controllers.Academics;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class AssessmentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double PercentofCa { get; set; }

        public DateTime DateGiven { get; set; }

        public DateTime DateDue { get; set; }

        public int CourseId { get; set; }

        public IEnumerable<Course> CourseList { get; set; }

        public string Questions { get; set; }

        public string Answers { get; set; }

        public bool IsChosen { get; set; }

        public string Type { get; set; }

        public IEnumerable<SelectListItem> TypeListItems { get; set; }

        public double HighestScore { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public int StudentId { get; set; }

        public IEnumerable<StudentList> StudentList { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<AssessmentController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<AssessmentController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;


            }


        }
    }
}