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
    public class SchoolClassesController : ApiController
    {
        private IUnitofWork _unitofWork;
        public SchoolClassesController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetSchoolClasses(string id)
        {
            List<SelectListItem> scList = new List<SelectListItem>();
            int gradeLevelId = Convert.ToInt32(id);
            var scClassList = _unitofWork._SchoolClassRepository.GetSchoolClassByGradeLevel(gradeLevelId);
            scList.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var sc in scClassList)
            {
                scList.Add(new SelectListItem { Text = sc.Code, Value = sc.Id.ToString() });
            }

            return Ok(new SelectList(scList, "Value", "Text"));
        }
    }
}
