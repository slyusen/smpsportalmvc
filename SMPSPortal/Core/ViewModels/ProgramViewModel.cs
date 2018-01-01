using System.Collections.Generic;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class ProgramViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }

        public IEnumerable<Program> Programs { get; set; }

        public string Heading { get; set; }

        public IEnumerable<GradeLevel> GradeLevels { get; set; }
    }
}