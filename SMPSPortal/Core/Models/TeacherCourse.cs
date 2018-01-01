using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmpsPortal.Core.Models
{
    public class TeacherCourse
    {
        public int Id { get; set; }


        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        public  int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public DateTime DateRegistered { get; set; }

        public int YearTermId { get; set; }

        [ForeignKey("YearTermId")]
        public virtual YearTerm YearTerm { get; set; }
    }
}