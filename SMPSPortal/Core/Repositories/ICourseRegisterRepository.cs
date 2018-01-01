using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
   public interface ICourseRegisterRepository
    {
        CourseRegister GetCourseRegisterById(int courseRegisterId);

        IEnumerable<CourseRegister> GetCourseRegistersByStudentYearTermId(string studentId, int yearTermId);

        IEnumerable<CourseRegister> GetCourseRegistersByStudentCourseYearTermId(string studentId, int courseId,
            int yearTermId);

        IEnumerable<CourseRegister> GetCourseRegistersByStudentClass(string studentId, int classId);
        IEnumerable<Course> GetCoursesByStudentClass(string studentId, int classId);
        bool CheckCourseByStudentClass(int courseId, string studentId, int classId);
        CourseRegister GetCourseByStudentClass(int courseId, string studentId, int classId);
        IEnumerable<Student> GetStudentsByCourseClass(int courseId, int classId);

        void Add(CourseRegister courseRegister);

        void Remove(CourseRegister courseRegister);
    }
}
