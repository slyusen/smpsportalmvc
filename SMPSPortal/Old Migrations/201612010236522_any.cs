namespace SmpsPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class any : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "SchoolClassId", "dbo.SchoolClasses");
            DropForeignKey("dbo.Courses", "GradeLevelId", "dbo.GradeLevels");
            DropIndex("dbo.Courses", new[] { "GradeLevelId" });
            DropIndex("dbo.Courses", new[] { "SchoolClassId" });
            AlterColumn("dbo.Courses", "GradeLevelId", c => c.Int());
            AlterColumn("dbo.Courses", "SchoolClassId", c => c.Int());
            CreateIndex("dbo.Courses", "GradeLevelId");
            CreateIndex("dbo.Courses", "SchoolClassId");
            AddForeignKey("dbo.Courses", "SchoolClassId", "dbo.SchoolClasses", "Id");
            AddForeignKey("dbo.Courses", "GradeLevelId", "dbo.GradeLevels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "GradeLevelId", "dbo.GradeLevels");
            DropForeignKey("dbo.Courses", "SchoolClassId", "dbo.SchoolClasses");
            DropIndex("dbo.Courses", new[] { "SchoolClassId" });
            DropIndex("dbo.Courses", new[] { "GradeLevelId" });
            AlterColumn("dbo.Courses", "SchoolClassId", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "GradeLevelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "SchoolClassId");
            CreateIndex("dbo.Courses", "GradeLevelId");
            AddForeignKey("dbo.Courses", "GradeLevelId", "dbo.GradeLevels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "SchoolClassId", "dbo.SchoolClasses", "Id", cascadeDelete: true);
        }
    }
}
