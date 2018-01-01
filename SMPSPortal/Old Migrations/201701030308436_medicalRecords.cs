namespace SmpsPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medicalRecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppUserId = c.String(maxLength: 128),
                        BloodGroup = c.String(),
                        Genotype = c.String(),
                        PhysicianName = c.String(),
                        PhysicianPhone = c.String(),
                        Hmo = c.String(),
                        HmoNumber = c.String(),
                        Allergies = c.String(),
                        Medications = c.String(),
                        BriefMedicalHistory = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUserId)
                .Index(t => t.AppUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalRecords", "AppUserId", "dbo.AspNetUsers");
            DropIndex("dbo.MedicalRecords", new[] { "AppUserId" });
            DropTable("dbo.MedicalRecords");
        }
    }
}
