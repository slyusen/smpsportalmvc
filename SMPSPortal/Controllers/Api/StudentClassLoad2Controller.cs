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
    public class StudentClassLoad2Controller : ApiController
    {
        private IUnitofWork _unitofWork;
        public StudentClassLoad2Controller()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetStudents(int[] valList)
        {
            var yTerm = valList[1];

            var cclass = valList[0];
            List<SelectListItem> scList = new List<SelectListItem>();
            int schoolClassId = Convert.ToInt32(cclass);
            int yTermId = Convert.ToInt32(yTerm);
            var studentList = _unitofWork._AcademicRecordRepository.GetStudentsInRecordByClassYearTerm(schoolClassId, yTermId);
            scList.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var sc in studentList)
            {
                
                scList.Add(new SelectListItem { Text = sc.FullName, Value = sc.Id });
            }

            return Ok(new SelectList(scList, "Value", "Text"));
        }
    }
}
