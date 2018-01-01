using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmpsPortal.Core;
using SmpsPortal.Core.Dtos;
using SmpsPortal.Persistence;
using Microsoft.AspNet.Identity;

namespace SmpsPortal.Controllers.Api
{
    public class GetPosition3Controller : ApiController
    {
        private IUnitofWork _unitofWork;

        public GetPosition3Controller()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }

        [HttpPost]
        public IHttpActionResult GetPos(UserRolesDto dto)
        {
            var userId = User.Identity.GetUserId();
            var student = _unitofWork._StudentRepository.GetStudentById(userId);
            var classId = Convert.ToInt32(dto.ValuesList[0]);
            var yTermId = Convert.ToInt32(dto.ValuesList[1]);


            var classPos = _unitofWork._AcademicRecordRepository.GetByStudentClassYearTerm2(student.Id, classId, yTermId).FirstOrDefault();

            return Ok(classPos.Position);
        }
    }
}
