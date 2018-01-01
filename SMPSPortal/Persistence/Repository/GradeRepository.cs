using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class GradeRepository : IGradeRepository
    {
        IApplicationDbContext _context;
        string url;
        public GradeRepository(ApplicationDbContext context)
        {
            _context = context;
            url = "http://localhost";
        }

     

        public Grade GetGradeById(int gradeId)
        {
            return _context.Grades.SingleOrDefault(g => g.Id == gradeId);
        }

        public GradeSystem GetGradeSystemById(int gradeSystemId)
        {
            return _context.GradeSystems.SingleOrDefault(g => g.Id == gradeSystemId);
        }

        public GradeSystem GetGradeSystemByGrade(string grade)
        {
            return _context.GradeSystems.SingleOrDefault(g => g.Grade == grade);
        }
        public GradeSystem GetGradeSystemByScoreBetween(double score)
        {
           
            return _context.GradeSystems.SingleOrDefault(g => score <= g.UpBoundary && score >= g.DownBoundary );
        }
        public IEnumerable<GradeSystem> GetAllGradeSystems()
        {
           
            return _context.GradeSystems;
        }
        public IEnumerable<Grade> GetAllGrades()
        {
          
            return _context.Grades;
        }
        public IEnumerable<Grade> GetAllGradesByClassYearTerm(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Grades.Where(g => g.SchoolClassId == schoolClassId && g.YearTermId == school.CurrentYearTermId);
        }
        public bool CheckGradesByClassYearTerm(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Grades.Any(g => g.SchoolClassId == schoolClassId && g.YearTermId == school.CurrentYearTermId);
        }
        public IEnumerable<Grade> GetAllGradesByStudentClassYearTerm(string studentId, int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Grades.Where(g => g.SchoolClassId == schoolClassId && g.StudentId == studentId && g.YearTermId == school.CurrentYearTermId);
        }
        public IEnumerable<Grade> GetAllGradesByStudentClassYearTerm2(string studentId, int schoolClassId, int yTermId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Grades.Where(g => g.SchoolClassId == schoolClassId && g.StudentId == studentId && g.YearTermId == yTermId );
        }
        public IEnumerable<Grade> GetAllGradesByStudentClass(string studentId, int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Grades.Where(g => g.SchoolClassId == schoolClassId && g.StudentId == studentId);
        }
        public void ClearGradesForClass(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            var query = "DELETE FROM Grades WHERE SchoolClassId = " + schoolClassId + " AND YearTermId = " + school.CurrentYearTermId ;

            // _context.Db.ExecuteSqlCommand(query);

            var grades = _context.Grades.Any();
            
            if(grades)
            _context.Db.ExecuteSqlCommand(query);
        }
        public void ClearAcademicRecordsForClass(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            var query = "DELETE FROM AcademicRecords WHERE SchoolClassId = " + schoolClassId + " AND YearTermId = " + school.CurrentYearTermId;

             _context.Db.ExecuteSqlCommand(query);
           // ApplicationDbContext context = new ApplicationDbContext();
            //context.Database.ExecuteSqlCommand(query);
        }
        public void ClearClassPositionsForClass(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            var query = "DELETE FROM ClassPositions WHERE SchoolClassId = " + schoolClassId + " AND YearTermId = " + school.CurrentYearTermId;

            _context.Db.ExecuteSqlCommand(query);
           // ApplicationDbContext context = new ApplicationDbContext();
            //context.Database.ExecuteSqlCommand(query);
        }
        public void AddGrade(Grade grade)
        {
            _context.Grades.Add(grade);
        }
        public void RemoveGrade(Grade grade)
        {
            _context.Grades.Remove(grade);
        }
        public void AddGradeSystem(GradeSystem gradeSys)
        {
            _context.GradeSystems.Add(gradeSys);
        }
        public void RemoveGradeSystem(GradeSystem gradeSys)
        {
            _context.GradeSystems.Remove(gradeSys);
        }
    }
}