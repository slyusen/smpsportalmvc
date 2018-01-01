using System.Data.Entity;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Course> Courses { get; set; }

        DbSet<Employee> Employees { get; set; }

        DbSet<Menu> Menus { get; set; }

        DbSet<MenuItem> MenuItems { get; set; }

        DbSet<Announcement> Announcements { get; set; }
        DbSet<MenuComponent> MenuComponents { get; set; }
        DbSet<Assessment> Assessments { get; set; }

        DbSet<Notification> Notifications { get; set; }

        DbSet<UserNotification> UserNotifications { get; set; }

        DbSet<Attendance> Attendances { get; set; }

        DbSet<MenuCapability> MenuCapabilities { get; set; }

        DbSet<Student> Students { get; set; }

        DbSet<Program> Programs { get; set; }

        DbSet<GradeLevel> GradeLevels { get; set; }

        DbSet<Exam> Exams { get; set; }

        DbSet<ExamResult> ExamResults { get; set; }

        DbSet<Grade> Grades { get; set; }

        DbSet<GradeSystem> GradeSystems { get; set; }

        DbSet<ClassPosition> ClassPositions { get; set; }

        DbSet<AcademicRecord> AcademicRecords { get; set; }

        DbSet<SchoolClass> SchoolClasses { get; set; }
        DbSet<TeacherCourse> TeacherCourses { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Designation> Designations { get; set; }
        DbSet<CaResult> CaResults { get; set; }

        ApplicationDbContext CreateContext();
        DbSet<Parent> Parents { get; set; }
        

        DbSet<StudentParent> StudentParents { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }

        DbSet<CourseRegister> CourseRegisters { get; set; }

        DbSet<SchoolConfig> SchoolConfigs { get; set; }

        DbSet<YearTerm> YearTerms { get; set; }

        Database Db { get; set; }
    }
}
