using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using SmpsPortal.Core;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;
using SmpsPortal.Persistence.Repository;

namespace SmpsPortal.Persistence
{
    
    public class UnitofWork : IUnitofWork
    {
        private ApplicationDbContext _context;

        public IUserRepository _UserRepository { get; private set; }
        
        public IDepartmentRepository _DepartmentRepository { get; private set; }
        public IDesignationRepository _DesignationRepository { get; private set; }
        public IProgramRepository _ProgramRepository { get; private set; }
        public IGradeLevelRepository _GradeLevelRepository { get; private set; }
        public IEmployeeRepository _EmployeeRepository { get; private set; }
        public IStudentRepository _StudentRepository { get; private set; }
        public IParentRepository _ParentRepository { get; private set; }
        public ISchoolClassRepository _SchoolClassRepository { get; private set; }
        public IStudentParentRepository _StudentParentRepository { get; private set; }
        public  ICourseRepository _CourseRepository { get; private set; }
        public IAttendanceRepository _AttendanceRepository { get; private set; }
        public ICaResultRepository _CaResultRepository { get; private set; }
        public IAssessmentRepository _AssessmentRepository { get; private set; }
        public ICourseRegisterRepository _CourseRegisterRepository { get; private set; }
        public IExamRepository _ExamRepository { get; set; }
        public IExamResultRepository _ExamResultRepository { get; set; }
        public  IGradeRepository _GradeRepository { get; set; }
        public IAnnouncementRepository _AnnouncementRepository { get; set; }

        public  ITeacherCourseRepository _TeacherCourseRepository { get; set; }
        
        public  IUserNotificationsRepository _UserNotificationsRepository { get; set; }

        public IAcademicRecordRepository _AcademicRecordRepository { get; set; }

       

        

        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            _UserRepository = new UsersRepository(_context);
           
            _DepartmentRepository = new DepartmentRepository(_context);
            _DesignationRepository = new DesignationRepository(_context);
            _GradeLevelRepository = new GradeLevelRepository(_context);
            _ProgramRepository = new ProgramRepository(_context);
            _EmployeeRepository = new EmployeeRepository(_context);
            _StudentRepository = new StudentRepository(_context);
            _ParentRepository = new ParentRepository(_context);
            _SchoolClassRepository = new SchoolClassRepository(_context);
            _StudentParentRepository = new StudentParentRepository(_context);
            _CaResultRepository = new CaResultRepository(_context);
            _AssessmentRepository = new AssessmentRepository(_context);
            _CourseRegisterRepository = new CourseRegisterRepository(_context);
            _AnnouncementRepository = new AnnouncementRepository(_context);
            _ExamRepository = new ExamRepository(_context);
            _ExamResultRepository = new ExamResultRepository(_context);
            _GradeRepository = new GradeRepository(_context);
            _CourseRepository = new CourseRepository(_context);
            _AttendanceRepository = new AttendanceRepository(_context);
            _TeacherCourseRepository = new TeacherCourseRepository(_context);
            _UserNotificationsRepository = new UserNotificationsRepository(_context);
            _AcademicRecordRepository = new AcademicRecordRepository(_context);
            
        }

        public void Complete()
        {
            _context.ChangeTracker.DetectChanges(); // Force EF to match associations.
            var objectContext = ((IObjectContextAdapter)_context).ObjectContext;
            var objectStateManager = objectContext.ObjectStateManager;
            var fieldInfo = objectStateManager.GetType().GetField("_entriesWithConceptualNulls", BindingFlags.Instance | BindingFlags.NonPublic);
            var conceptualNulls = fieldInfo.GetValue(objectStateManager);
            if (conceptualNulls != null)
            {
                objectStateManager.ChangeObjectState(_context.MenuComponents, EntityState.Unchanged);
            }
            _context.SaveChanges();
        }
    }
}