using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.Models
{
    public class CourseRegister
    {
        public int Id { get; set; }

        public Student Student { get; set; }

        public string StudentId { get; set; }

        public Course Course { get; set; }

        public int CourseId { get; set; }

        public DateTime DateRegistered { get; private set; }

        public SchoolClass SchoolClass { get; set; }

        public int SchoolClassId { get; set; }

        public YearTerm YearTerm { get; set; }

        public int YearTermId { get; set; }

        public CourseRegister()
        {
            DateRegistered = DateTime.Today;
        }
    }
}