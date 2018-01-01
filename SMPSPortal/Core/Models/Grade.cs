using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.Models
{
    public class Grade
    {
        public int Id { get; set; }

        public double ExamScore { get; set; }

        public double TotalCAScore { get; set; }

        public string StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int SchoolClassId { get; set; }

        [ForeignKey("SchoolClassId")]
        public SchoolClass SchoolClass { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public int YearTermId { get; set; }

        [ForeignKey("YearTermId")]
        public YearTerm YearTerm { get; set; }

        public int GradeSystemId { get; set; }

        [ForeignKey("GradeSystemId")]
        public GradeSystem GradeSystem { get; set; }

        
    }
}