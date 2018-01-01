using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
    public interface IGradeLevelRepository
    {
        GradeLevel GetGradeLevelById(int gradeLevelId);
        IEnumerable<GradeLevel> GetAllGradeLevelBySchool();
        IEnumerable<GradeLevel> GetAllGradeLevelByTitleSchool(string title);
        IEnumerable<GradeLevel> GetAllGradeLevelByCodeSchool(string codes);
        void Add(GradeLevel gradeLevel);
        void Remove(GradeLevel gradeLevel);
        void Update(GradeLevel gradeLevel);
    }
}
