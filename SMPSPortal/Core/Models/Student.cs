using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class Student : ApplicationUser
    {

        [StringLength(20)]
        public string StudentNumber { get; set; }

        
        [Display(Name = "Date Enrolled")]
        public DateTime DateEnrolled { get; set; }

        public int SchoolClassId { get; set; }

        public SchoolClass SchoolClass { get; set; }

        public  int GradeLevelId { get; set; }

        public GradeLevel GradeLevel { get; set; }

        public virtual ICollection<StudentParent> Parents { get; private set; }

        public virtual ICollection<CourseRegister> Courses { get; private set; }

        public virtual ICollection<ExamResult> ExamResults { get; private set; }


        public Student()
        {
            Parents = new Collection<StudentParent>();

            Courses = new Collection<CourseRegister>();

            ExamResults = new Collection<ExamResult>();
        }

        public Student(RegisterViewModel model)
        {
            StudentNumber = "STD"  + DateEnrolled.Year +  Get8Digits();
            DateEnrolled = model.DateEnrolled;
            SchoolClassId = model.SchoolClassId;
            GradeLevelId = model.GradeLevelId;
        }
    }

    
}