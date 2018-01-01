using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class ParentRepository : IParentRepository
    {
        IApplicationDbContext _context;
        public ParentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Parent GetParentById(string parentId)
        {
            return _context.Parents.SingleOrDefault(p => p.Id == parentId);
        }

        public IEnumerable<Parent> GetAllParents()
        {
           return _context.Parents.ToList();
        }

        public void Add(Parent parent)
        {
            _context.Parents.Add(parent);
        }

        public void Remove(Parent parent)
        {
            _context.Parents.Remove(parent);
        }
    }
}