using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class CaResultRepository : ICaResultRepository
    {
        IApplicationDbContext _context;
       
        public CaResultRepository(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public CaResult GetCaResultById(int caResultId)
        {
            return _context.CaResults.SingleOrDefault(c => c.Id == caResultId);
        }

        public IEnumerable<CaResult> GetCaResultsByCaId(int caId)
        {
            return _context.CaResults.Where(c => c.AssessmentId == caId);
        }

        public CaResult GetCaResultByCaStudent(int caId, string studentId)
        {
            
            return _context.CaResults.SingleOrDefault(c => c.AssessmentId == caId && c.StudentId == studentId);
        }


        public bool CaResultByStudentCaIdExists(int caId, string studentId)
        {
            return _context.CaResults.Any(c => c.AssessmentId == caId && c.StudentId == studentId);
        }

        public void Add(CaResult caResult)
        {
            _context.CaResults.Add(caResult);
        }

        public void Remove(CaResult caResult)
        {
            _context.CaResults.Remove(caResult);
        }
    }
}
