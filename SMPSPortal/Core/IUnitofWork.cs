using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Core
{
   public interface IUnitofWork
   {
        IUserRepository _UserRepository { get; }
       
        IDepartmentRepository _DepartmentRepository { get; }
        IDesignationRepository _DesignationRepository { get; }
        IProgramRepository _ProgramRepository { get; }
        IGradeLevelRepository _GradeLevelRepository { get; }
        IEmployeeRepository _EmployeeRepository { get; }
        IStudentRepository _StudentRepository { get; }
        IParentRepository _ParentRepository { get; }
        ISchoolClassRepository _SchoolClassRepository { get; }
        IStudentParentRepository _StudentParentRepository { get; }
        ICourseRepository _CourseRepository { get; }
        IAttendanceRepository _AttendanceRepository { get; }
        ICaResultRepository _CaResultRepository { get; }
        IExamRepository _ExamRepository { get; }
        IExamResultRepository _ExamResultRepository { get; }
        IGradeRepository _GradeRepository { get; }
        IAnnouncementRepository _AnnouncementRepository { get; }
        ITeacherCourseRepository _TeacherCourseRepository { get; }
        IUserNotificationsRepository _UserNotificationsRepository { get; }
        IAcademicRecordRepository _AcademicRecordRepository { get; }
        IAssessmentRepository _AssessmentRepository { get; }
        ICourseRegisterRepository _CourseRegisterRepository { get; }

    
       void Complete();
   }
}
