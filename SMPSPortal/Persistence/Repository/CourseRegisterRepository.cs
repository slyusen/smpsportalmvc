using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class CourseRegisterRepository : ICourseRegisterRepository
    {
        IApplicationDbContext _context;
        private string url;

        public CourseRegisterRepository(ApplicationDbContext context)
        {
            _context = context;
            url = "http://localhost";
        }

        public CourseRegister GetCourseRegisterById(int courseRegisterId)
        {
            return _context.CourseRegisters.SingleOrDefault(cr => cr.Id == courseRegisterId);
        }
       
        public IEnumerable<CourseRegister> GetCourseRegistersByStudentClass(string studentId, int classId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.CourseRegisters.Where(
                cr =>
                cr.StudentId == studentId &&
                cr.SchoolClassId == classId)
                .ToList();
        }

        public IEnumerable<Course> GetCoursesByStudentClass(string studentId, int classId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.CourseRegisters.Where(
                cr =>
                cr.StudentId == studentId &&
                cr.SchoolClassId == classId).Select(cr => cr.Course)
                .ToList();
        }
        public IEnumerable<Student> GetStudentsByCourseClass(int courseId, int classId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.CourseRegisters.Where(
                cr =>
                cr.CourseId == courseId &&
                cr.SchoolClassId == classId).Select(cr => cr.Student)
                .ToList();
        }
        public bool CheckCourseByStudentClass(int courseId, string studentId, int classId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.CourseRegisters.Any(
                cr =>
                cr.StudentId == studentId &&
                cr.CourseId == courseId);
        }
        public CourseRegister GetCourseByStudentClass(int courseId, string studentId, int classId)
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.CourseRegisters.SingleOrDefault(
                cr =>
                cr.StudentId == studentId &&
                cr.SchoolClassId == classId &&
                cr.CourseId == courseId);
        }
        public IEnumerable<CourseRegister> GetCourseRegistersByStudentYearTermId(string studentId, int yearTermId)
        {
            return _context.CourseRegisters.Where(
                cr =>
                cr.StudentId == studentId &&
                cr.YearTermId == yearTermId)
                .ToList();
        }

        public IEnumerable<CourseRegister> GetCourseRegistersByStudentCourseYearTermId(string studentId, int courseId, int yearTermId)
        {
            return _context.CourseRegisters.Where(
                cr =>
                    cr.StudentId == studentId &&
                    cr.CourseId == courseId &&
                    cr.YearTermId == yearTermId)
                .ToList();
        }

        public void Add(CourseRegister courseRegister)
        {
            _context.CourseRegisters.Add(courseRegister);
        }

        public void Remove(CourseRegister courseRegister)
        {
            _context.CourseRegisters.Remove(courseRegister);
        }
    }
}