using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private IApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Student GetStudentById(string studentId)
        {
            return _context.Students.SingleOrDefault(
                e =>
                e.Id == studentId);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            
            return _context.Students.ToList();
        }
        public IEnumerable<Student> GetAllActiveStudents()
        {
           
            return _context.Students.Where(
                e =>
                e.IsActive)
                .ToList();
        }

        public IEnumerable<Student> GetAllActiveStudentsByClass(int sclassId)
        {
            return _context.Students.Where(
                e =>
                e.SchoolClassId == sclassId
               ).ToList();
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
        }
        public void Remove(Student student)
        {
            _context.Students.Remove(student);
        }


    }
}