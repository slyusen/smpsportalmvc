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
    public class ExamViewModel
    {
        public int Id { get; set; }

        public string Heading { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public ExamType Type { get; set; }

        public string Title { get; set; }

        public string Duration { get; set; }

        public string Questions { get; set; }

        public string Answers { get; set; }

        public double HighestScore { get; set; }

        public DateTime DateGiven { get; set; }

        public int CourseId { get; set; }

        public IEnumerable<Course> CourseList { get; set; }

        public int[] InvigilatorId { get; set; }

        public MultiSelectList Invigilators { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ExamsController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<ExamsController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }


        }

    }
}