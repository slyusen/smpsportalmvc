using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
   public interface IExamRepository
    {
        IEnumerable<Exam> GetExamsByClass(int schoolClassId);
        IEnumerable<Exam> GetExamsByCourse(int courseId);
        Exam GetExamById(int examid);
        IEnumerable<Exam> GetExamsByTeacherCourse(string teacherId, int courseId);
        IEnumerable<Exam> GetExamsByTeacher(string teacherId);
        IEnumerable<Exam> GetAllExams();
        IEnumerable<Exam> GetExamsByCourseClass(int courseId, int classId);
        bool CheckExamExistsByCourseClass(int courseId, int classId);
       
        void Add(Exam exam);
        void Remove(Exam exam);
    }
}
