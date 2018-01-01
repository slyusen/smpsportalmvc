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
    public class CaResponseViewModel
    {
        public int Id { get; set; }

        public string Questions { get; set; }

        public string Response { get; set; }

        public string Title { get; set; }

        public string StudentName { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public double Score { get; set; }

        public string Remark { get; set; }

        public string Heading { get; set; }

        public DateTime DateDue { get; set; }

        public string ActionChosen { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<AssessmentController, ActionResult>> update = (c => c.ResponseUpdate(this));
                Expression<Func<AssessmentController, ActionResult>> create = (c => c.StudResponseUpdate(this));

                var action = (ActionChosen != "Student") ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;


            }


        }
    }
}