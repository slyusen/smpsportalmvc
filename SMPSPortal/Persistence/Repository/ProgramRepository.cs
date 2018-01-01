using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class ProgramRepository : IProgramRepository
    {
        IApplicationDbContext _context;
       

        public ProgramRepository(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public Program GetProgramById(int programId)
        {
            return _context.Programs.SingleOrDefault(
                p =>
                p.Id == programId);
        }
       

        public IEnumerable<Program> GetAllPrograms()
        {
            
            return _context.Programs
                .ToList();
        }
        public IEnumerable<Program> GetAllProgramsByName(string name)
        {
            return _context.Programs.Where(
                p =>
                    p.Name == name)
                .ToList();
        }

        public void Add(Program program)
        {
            _context.Programs.Add(program);
        }
        public void Update(Program program)
        {
            var pro = _context.Programs.SingleOrDefault(p => p.Id == program.Id);
            pro.Name = program.Name;
            pro.Description = program.Description;


        }


        public void Remove(Program program)
        {
            _context.Programs.Remove(program);
        }
    }
}
