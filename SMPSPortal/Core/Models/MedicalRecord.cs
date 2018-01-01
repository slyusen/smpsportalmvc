using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }

        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public ApplicationUser AppUser { get; set; }

        public string BloodGroup { get; set; }

        public string Genotype { get; set; }

        public string PhysicianName { get; set; }

        public string PhysicianPhone { get; set; }

        public string Hmo { get; set; }

        public string HmoNumber { get; set; }

        public string Allergies { get; set; }

        public string Medications { get; set; }

        public string BriefMedicalHistory { get; set; }

        public void Modify(string bloodGroup, string genotype, string physicianName, string physicianPhone, string hmo, string hmoNumber, string allergies, string medications, string briefMedHistory)
        {
            this.Allergies = allergies;
            this.BloodGroup = bloodGroup;
            this.BriefMedicalHistory = briefMedHistory;
            this.Genotype = genotype;
            this.Hmo = hmo;
            this.HmoNumber = hmoNumber;
            this.Medications = medications;
            this.PhysicianName = physicianName;
            this.PhysicianPhone = physicianPhone;

        }
    }
}