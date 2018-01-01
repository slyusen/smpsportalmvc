using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Persistence;

namespace SmpsPortal.Controllers.Api
{
    public class StudentLoadController : ApiController
    {
        private IUnitofWork _unitofWork;
        public StudentLoadController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetStudents(string id)
        {
            List<SelectListItem> scList = new List<SelectListItem>();
            int schoolClassId = Convert.ToInt32(id);
            var studentList = _unitofWork._StudentRepository.GetAllActiveStudentsByClass(schoolClassId);
            scList.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var sc in studentList)
            {
                
                scList.Add(new SelectListItem { Text = sc.FullName, Value = sc.Id.ToString() });
            }

            return Ok(new SelectList(scList, "Value", "Text"));
        }
    }
}
