using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using SmpsPortal.Controllers.Administration;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class DesignationViewModel
    {


        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public IEnumerable<Department> DepartmentList { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<DesignationsController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<DesignationsController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }


        }
    }
}