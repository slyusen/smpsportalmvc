using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    [Serializable]
    public class TeacherCourseList
    {

        public TeacherCourseList()
        {

        }

        public TeacherCourseList(int courseId, string teacherId, string courseCode, string teacherName)
        {
            this.CourseCode = courseCode;
            this.Id = courseId;
            this.TeacherId = teacherId;
            this.TeacherName = teacherName;
        }
        public int Id { get; set; }

        public string TeacherId { get; set; }

        public string CourseCode { get; set; }

        public string TeacherName { get; set; }

    }
}