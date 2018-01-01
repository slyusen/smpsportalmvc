using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
   public interface IExamResultRepository
    {
        ExamResult GetExamResultById(int examResultId);
        IEnumerable<ExamResult> GetExamResultsByExamId(int examId);
        ExamResult GetExamResultByExamStudent(int examId, string studentId);
        bool ExamResultByStudentExamIdExists(int examId, string studentId);
        void Add(ExamResult examResult);
        void Remove(ExamResult examResult);
    }
}
