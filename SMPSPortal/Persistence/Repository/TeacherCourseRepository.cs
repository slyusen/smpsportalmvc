using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class TeacherCourseRepository : ITeacherCourseRepository
    {
        IApplicationDbContext _context;

        private string url;

        public TeacherCourseRepository(ApplicationDbContext context)
        {
            _context = context;
            url ="http://localhost";
        }

        public TeacherCourse GetTeacherCourseById(int teacherCourseId)
        {
            return _context.TeacherCourses.SingleOrDefault(tc => tc.Id == teacherCourseId);
        }
       
        public IEnumerable<TeacherCourse> GetAllTeacherCourses()
        {
           
            return
                _context.TeacherCourses
                    .ToList();
        }
        public IEnumerable<TeacherCourse> GetAllAssignedTeacherCourses(string empId)
        {
           
            return
                _context.TeacherCourses.Where(
                    tc =>
                    tc.EmployeeId == empId)
                    .ToList();
        }
        public IEnumerable<Course> GetAllAssignedCoursesForTeacher(string empId)
        {
           
            return
                _context.TeacherCourses.Where(
                    tc =>
                    tc.EmployeeId == empId).Select(tc => tc.Course)
                    .ToList();
        }
        public void Add(TeacherCourse tc)
        {
            _context.TeacherCourses.Add(tc);
        }

        public void Remove(TeacherCourse tc)
        {
            _context.TeacherCourses.Remove(tc);
        }
    }
}