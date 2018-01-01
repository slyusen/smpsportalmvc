using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
   public interface ISchoolClassRepository
   {
       SchoolClass GetSchoolClassById(int id);
       IEnumerable<SchoolClass> GetAllSchoolClass();
       IEnumerable<SchoolClass> GetSchoolClassByGradeLevel(int gradeLevelId);
       void Add(SchoolClass schoolClass);
       void Remove(SchoolClass schoolClass);
       YearTerm GetYearTermById(int yearTermId);
       SchoolConfig GetSchoolConfigById(int schoolConfigId);
       SchoolConfig GetSchoolConfigByWebAddress(string webAdress);
   }
}
