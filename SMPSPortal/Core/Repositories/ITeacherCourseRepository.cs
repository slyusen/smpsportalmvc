using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
    public interface ITeacherCourseRepository
    {
        TeacherCourse GetTeacherCourseById(int teacherCourseId);
        IEnumerable<TeacherCourse> GetAllTeacherCourses();
        IEnumerable<TeacherCourse> GetAllAssignedTeacherCourses(string empId);
        IEnumerable<Course> GetAllAssignedCoursesForTeacher(string empId);
        void Add(TeacherCourse tc);
        void Remove(TeacherCourse tc);
    }
}
