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
    public class SchoolClassesViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }

        public string Heading { get; set; }

        public IEnumerable<GradeLevel> GradeLevelList { get; set; }

        public  IEnumerable<Program> ProgramList { get; set; }

        public string Code { get; set; }

        public int NumberofElectives { get; set; }

        public int Capacity { get; set; }

        public int ProgramId { get; set; }

        public int GradeLevelId { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<SchoolClassesController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<SchoolClassesController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }


        }

    }
}