using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Repositories
{
    public interface ICourseRepository
    {
        Course GetCourseById(int courseId);

        IEnumerable<Course> GetAllCoursesForStudent(string studentId, int schoolClassId);

        IEnumerable<Course> GetAllCourses();

        IEnumerable<Course> GetAllCoursesByProgram(int programId);

        IEnumerable<Course> GetAllCoursesByClassCategory(int schoolClassId, CourseCategory category);

        IEnumerable<Course> GetAllCoursesByClass(int schoolClassId);
        void Add(Course course);
        void Remove(Course course);

    }
}
