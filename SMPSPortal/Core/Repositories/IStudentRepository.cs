using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
    public interface IStudentRepository
    {
        Student GetStudentById(string studentId);
        IEnumerable<Student> GetAllStudents();
        IEnumerable<Student> GetAllActiveStudents();
        IEnumerable<Student> GetAllActiveStudentsByClass(int sclassId);
        void Add(Student student);
        void Remove(Student student);
    }
}
