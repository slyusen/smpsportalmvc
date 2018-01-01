using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using SmpsPortal.Core;
using SmpsPortal.Core.Dtos;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;
using SmpsPortal.Persistence;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace SmpsPortal.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private IUnitofWork _unitofWork;
        public NotificationsController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }

        public IEnumerable<NotificationDto> GetNewNotification()
        {
            var userId = User.Identity.GetUserId();
            var notifyExits = _unitofWork._UserNotificationsRepository.IsUserNotificationExist(userId);
            if (notifyExits)
            {
                var userNts = _unitofWork._UserNotificationsRepository.GetUserNotifiationsForUser(userId);
                var notifyDto = new List<NotificationDto>();
                foreach (var userNt in userNts)
                {
                    var un = _unitofWork._UserNotificationsRepository.GetNotificationById(userNt.NotificationId);

                    if (un.Type == NotificationType.AnnouncementCreated)
                    {
                        var nDto = new NotificationDto();
                        nDto.DateTime = un.DateTime;
                        nDto.Type = un.Type;
                        var annouce = _unitofWork._AnnouncementRepository.GetAnnouncementById((int)un.AnnouncementId);
                        nDto.Announcement = annouce.IfNotNull(Mapper.Map<Announcement, AnnouncementDto>);
                        var announcer = _unitofWork._UserRepository.GetSingleUser(annouce.AnnouncerId);
                        nDto.Announcement.Announcer = announcer.IfNotNull(Mapper.Map<ApplicationUser, UserDto>);
                        notifyDto.Add(nDto);
                    }
                    if (un.Type == NotificationType.AssessmentCreated)
                    {
                        var nDto = new NotificationDto();
                        nDto.DateTime = un.DateTime;
                        nDto.Type = un.Type;
                        var assess = _unitofWork._AssessmentRepository.GetContinuousAssessmentById((int)un.AssessmentId);
                        nDto.Assessment = assess.IfNotNull(Mapper.Map<Assessment, AssessmentDto>);
                        var course = _unitofWork._CourseRepository.GetCourseById(assess.CourseId);
                        nDto.Assessment.Course = course.IfNotNull(Mapper.Map<Course, CourseDto>);
                        notifyDto.Add(nDto);
                    }
                   
                    if (un.Type == NotificationType.AnnouncementUpdated)
                    {
                        var nDto = new NotificationDto();
                        nDto.DateTime = un.DateTime;
                        nDto.Type = un.Type;
                        nDto.OriginalAnnouncementTitle = un.OriginalAnnouncementTitle;

                        var annouce = _unitofWork._AnnouncementRepository.GetAnnouncementById((int)un.AnnouncementId);
                        nDto.Announcement = annouce.IfNotNull(Mapper.Map<Announcement, AnnouncementDto>);
                        var announcer = _unitofWork._UserRepository.GetSingleUser(annouce.AnnouncerId);
                        nDto.Announcement.Announcer = announcer.IfNotNull(Mapper.Map<ApplicationUser, UserDto>);
                        notifyDto.Add(nDto);
                    }
                  
                }

                return notifyDto;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public IHttpActionResult MarkRead()
        {
            var userId = User.Identity.GetUserId();

            var userNotifications = _unitofWork._UserNotificationsRepository.GetAllUnreaUserNotifications(userId);

            userNotifications.ForEach(u => u.MarkIsRead());

            _unitofWork.Complete();

            return Ok();
        }
    }
}
