using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class ExamRepository : IExamRepository
    {
        IApplicationDbContext _context;
        private string url;

        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;
            url = "http://localhost";
        }
     
        public IEnumerable<Exam> GetExamsByClass(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Exams.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.SchoolClassId == schoolClassId);
        }
        public IEnumerable<Exam> GetExamsByCourse(int courseId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Exams.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.CourseId == courseId);
        }

        public IEnumerable<Exam> GetExamsByCourseClass(int courseId, int classId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Exams.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.CourseId == courseId &&
                    c.SchoolClassId == classId);
        }
        public bool CheckExamExistsByCourseClass(int courseId, int classId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Exams.Any
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.CourseId == courseId &&
                    c.SchoolClassId == classId);
        }
        public Exam GetExamById(int examid)
        {
            return _context.Exams.SingleOrDefault(e => e.Id == examid);
        }
        public IEnumerable<Exam> GetExamsByTeacherCourse(string teacherId, int courseId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Exams.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.CourseId == courseId &&
                    c.TeacherId == teacherId);
        }
        public IEnumerable<Exam> GetExamsByTeacher(string teacherId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Exams.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.TeacherId == teacherId);
        }
        

        public IEnumerable<Exam> GetAllExams()
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Exams.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId);
        }

       
        public void Add(Exam exam)
        {
            _context.Exams.Add(exam);
        }

        public void Remove(Exam exam)
        {
            _context.Exams.Remove(exam);
        }
    }
}