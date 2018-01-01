using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class SchoolClassesList
    {
        public SchoolClassesList()
        {

        }

        public SchoolClassesList(int id, string code, int capacity, int numberofElectives, string programTitle, string gradeLevelTitle)
        {
            this.Id = id;
            this.Code = code;
            this.Capacity = capacity;
            this.NumberofElectives = numberofElectives;
            this.ProgramTitle = programTitle;
            this.GradeLevelTitle = gradeLevelTitle;
            
        }
        public int Id { get; set; }

        public string Code { get; set; }

        public int Capacity { get; set; }

        public int NumberofElectives { get; set; }

        public string ProgramTitle { get; set; }

        public string GradeLevelTitle { get; set; }

        
    }
}