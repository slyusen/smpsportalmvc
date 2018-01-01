using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Core.Dtos;
using SmpsPortal.Persistence;

namespace SmpsPortal.Controllers.Api
{
    public class StudentYearTermLoadController : ApiController
    {
        private IUnitofWork _unitofWork;
        public StudentYearTermLoadController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetClass(UserRolesDto dto)
        {
            List<SelectListItem> scList = new List<SelectListItem>();
            string studentId = dto.ValuesList[0];

            var yTermList = _unitofWork._AcademicRecordRepository.GetStudentYearTermsInRecord(studentId);
            scList.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var sc in yTermList)
            {
                var term = sc.Year.Year.ToString() + " - " + sc.Term;
                scList.Add(new SelectListItem { Text = term, Value = sc.Id.ToString() });
            }

            return Ok(new SelectList(scList, "Value", "Text"));
        }
    }
}
