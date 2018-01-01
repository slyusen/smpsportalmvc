using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
    public interface IProgramRepository
    {
        Program GetProgramById(int programId);
        IEnumerable<Program> GetAllPrograms();
        IEnumerable<Program> GetAllProgramsByName(string name);
        void Add(Program program);
        void Remove(Program program);
        void Update(Program program);
    }
}
