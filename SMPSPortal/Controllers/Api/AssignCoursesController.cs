using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SmpsPortal.Core;
using SmpsPortal.Core.Dtos;
using SmpsPortal.Core.Models;
using SmpsPortal.Persistence;

namespace SmpsPortal.Controllers.Api
{
    public class AssignCoursesController : ApiController
    {
        IUnitofWork _unitofWork;

        public AssignCoursesController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }

        [HttpPost]
        public IHttpActionResult Index(UserRolesDto dto)
        {

            var teacherName = dto.ValuesList[1];

            var teacherId =dto.ValuesList[0];


            var currentAssignedCourses = _unitofWork._TeacherCourseRepository.GetAllAssignedTeacherCourses(teacherId);

            _unitofWork.Complete();

            foreach (var crs in currentAssignedCourses)
            {

                _unitofWork._TeacherCourseRepository.Remove(crs);
                _unitofWork.Complete();
            }
            for (var i = 2; i < dto.ValuesList.Count; i++)
            {
                string url ="http://localhost";
                var sch = _unitofWork._SchoolClassRepository.GetSchoolConfigByWebAddress(url);
                var tcourse = new TeacherCourse();
               
                tcourse.CourseId = Convert.ToInt32(dto.ValuesList[i]);
                tcourse.EmployeeId = teacherId;
                tcourse.DateRegistered = DateTime.Now;
                tcourse.YearTermId = sch.CurrentYearTermId;
                _unitofWork._TeacherCourseRepository.Add(tcourse);
                _unitofWork.Complete();

            }

            HttpContext.Current.Response.Redirect(Request.RequestUri.PathAndQuery);
            return Ok();
        }
       
    }
}
