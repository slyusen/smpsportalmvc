namespace SmpsPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passVal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CummulativePoints = c.Double(nullable: false),
                        StudentId = c.String(maxLength: 128),
                        SchoolClassId = c.Int(nullable: false),
                        YearTermId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClassId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .ForeignKey("dbo.YearTerms", t => t.YearTermId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SchoolClassId)
                .Index(t => t.YearTermId);
            
            CreateTable(
                "dbo.SchoolClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Capacity = c.Int(nullable: false),
                        NumberofElectives = c.Int(nullable: false),
                        Description = c.String(),
                        ProgramId = c.Int(nullable: false),
                        GradeLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GradeLevels", t => t.GradeLevelId, cascadeDelete: true)
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .Index(t => t.ProgramId)
                .Index(t => t.GradeLevelId);
            
            CreateTable(
                "dbo.GradeLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateofBirth = c.DateTime(nullable: false),
                        Address = c.String(maxLength: 500),
                        ProfileImageUrl = c.String(maxLength: 500),
                        AboutMe = c.String(maxLength: 500),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Gender = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        StudentNumber = c.String(maxLength: 20),
                        DateEnrolled = c.DateTime(),
                        SchoolClassId = c.Int(),
                        GradeLevelId = c.Int(),
                        EmployeeNumber = c.String(maxLength: 20),
                        DateEmployed = c.DateTime(),
                        DateConfirmed = c.DateTime(),
                        IsConfirmed = c.Boolean(),
                        EmployeeType = c.Int(),
                        EmployeeCategory = c.Int(),
                        DepartmentId = c.Int(),
                        DesignationId = c.Int(),
                        Occupation = c.String(),
                        BusinessAddress = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Designations", t => t.DesignationId)
                .ForeignKey("dbo.GradeLevels", t => t.GradeLevelId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClassId, cascadeDelete: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.SchoolClassId)
                .Index(t => t.GradeLevelId)
                .Index(t => t.DepartmentId)
                .Index(t => t.DesignationId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CourseRegisters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        CourseId = c.Int(nullable: false),
                        DateRegistered = c.DateTime(nullable: false),
                        SchoolClassId = c.Int(nullable: false),
                        YearTermId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClassId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .ForeignKey("dbo.YearTerms", t => t.YearTermId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId)
                .Index(t => t.SchoolClassId)
                .Index(t => t.YearTermId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 40),
                        Code = c.String(),
                        Category = c.Int(nullable: false),
                        ProgramId = c.Int(nullable: false),
                        GradeLevelId = c.Int(),
                        SchoolClassId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GradeLevels", t => t.GradeLevelId)
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClassId)
                .Index(t => t.ProgramId)
                .Index(t => t.GradeLevelId)
                .Index(t => t.SchoolClassId);
            
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.String(maxLength: 128),
                        CourseId = c.Int(nullable: false),
                        DateRegistered = c.DateTime(nullable: false),
                        YearTermId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeId)
                .ForeignKey("dbo.YearTerms", t => t.YearTermId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.CourseId)
                .Index(t => t.YearTermId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Abbreviation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationUserId = c.String(maxLength: 128),
                        NotificationId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.NotificationUserId)
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .Index(t => t.NotificationUserId)
                .Index(t => t.NotificationId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        AnnouncementId = c.Int(),
                        AssessmentId = c.Int(),
                        OriginalAnnouncementTitle = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Announcements", t => t.AnnouncementId)
                .ForeignKey("dbo.Assessments", t => t.AssessmentId)
                .Index(t => t.AnnouncementId)
                .Index(t => t.AssessmentId);
            
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnnouncerId = c.String(maxLength: 128),
                        Title = c.String(),
                        Message = c.String(),
                        DateGiven = c.DateTime(nullable: false),
                        DateExpired = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserGroupName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AnnouncerId)
                .Index(t => t.AnnouncerId);
            
            CreateTable(
                "dbo.StudentParents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        ParentId = c.String(maxLength: 128),
                        RelationshipToStudent = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ParentId)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AttendeeId = c.String(maxLength: 128),
                        AssessmentId = c.Int(),
                        AnnouncementId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Announcements", t => t.AnnouncementId)
                .ForeignKey("dbo.Assessments", t => t.AssessmentId)
                .ForeignKey("dbo.AspNetUsers", t => t.AttendeeId)
                .Index(t => t.AttendeeId)
                .Index(t => t.AssessmentId)
                .Index(t => t.AnnouncementId);
            
            CreateTable(
                "dbo.Assessments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PercentofCa = c.Double(nullable: false),
                        DateGiven = c.DateTime(nullable: false),
                        DateDue = c.DateTime(nullable: false),
                        CourseId = c.Int(nullable: false),
                        SchoolClassId = c.Int(nullable: false),
                        Questions = c.String(),
                        Answers = c.String(),
                        IsChosen = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        HighestScore = c.Double(nullable: false),
                        TeacherId = c.String(maxLength: 128),
                        YearTermId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClassId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TeacherId)
                .ForeignKey("dbo.YearTerms", t => t.YearTermId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.SchoolClassId)
                .Index(t => t.TeacherId)
                .Index(t => t.YearTermId);
            
            CreateTable(
                "dbo.YearTerms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.DateTime(nullable: false),
                        Term = c.String(),
                        Status = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExamResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        ExamId = c.Int(nullable: false),
                        Score = c.Double(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ExamId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Duration = c.String(),
                        Questions = c.String(),
                        Answers = c.String(),
                        HighestScore = c.Double(nullable: false),
                        DateGiven = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        ExamNumber = c.String(),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.String(maxLength: 128),
                        YearTermId = c.Int(nullable: false),
                        SchoolClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClassId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TeacherId)
                .ForeignKey("dbo.YearTerms", t => t.YearTermId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.TeacherId)
                .Index(t => t.YearTermId)
                .Index(t => t.SchoolClassId);
            
            CreateTable(
                "dbo.CaResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        AssessmentId = c.Int(nullable: false),
                        Score = c.Double(nullable: false),
                        Response = c.String(),
                        DateSubmitted = c.DateTime(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assessments", t => t.AssessmentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.AssessmentId);
            
            CreateTable(
                "dbo.ClassPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CummulativePoints = c.Double(nullable: false),
                        Position = c.Int(nullable: false),
                        StudentId = c.String(maxLength: 128),
                        SchoolClassId = c.Int(nullable: false),
                        YearTermId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClassId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .ForeignKey("dbo.YearTerms", t => t.YearTermId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SchoolClassId)
                .Index(t => t.YearTermId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExamScore = c.Double(nullable: false),
                        TotalCAScore = c.Double(nullable: false),
                        StudentId = c.String(maxLength: 128),
                        SchoolClassId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        YearTermId = c.Int(nullable: false),
                        GradeSystemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.GradeSystems", t => t.GradeSystemId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClassId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .ForeignKey("dbo.YearTerms", t => t.YearTermId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SchoolClassId)
                .Index(t => t.CourseId)
                .Index(t => t.YearTermId)
                .Index(t => t.GradeSystemId);
            
            CreateTable(
                "dbo.GradeSystems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Points = c.Double(nullable: false),
                        Grade = c.String(),
                        Definition = c.String(),
                        UpBoundary = c.Double(nullable: false),
                        DownBoundary = c.Double(nullable: false),
                        Equivalent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.MenuCapabilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(),
                        MenuItemId = c.Int(),
                        RoleName = c.String(),
                        AllowedAction = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuComponents", t => t.MenuId)
                .ForeignKey("dbo.MenuComponents", t => t.MenuItemId)
                .Index(t => t.MenuId)
                .Index(t => t.MenuItemId);
            
            CreateTable(
                "dbo.MenuComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        MenuOrder = c.Int(nullable: false),
                        Description = c.String(),
                        Icon = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Menu_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuComponents", t => t.Menu_Id)
                .Index(t => t.Menu_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SchoolConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WebAdress = c.String(),
                        CAPercent = c.Double(nullable: false),
                        ExamPercent = c.Double(nullable: false),
                        CurrentYearTermId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.YearTerms", t => t.CurrentYearTermId, cascadeDelete: true)
                .Index(t => t.CurrentYearTermId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolConfigs", "CurrentYearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MenuCapabilities", "MenuItemId", "dbo.MenuComponents");
            DropForeignKey("dbo.MenuCapabilities", "MenuId", "dbo.MenuComponents");
            DropForeignKey("dbo.MenuComponents", "Menu_Id", "dbo.MenuComponents");
            DropForeignKey("dbo.MedicalRecords", "AppUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Grades", "YearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.Grades", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Grades", "SchoolClassId", "dbo.SchoolClasses");
            DropForeignKey("dbo.Grades", "GradeSystemId", "dbo.GradeSystems");
            DropForeignKey("dbo.Grades", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ClassPositions", "YearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.ClassPositions", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClassPositions", "SchoolClassId", "dbo.SchoolClasses");
            DropForeignKey("dbo.CaResults", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CaResults", "AssessmentId", "dbo.Assessments");
            DropForeignKey("dbo.AcademicRecords", "YearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.AcademicRecords", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "SchoolClassId", "dbo.SchoolClasses");
            DropForeignKey("dbo.AspNetUsers", "GradeLevelId", "dbo.GradeLevels");
            DropForeignKey("dbo.ExamResults", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Exams", "YearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.Exams", "TeacherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Exams", "SchoolClassId", "dbo.SchoolClasses");
            DropForeignKey("dbo.ExamResults", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseRegisters", "YearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.CourseRegisters", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseRegisters", "SchoolClassId", "dbo.SchoolClasses");
            DropForeignKey("dbo.CourseRegisters", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TeacherCourses", "YearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "AssessmentId", "dbo.Assessments");
            DropForeignKey("dbo.Notifications", "AnnouncementId", "dbo.Announcements");
            DropForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assessments", "YearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.Assessments", "TeacherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assessments", "SchoolClassId", "dbo.SchoolClasses");
            DropForeignKey("dbo.Assessments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Attendances", "AssessmentId", "dbo.Assessments");
            DropForeignKey("dbo.Attendances", "AnnouncementId", "dbo.Announcements");
            DropForeignKey("dbo.Announcements", "AnnouncerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentParents", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentParents", "ParentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "NotificationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Designations", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AspNetUsers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.TeacherCourses", "EmployeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "SchoolClassId", "dbo.SchoolClasses");
            DropForeignKey("dbo.Courses", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Courses", "GradeLevelId", "dbo.GradeLevels");
            DropForeignKey("dbo.AcademicRecords", "SchoolClassId", "dbo.SchoolClasses");
            DropForeignKey("dbo.SchoolClasses", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.SchoolClasses", "GradeLevelId", "dbo.GradeLevels");
            DropIndex("dbo.SchoolConfigs", new[] { "CurrentYearTermId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MenuComponents", new[] { "Menu_Id" });
            DropIndex("dbo.MenuCapabilities", new[] { "MenuItemId" });
            DropIndex("dbo.MenuCapabilities", new[] { "MenuId" });
            DropIndex("dbo.MedicalRecords", new[] { "AppUserId" });
            DropIndex("dbo.Grades", new[] { "GradeSystemId" });
            DropIndex("dbo.Grades", new[] { "YearTermId" });
            DropIndex("dbo.Grades", new[] { "CourseId" });
            DropIndex("dbo.Grades", new[] { "SchoolClassId" });
            DropIndex("dbo.Grades", new[] { "StudentId" });
            DropIndex("dbo.ClassPositions", new[] { "YearTermId" });
            DropIndex("dbo.ClassPositions", new[] { "SchoolClassId" });
            DropIndex("dbo.ClassPositions", new[] { "StudentId" });
            DropIndex("dbo.CaResults", new[] { "AssessmentId" });
            DropIndex("dbo.CaResults", new[] { "StudentId" });
            DropIndex("dbo.Exams", new[] { "SchoolClassId" });
            DropIndex("dbo.Exams", new[] { "YearTermId" });
            DropIndex("dbo.Exams", new[] { "TeacherId" });
            DropIndex("dbo.Exams", new[] { "CourseId" });
            DropIndex("dbo.ExamResults", new[] { "ExamId" });
            DropIndex("dbo.ExamResults", new[] { "StudentId" });
            DropIndex("dbo.Assessments", new[] { "YearTermId" });
            DropIndex("dbo.Assessments", new[] { "TeacherId" });
            DropIndex("dbo.Assessments", new[] { "SchoolClassId" });
            DropIndex("dbo.Assessments", new[] { "CourseId" });
            DropIndex("dbo.Attendances", new[] { "AnnouncementId" });
            DropIndex("dbo.Attendances", new[] { "AssessmentId" });
            DropIndex("dbo.Attendances", new[] { "AttendeeId" });
            DropIndex("dbo.StudentParents", new[] { "ParentId" });
            DropIndex("dbo.StudentParents", new[] { "StudentId" });
            DropIndex("dbo.Announcements", new[] { "AnnouncerId" });
            DropIndex("dbo.Notifications", new[] { "AssessmentId" });
            DropIndex("dbo.Notifications", new[] { "AnnouncementId" });
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropIndex("dbo.UserNotifications", new[] { "NotificationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Designations", new[] { "DepartmentId" });
            DropIndex("dbo.TeacherCourses", new[] { "YearTermId" });
            DropIndex("dbo.TeacherCourses", new[] { "CourseId" });
            DropIndex("dbo.TeacherCourses", new[] { "EmployeeId" });
            DropIndex("dbo.Courses", new[] { "SchoolClassId" });
            DropIndex("dbo.Courses", new[] { "GradeLevelId" });
            DropIndex("dbo.Courses", new[] { "ProgramId" });
            DropIndex("dbo.CourseRegisters", new[] { "YearTermId" });
            DropIndex("dbo.CourseRegisters", new[] { "SchoolClassId" });
            DropIndex("dbo.CourseRegisters", new[] { "CourseId" });
            DropIndex("dbo.CourseRegisters", new[] { "StudentId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "DesignationId" });
            DropIndex("dbo.AspNetUsers", new[] { "DepartmentId" });
            DropIndex("dbo.AspNetUsers", new[] { "GradeLevelId" });
            DropIndex("dbo.AspNetUsers", new[] { "SchoolClassId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SchoolClasses", new[] { "GradeLevelId" });
            DropIndex("dbo.SchoolClasses", new[] { "ProgramId" });
            DropIndex("dbo.AcademicRecords", new[] { "YearTermId" });
            DropIndex("dbo.AcademicRecords", new[] { "SchoolClassId" });
            DropIndex("dbo.AcademicRecords", new[] { "StudentId" });
            DropTable("dbo.SchoolConfigs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MenuComponents");
            DropTable("dbo.MenuCapabilities");
            DropTable("dbo.MedicalRecords");
            DropTable("dbo.GradeSystems");
            DropTable("dbo.Grades");
            DropTable("dbo.ClassPositions");
            DropTable("dbo.CaResults");
            DropTable("dbo.Exams");
            DropTable("dbo.ExamResults");
            DropTable("dbo.YearTerms");
            DropTable("dbo.Assessments");
            DropTable("dbo.Attendances");
            DropTable("dbo.StudentParents");
            DropTable("dbo.Announcements");
            DropTable("dbo.Notifications");
            DropTable("dbo.UserNotifications");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Designations");
            DropTable("dbo.Departments");
            DropTable("dbo.TeacherCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseRegisters");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Programs");
            DropTable("dbo.GradeLevels");
            DropTable("dbo.SchoolClasses");
            DropTable("dbo.AcademicRecords");
        }
    }
}
