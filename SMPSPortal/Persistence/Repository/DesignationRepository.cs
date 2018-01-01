using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class DesignationRepository : IDesignationRepository
    {
        IApplicationDbContext _context;
       
        public DesignationRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public Designation GetDesignationById(int designationId)
        {
            return _context.Designations.SingleOrDefault(d => d.Id == designationId);
        }

        public IEnumerable<Designation> GetAllDesignationsBySchool()
        {
           
            return _context.Designations.ToList();
        }


        public IEnumerable<Designation> GetAllDesignationsBySchoolDepartment(int departmentId)
        {
           
            return _context.Designations.Where(
                d =>
                    d.DepartmentId == departmentId)
                .ToList();
        }
        public IEnumerable<Designation> GetAllDesignationsBySchoolTitle(string title)
        {
            return _context.Designations.Where(
                d =>
                    d.Title == title)
                .ToList();
        }


        
        public void Add(Designation designation)
        {
            _context.Designations.Add(designation);
        }

        public void Remove(Designation designation)
        {
            _context.Designations.Remove(designation);
        }
        public void Update(Designation designation)
        {
            var desg = _context.Designations.SingleOrDefault(p => p.Id == designation.Id);
            desg.Title = designation.Title;
            desg.DepartmentId = designation.DepartmentId;

        }


    }
}
