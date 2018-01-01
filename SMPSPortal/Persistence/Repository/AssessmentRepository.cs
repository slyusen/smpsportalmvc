using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private IApplicationDbContext _context;
        private string url;

        public AssessmentRepository(ApplicationDbContext context)
        {
            _context = context;
            url = "http://localhost";
        }

        
        public Assessment GetContinuousAssessmentById(int caId)
        {
            return _context.Assessments.SingleOrDefault(c => c.Id == caId);
        }

        public IEnumerable<Assessment> GetAssessmentByClass(int schoolClassId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Assessments.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.SchoolClassId == schoolClassId);
        }
        public IEnumerable<Assessment> GetAssessmentsByCourse(int courseId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Assessments.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.CourseId == courseId);
        }
        public IEnumerable<Assessment> GetAssessmentsByCourseChosen(int courseId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Assessments.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.CourseId == courseId &&
                    c.IsChosen);
        }
        public IEnumerable<Assessment> GetAssessmentsByCourseClassChosen(int courseId, int classId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Assessments.Where
                (c =>
                 
                    c.YearTermId == school.CurrentYearTermId &&
                    c.CourseId == courseId &&
                    c.SchoolClassId == classId &&
                    c.IsChosen);
        }
        public IEnumerable<Assessment> GetAssessmentsByTeacherCourse(string teacherId, int courseId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Assessments.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.CourseId == courseId &&
                    c.TeacherId == teacherId);
        }
        public IEnumerable<Assessment> GetAssessmentsByTeacher(string teacherId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Assessments.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId &&
                    c.TeacherId == teacherId);
        }

        public IEnumerable<Assessment> GetAllAssessments()
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Assessments.Where
                (c =>
                    c.YearTermId == school.CurrentYearTermId);
        }

        public void Add(Assessment caAssessment)
        {
            _context.Assessments.Add(caAssessment);
        }

        public void Remove(Assessment caAssessment)
        {
            _context.Assessments.Remove(caAssessment);
        }
    }
}