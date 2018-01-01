using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
    public interface ICaResultRepository
    {
        CaResult GetCaResultById(int caResultId);
        IEnumerable<CaResult> GetCaResultsByCaId(int caId);
        CaResult GetCaResultByCaStudent(int caId, string studentId);
        bool CaResultByStudentCaIdExists(int caId, string studentId);
        void Add(CaResult caResult);
        void Remove(CaResult caResult);
    }
}
