using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using SmpsPortal.Controllers.HR;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class EmployeeViewModel : RegisterViewModel
    {
       
        public string Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Employed")]
        public override DateTime DateEmployed { get; set; }

        [Display(Name = "Employee Category")]
        public override EmployeeCategory EmployeeCategory { get; set; }

        [Display(Name = "Employee Type")]
        public override EmployeeType EmployeeType { get; set; }

        
        public override int DepartmentId { get; set; }

        public IEnumerable<Department> DepartmentList { get; set; }

        
        public override int DesignationId { get; set; }

        public IEnumerable<Designation> DesignationList { get; set; }

        public override bool IsConfirmed { get; set; }

        public override DateTime DateConfirmed { get; set; }

       

        public IEnumerable<Menu> Menus { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<EmployeeController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<EmployeeController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != null) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }


        }
    }
}