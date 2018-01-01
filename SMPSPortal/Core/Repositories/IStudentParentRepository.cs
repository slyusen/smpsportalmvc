using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
    public interface IStudentParentRepository
    {
        StudentParent GetStudentParentById(int studentParentId);
        StudentParent GetStudentParentByStudentParentId(string studentId, string parentId);
        IEnumerable<StudentParent> GetAllStudentParentsByStudent(string studentId);
        IEnumerable<StudentParent> GetAllStudentParentsByParent(string parentId);
        void Add(StudentParent studentParent);
        void Remove(StudentParent studentParent);
    }
}