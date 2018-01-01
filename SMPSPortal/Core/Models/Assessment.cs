using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SmpsPortal.Core.Repositories;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class Assessment 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double PercentofCa { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Given")]
        public DateTime DateGiven { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Due")]
        public DateTime DateDue { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }


        public int SchoolClassId { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }

        public string Questions { get; set; }

        public string Answers { get; set; }

        public bool IsChosen { get; set; }

        public AssessmentType Type { get; set; }

        public double HighestScore { get; set; }


        public string TeacherId { get; set; }

        public virtual Employee Teacher { get; set; }

        public  int YearTermId { get; set; }

        public  YearTerm YearTerm {get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        public Assessment()
        {
            Attendances = new Collection<Attendance>();
        }


        public void Modify(string title, string questions, string answers, AssessmentType type, double highestScore, double percentCa, DateTime dateDue, int courseId, bool isChosen)
        {
            Title = title;
            Questions = questions;
            Answers = answers;
            Type = type;
            HighestScore = highestScore;
            PercentofCa = percentCa;
            DateDue = dateDue;
            CourseId = courseId;
            IsChosen = isChosen;
        }
        public void Create()
        {
            var notification = Notification.AssessmentCreated(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}