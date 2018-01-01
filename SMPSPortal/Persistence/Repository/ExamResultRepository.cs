using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class ExamResultRepository : IExamResultRepository
    {
        IApplicationDbContext _context;
        private string url;


        public ExamResultRepository(ApplicationDbContext context)
        {
            _context = context;
            url = "http://localhost";
        }

        

        public ExamResult GetExamResultById(int examResultId)
        {
            return _context.ExamResults.SingleOrDefault(c => c.Id == examResultId);
        }

        public IEnumerable<ExamResult> GetExamResultsByExamId(int examId)
        {
            return _context.ExamResults.Where(c => c.ExamId == examId);
        }

        public ExamResult GetExamResultByExamStudent(int examId, string studentId)
        {
            return _context.ExamResults.SingleOrDefault(c => c.ExamId == examId && c.StudentId == studentId);
        }

        public bool ExamResultByStudentExamIdExists(int examId, string studentId)
        {
            return _context.ExamResults.Any(c => c.ExamId == examId && c.StudentId == studentId);
        }

        public void Add(ExamResult examResult)
        {
            _context.ExamResults.Add(examResult);
        }

        public void Remove(ExamResult examResult)
        {
            _context.ExamResults.Remove(examResult);
        }
    }
}