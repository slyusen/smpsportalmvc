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
    public class StudentClassLoadController : ApiController
    {
        private IUnitofWork _unitofWork;
        public StudentClassLoadController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }
        public IHttpActionResult GetClass(UserRolesDto dto)
        {



            List<SelectListItem> scList = new List<SelectListItem>();
            string studentId = dto.ValuesList[0];

            var scclassList = _unitofWork._AcademicRecordRepository.GetStudentClassesInRecord(studentId);
            scList.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var sc in scclassList)
            {

                scList.Add(new SelectListItem { Text = sc.Code, Value = sc.Id.ToString() });
            }

            return Ok(new SelectList(scList, "Value", "Text"));
        }
    }
}
