using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SmpsPortal.Core.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public int Capacity { get; set; }

        public int NumberofElectives { get; set; }

        public string Description { get; set; }

        public int ProgramId { get; set; }

        public Program Program { get; set; }

        public int GradeLevelId { get; set; }

        public GradeLevel GradeLevel { get; set; }

     
        public void Modify(string code, int capacity, int numberofElectives, string description, int programId, int gradeLevelId)
        {
            Code = code;
            Capacity = capacity;
            NumberofElectives = numberofElectives;
            Description = description;
            ProgramId = programId;
            GradeLevelId = gradeLevelId;
           
        }
    }
}