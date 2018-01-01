using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
   public interface IGradeRepository
    {
        Grade GetGradeById(int gradeId);
        GradeSystem GetGradeSystemById(int gradeSystemId);
        GradeSystem GetGradeSystemByGrade(string grade);
        GradeSystem GetGradeSystemByScoreBetween(double score);
        IEnumerable<GradeSystem> GetAllGradeSystems();
        bool CheckGradesByClassYearTerm(int schoolClassId);
        IEnumerable<Grade> GetAllGrades();
        IEnumerable<Grade> GetAllGradesByClassYearTerm(int schoolClassId);
        IEnumerable<Grade> GetAllGradesByStudentClassYearTerm(string studentId, int schoolClassId);
        IEnumerable<Grade> GetAllGradesByStudentClass(string studentId, int schoolClassId);
        IEnumerable<Grade> GetAllGradesByStudentClassYearTerm2(string studentId, int schoolClassId, int yTermId);
        void ClearGradesForClass(int schoolClassId);
        void ClearAcademicRecordsForClass(int schoolClassId);
        void ClearClassPositionsForClass(int schoolClassId);
        void AddGrade(Grade grade);
        void RemoveGrade(Grade grade);
        void AddGradeSystem(GradeSystem gradeSys);
        void RemoveGradeSystem(GradeSystem gradeSys);
    }
}
