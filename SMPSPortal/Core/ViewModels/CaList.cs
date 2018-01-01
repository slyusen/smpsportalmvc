using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class CaList
    {
        public CaList(int id, string title, string type, double score, string remarks, double highestScore, int courseId)
        {
            Id = id;
            Title = title;
            Type = type;
            Score = score;
            Remarks = remarks;
            HighestScore = highestScore;
            CourseId = courseId;
        }

        public CaList(int id, string title, string type, string className, double highestScore, int courseId)
        {
            Id = id;
            Title = title;
            Type = type;
            ClassName = className;
            HighestScore = highestScore;
            CourseId = courseId;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public int CourseId { get; set; }

        public string ClassName { get; set; }

        public double HighestScore { get; set; }

        public double Score { get; set; }

        public string Remarks { get; set; }

    }
}