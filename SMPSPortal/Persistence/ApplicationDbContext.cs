using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private  static ApplicationDbContext _context;
        public DbSet<Course> Courses { get; set; }
       public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<MenuComponent> MenuComponents { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Program> Programs { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<Assessment> Assessments { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<MenuCapability> MenuCapabilities { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<GradeLevel> GradeLevels { get; set; }

        public DbSet<SchoolClass> SchoolClasses { get; set; }

        public DbSet<TeacherCourse> TeacherCourses { get; set; }
       
        public DbSet<YearTerm> YearTerms { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<StudentParent> StudentParents { get; set; }

        public DbSet<CaResult> CaResults { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<ExamResult> ExamResults { get; set; }
       
        public DbSet<GradeSystem> GradeSystems { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<ClassPosition> ClassPositions { get; set; }

        public DbSet<AcademicRecord> AcademicRecords { get; set; }
        
        public DbSet<CourseRegister> CourseRegisters { get; set; }

        public DbSet<SchoolConfig> SchoolConfigs { get; set; }

        

        public Database Db { get; set; }

        private ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }

        public ApplicationDbContext CreateContext()
        {
            return Create();
        }

        public static ApplicationDbContext Create()
        {
            if (_context == null)
                _context = new ApplicationDbContext();

            return new ApplicationDbContext();
        }


    }
}