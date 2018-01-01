using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class Course
    {
        public int Id { get; set; }

        [StringLength(40)]
        public string Title { get; set; }

        public string Code { get; set; }

        public CourseCategory Category { get; set; }

        public int ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public Program Program { get; set; }

        public int? GradeLevelId { get; set; }

        [ForeignKey("GradeLevelId")]
        public GradeLevel GradeLevel { get; set; }
       

        public int? SchoolClassId { get; set; }

        [ForeignKey("SchoolClassId")]
        public SchoolClass SchoolClass { get; set; }


        public ICollection<TeacherCourse> TeacherCourses { get; private set; }

        public Course()
        {
            TeacherCourses = new Collection<TeacherCourse>();
        }

        public void Create()
        {

        }

        public void Modify(string title, string code, CourseCategory category, int programId, int gradeLevelId, int schoolClassId)
        {
            this.Code = code;
            this.Title = title;
            this.Category = category;
            this.ProgramId = programId;
            this.GradeLevelId = gradeLevelId;
            this.SchoolClassId = schoolClassId;
        }


    }
}