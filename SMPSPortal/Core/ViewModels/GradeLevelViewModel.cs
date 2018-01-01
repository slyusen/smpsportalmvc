using System.Collections.Generic;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class GradeLevelViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }
      
        public string Heading { get; set; }

        public IEnumerable<GradeLevel> GradeLevels { get; set; }
    }
}