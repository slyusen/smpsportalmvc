using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Persistence.Repository
{
    public class CourseRepository : ICourseRepository
    {
        IApplicationDbContext _context;
       
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses
                .SingleOrDefault(c => c.Id == courseId);
        }
        
        public IEnumerable<Course> GetAllCoursesForStudent(string studentId, int schoolClassId)
        {
            var courses = GetAllCoursesByClass(schoolClassId);
           
            var cRegister = _context.CourseRegisters.Where(
                cr =>
                cr.StudentId == studentId &&
                cr.SchoolClassId == schoolClassId)
                .ToList();
            List<int> regCourse = cRegister.Select(c => c.CourseId).ToList();

            var mCourses = courses.Where(c => c.Category == CourseCategory.Compulsory || regCourse.Contains(c.Id));

            return mCourses;
        }
        public IEnumerable<Course> GetAllCourses()
        {
           
            return _context.Courses
                .ToList();
        }

        public IEnumerable<Course> GetAllCoursesByProgram(int programId)
        {
            
            return _context.Courses
                .Where(
                c => c.ProgramId == programId)
                .ToList();
        }

        public IEnumerable<Course> GetAllCoursesByClassCategory(int schoolClassId, CourseCategory category)
        {
           return _context.Courses
                .Where(
                c =>
                c.SchoolClassId == schoolClassId &&
                c.Category == category)
                .ToList();
        }

        public IEnumerable<Course> GetAllCoursesByClass(int schoolClassId)
        {
            
            return _context.Courses
                .Where(
                    c => c.SchoolClassId == schoolClassId)
                .ToList();
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Remove(Course course)
        {
            _context.Courses.Remove(course);
        }
    }
}