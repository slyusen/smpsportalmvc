using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Controllers.Administration;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class ParentViewModel : RegisterViewModel
    {
        public string Id { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public string Heading { get; set; }

        public override string  Occupation { get; set; }

        public override string BusinessAddress { get; set; }

        public string Relationship { get; set; }

        public IEnumerable<SelectListItem> RelationshipList { get; set; }

        public string StudentId { get; set; }

        public string Message { get; set; }

        public IEnumerable<GradeLevel> GradeLevelList { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ParentController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<ParentController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != null) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }


        }
    }
}