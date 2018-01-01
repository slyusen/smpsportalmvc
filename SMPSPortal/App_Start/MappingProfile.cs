using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SmpsPortal.Core.Dtos;
using SmpsPortal.Core.Models;

namespace SmpsPortal.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Course, CourseDto>();
           
            Mapper.CreateMap<Notification, NotificationDto>();
            Mapper.CreateMap<Announcement, AnnouncementDto>();
            Mapper.CreateMap<Assessment, AssessmentDto>();


        }

        protected override  void Configure()
        {

        }
    }
}