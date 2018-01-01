using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using SmpsPortal.Controllers.Administration;
using SmpsPortal.Controllers.HR;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class StudentViewModel : RegisterViewModel
    {
        public  string Id { get; set; }
        
        [Display(Name = "Date Enrolled")]
        public override DateTime DateEnrolled { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public string Heading { get; set; }

        public override int SchoolClassId { get; set; }

        public override int GradeLevelId { get; set; }

        public IEnumerable<GradeLevel> GradeLevelList { get; set; }

        public IEnumerable<SchoolClass> SchoolClassList { get; set; }

        public string BloodGroup { get; set; }

        public string Genotype { get; set; }

        public string PhysicianName { get; set; }

        public string PhysicianPhone { get; set; }

        public string Hmo { get; set; }

        public string HmoNumber { get; set; }

        public string Allergies { get; set; }

        public string Medications { get; set; }

        public string BriefMedicalHistory { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<StudentsController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<StudentsController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != null) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }


        }
    }
}