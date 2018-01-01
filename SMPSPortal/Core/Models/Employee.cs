using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc.Html;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class Employee : ApplicationUser
    {
        [StringLength(20)]
        public string EmployeeNumber { get; set; }

        
        [Display(Name = "Date Employed")]
        
        public DateTime DateEmployed { get; set; }

       
        [Display(Name = "Date Confirmed")]
        
        public DateTime DateConfirmed { get; set; }

        public bool IsConfirmed { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public EmployeeCategory EmployeeCategory { get; set; }

        public  int? DepartmentId { get; set; }


        public  int? DesignationId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Designation Designation { get; set; }

        public virtual ICollection<TeacherCourse> Courses { get; private set; }

        public Employee(RegisterViewModel model)
        {
            
            EmployeeNumber = "EMP"  + DateEmployed.Year + Get8Digits();
            Courses = new Collection<TeacherCourse>();

            DateEmployed = model.DateEmployed;
            DateConfirmed = model.DateConfirmed;
            IsConfirmed = model.IsConfirmed;
            EmployeeType = model.EmployeeType;
            EmployeeCategory = model.EmployeeCategory;
            DepartmentId = model.DepartmentId;
            DesignationId = model.DesignationId;
        }
       
        public Employee()
        {
           
        }

        
    }
}