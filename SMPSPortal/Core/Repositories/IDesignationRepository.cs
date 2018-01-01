using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
   public interface IDesignationRepository
    {
        Designation GetDesignationById(int designationId);

        IEnumerable<Designation> GetAllDesignationsBySchool();

        IEnumerable<Designation> GetAllDesignationsBySchoolDepartment(int departmentId);

        IEnumerable<Designation> GetAllDesignationsBySchoolTitle(string title);


        void Add(Designation designation);

        void Remove(Designation designation);

        void Update(Designation designation);
    }
}
