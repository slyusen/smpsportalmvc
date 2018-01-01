using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
   public interface IAssessmentRepository
   {
       Assessment GetContinuousAssessmentById(int caId);
       IEnumerable<Assessment> GetAssessmentByClass(int schoolClassId);
       IEnumerable<Assessment> GetAssessmentsByCourse(int courseId);
       IEnumerable<Assessment> GetAssessmentsByCourseChosen(int courseId);
       IEnumerable<Assessment> GetAssessmentsByCourseClassChosen(int courseId, int classId);
       IEnumerable<Assessment> GetAssessmentsByTeacherCourse(string teacherId, int courseId);
       IEnumerable<Assessment> GetAssessmentsByTeacher(string teacherId);
       IEnumerable<Assessment> GetAllAssessments();
       void Add(Assessment caAssessment);
       void Remove(Assessment caAssessment);
   }
}
