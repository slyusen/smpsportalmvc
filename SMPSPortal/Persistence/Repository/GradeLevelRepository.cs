using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class GradeLevelRepository : IGradeLevelRepository
    {
        IApplicationDbContext _context;
        private string url;

        public GradeLevelRepository(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public GradeLevel GetGradeLevelById(int gradeLevelId)
        {
            return _context.GradeLevels.SingleOrDefault(
                g =>
                g.Id == gradeLevelId);
        }

      

        public IEnumerable<GradeLevel> GetAllGradeLevelBySchool()
        {
           
            return _context.GradeLevels;
        }

        public IEnumerable<GradeLevel> GetAllGradeLevelByTitleSchool(string title)
        {
            return _context.GradeLevels.Where(
                gl =>
                gl.Title == title);
        }
        public IEnumerable<GradeLevel> GetAllGradeLevelByCodeSchool(string code)
        {
            return _context.GradeLevels.Where(
                gl =>
                gl.Code == code);
        }

        public void Update(GradeLevel gradeLevel)
        {
            var gl = _context.GradeLevels.SingleOrDefault(g => g.Id == gradeLevel.Id);
            gl.Title = gradeLevel.Title;
            gl.Code = gradeLevel.Code;
        }

        public void Add(GradeLevel gradeLevel)
        {
            _context.GradeLevels.Add(gradeLevel);
        }

        public void Remove(GradeLevel gradeLevel)
        {
            _context.GradeLevels.Remove(gradeLevel);
        }
    }
}