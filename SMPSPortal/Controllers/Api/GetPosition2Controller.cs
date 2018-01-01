using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmpsPortal.Core;
using SmpsPortal.Core.Dtos;
using SmpsPortal.Persistence;

namespace SmpsPortal.Controllers.Api
{
    public class GetPosition2Controller : ApiController
    {
        private IUnitofWork _unitofWork;

        public GetPosition2Controller()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }

        [HttpPost]
        public IHttpActionResult GetPos(UserRolesDto dto)
        {
            var studentId = dto.ValuesList[0];

            var classId = Convert.ToInt32(dto.ValuesList[1]);
            var yTermId = Convert.ToInt32(dto.ValuesList[2]);


            var classPos = _unitofWork._AcademicRecordRepository.GetByStudentClassYearTerm2(studentId, classId, yTermId).FirstOrDefault();

            return Ok(classPos.Position);
        }
    }
}
