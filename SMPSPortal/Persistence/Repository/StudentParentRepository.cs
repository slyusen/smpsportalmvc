using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class StudentParentRepository : IStudentParentRepository
    {
        IApplicationDbContext _context;

        public StudentParentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public StudentParent GetStudentParentById(int studentParentId)
        {
            return _context.StudentParents.SingleOrDefault(sp => sp.Id == studentParentId);
        }
        public StudentParent GetStudentParentByStudentParentId(string studentId, string parentId)
        {
            return _context.StudentParents.SingleOrDefault(sp => sp.StudentId == studentId && sp.ParentId == parentId);
        }
        public IEnumerable<StudentParent> GetAllStudentParentsByStudent(string studentId)

        {
            return _context.StudentParents.Where(sp => sp.StudentId == studentId).ToList();
        }

        public IEnumerable<StudentParent> GetAllStudentParentsByParent(string parentId)
        {
            return _context.StudentParents.Where(sp => sp.ParentId == parentId)
                .Include(sp => sp.Student)
                .ToList();
        }

        public void Add(StudentParent studentParent)
        {
            _context.StudentParents.Add(studentParent);
        }

        public void Remove(StudentParent studentParent)
        {
            _context.StudentParents.Remove(studentParent);
        }
    }
}