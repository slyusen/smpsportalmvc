using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private IApplicationDbContext _context;

        public SchoolClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public SchoolClass GetSchoolClassById(int id)
        {
            return _context.SchoolClasses.SingleOrDefault(c => c.Id == id);
        }
        public IEnumerable<SchoolClass> GetAllSchoolClass()
        {
          return _context.SchoolClasses
                .ToList();
        }
        public IEnumerable<SchoolClass> GetSchoolClassByGradeLevel(int gradeLevelId)
        {
           return _context.SchoolClasses.Where(
                sc =>
                sc.GradeLevelId == gradeLevelId)
                .ToList();
        }
        public YearTerm GetYearTermById(int yearTermId)
        {
            return _context.YearTerms.SingleOrDefault(y => y.Id == yearTermId);
        }
        public void Add(SchoolClass schoolClass)
        {
            _context.SchoolClasses.Add(schoolClass);
        }
        public SchoolConfig GetSchoolConfigById(int schoolConfigId)
        {
            return _context.SchoolConfigs.SingleOrDefault(sg => sg.Id == schoolConfigId);
        }

        public SchoolConfig GetSchoolConfigByWebAddress(string webAdress)
        {
            return _context.SchoolConfigs.SingleOrDefault(sg => sg.WebAdress == webAdress);
        }
        public void Remove(SchoolClass schoolClass)
        {
            _context.SchoolClasses.Remove(schoolClass);
        }
    }
}