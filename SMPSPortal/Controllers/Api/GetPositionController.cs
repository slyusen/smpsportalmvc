using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmpsPortal.Core;
using SmpsPortal.Persistence;

namespace SmpsPortal.Controllers.Api
{
    public class GetPositionController : ApiController
    {
        private IUnitofWork _unitofWork;

        public GetPositionController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }

        [HttpPost]
        public IHttpActionResult GetPos(string id)
        {

          
            var student = _unitofWork._StudentRepository.GetStudentById(id);
            var classPos = _unitofWork._AcademicRecordRepository.GetByStudentClassYearTerm(id, student.SchoolClassId).FirstOrDefault();

            return Ok(classPos.Position);
        }
    }
}
