using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class CaListItems
    {
        public CaListItems(int id, string title, string type, double score, string remarks, double highestScore, int courseId, string courseCode)
        {
            Id = id;
            Title = title;
            Type = type;
            Score = score;
            Remarks = remarks;
            HighestScore = highestScore;
            CourseId = courseId;
            CourseCode = courseCode;
        }



        public int Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public int CourseId { get; set; }

        public string CourseCode { get; set; }

        public double HighestScore { get; set; }

        public double Score { get; set; }

        public string Remarks { get; set; }
    }
}