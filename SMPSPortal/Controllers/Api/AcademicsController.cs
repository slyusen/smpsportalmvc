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
    public class AcademicsController : ApiController
    {
        private IUnitofWork _unitofWork;

        public AcademicsController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }

        [HttpPost]
        public IHttpActionResult Index(UserRolesDto dto)
        {

            var Description = dto.ValuesList[1];

            var Name = dto.ValuesList[0];
            
            Program prog = new Program();
            prog.Name = Name;
            prog.Description = Description;
           

            _unitofWork._ProgramRepository.Add(prog);
            _unitofWork.Complete();
            return Ok();
        }

       
    }
}
