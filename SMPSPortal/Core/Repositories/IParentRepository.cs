using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
    public interface IParentRepository
    {
        Parent GetParentById(string parentId);
        IEnumerable<Parent> GetAllParents();
        void Add(Parent parent);
        void Remove(Parent parent);
    }
}
