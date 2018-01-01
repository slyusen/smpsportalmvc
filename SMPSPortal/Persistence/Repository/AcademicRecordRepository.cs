using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class AcademicRecordRepository : IAcademicRecordRepository
    {
        IApplicationDbContext _context;
        string url;
        public AcademicRecordRepository(ApplicationDbContext context)
        {
            _context = context;
            url = "http://localhost";
        }
      
        public AcademicRecord GetAcademicRecordById(int id)
        {
            return _context.AcademicRecords.SingleOrDefault(a => a.Id == id);
        }
        public ClassPosition GetClassPositionById(int id)
        {
            return _context.ClassPositions.SingleOrDefault(a => a.Id == id);
        }
        public IEnumerable<ClassPosition> GetByClassYearTerm(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.ClassPositions.Where(c => c.SchoolClassId == schoolClassId && c.YearTermId == school.CurrentYearTermId);
        }
        public bool CheckByClassYearTerm(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.ClassPositions.Any(c => c.SchoolClassId == schoolClassId && c.YearTermId == school.CurrentYearTermId);
        }
        public IEnumerable<ClassPosition> GetByStudentClassYearTerm(string studentId, int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.ClassPositions.Where(c => c.StudentId == studentId && c.SchoolClassId == schoolClassId && c.YearTermId == school.CurrentYearTermId);
        }
        public IEnumerable<ClassPosition> GetByStudentClassYearTerm2(string studentId, int schoolClassId, int yTermId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.ClassPositions.Where(c => c.StudentId == studentId && c.SchoolClassId == schoolClassId && c.YearTermId == yTermId);
        }
        public IEnumerable<ClassPosition> GetByStudentYearTerm(string studentId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.ClassPositions.Where(c => c.StudentId == studentId && c.YearTermId == school.CurrentYearTermId);
        }
        public IEnumerable<ClassPosition> GetByRank(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            string query = "SELECT Id, CummulativePoints, StudentId, SchoolClassId, YearTermId, SchoolId, SchoolLocationId, RANK() OVER(ORDER BY CummulativePoints DESC) AS Position FROM dbo.ClassPositions WHERE SchoolClassId = " + schoolClassId + " AND YearTermId = " + school.CurrentYearTermId;
            IEnumerable<ClassPosition> data = _context.ClassPositions.SqlQuery(query);
            return data;
        }

        public IEnumerable<SchoolClass> GetClassesInRecord()
        {
           
            return _context.AcademicRecords.Select(a => a.SchoolClass).Distinct().ToList();
        }
        public IEnumerable<SchoolClass> GetStudentClassesInRecord(string studentId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.AcademicRecords.Where(a => a.StudentId == studentId).Select(a => a.SchoolClass).Distinct().ToList();
        }
        public IEnumerable<SchoolClass> GetParentClassesInRecord(List<string> studentId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.AcademicRecords.Where(a => studentId.Contains(a.StudentId)).Select(a => a.SchoolClass).Distinct().ToList();
        }

        public SchoolClass GetFirstParentClassesInRecord(string studentId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.AcademicRecords.Where(a => a.StudentId == studentId).Select(a => a.SchoolClass).Distinct().FirstOrDefault();
        }

        public IEnumerable<YearTerm> GetStudentYearTermsInRecord(string studentId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.AcademicRecords.Where(a => a.StudentId == studentId).Select(a => a.YearTerm).Distinct().ToList();
        }
        public IEnumerable<YearTerm> GetParentYearTermsInRecord(List<string> studentId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.AcademicRecords.Where(a => studentId.Contains(a.StudentId)).Select(a => a.YearTerm).Distinct().ToList();
        }

        public YearTerm GetFirstParentYearTermsInRecord(string studentId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.AcademicRecords.Where(a =>  a.StudentId == studentId).Select(a => a.YearTerm).Distinct().FirstOrDefault();
        }

        public IEnumerable<YearTerm> GetYearTermsInRecord()
        {
            
            return _context.AcademicRecords.Select(a => a.YearTerm).Distinct().ToList();
        }

        public IEnumerable<Student> GetStudentsInRecordByClassYearTerm(int classId, int yearTermId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.AcademicRecords.Where(a => a.SchoolClassId == classId && a.YearTermId == yearTermId).Select(a => a.Student).Distinct().ToList();
        }


        public IEnumerable<AcademicRecord> GetAllAcademicRecords()
        {
            return _context.AcademicRecords;

        }
        public IEnumerable<AcademicRecord> GetAcademicRecordByClassYearTerm(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.AcademicRecords.Where(a => a.SchoolClassId == schoolClassId && a.YearTermId == school.CurrentYearTermId);
        }
        public IEnumerable<AcademicRecord> GetAcademicRecordByStudentClassYearTerm(string studentId, int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.AcademicRecords.Where(a => a.StudentId == studentId && a.SchoolClassId == schoolClassId && a.YearTermId == school.CurrentYearTermId);
        }

        public void AcademicRecordInsertIntoClassPosition(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            var query = "INSERT INTO ClassPositions (YearTermId, StudentId, SchoolClassId, CummulativePoints, Position)  SELECT YearTermId, StudentId, SchoolClassId, CummulativePoints, DENSE_RANK() OVER(ORDER BY CummulativePoints DESC) FROM AcademicRecords  WHERE SchoolClassId = " + schoolClassId + " AND YearTermId = " + school.CurrentYearTermId;

             //_context.Db.ExecuteSqlCommand(query);
            ApplicationDbContext context = ApplicationDbContext.Create();
            context.Database.ExecuteSqlCommand(query);
        }

        public void AddRecord(AcademicRecord record)
        {
            _context.AcademicRecords.Add(record);
        }

        public void RemoveRecord(AcademicRecord record)
        {
            _context.AcademicRecords.Remove(record);
        }

        public void AddClassPosition(ClassPosition cp)
        {
            _context.ClassPositions.Add(cp);
        }
        public void RemoveClassPosition(ClassPosition cp)
        {
            _context.ClassPositions.Remove(cp);
        }
    }
}