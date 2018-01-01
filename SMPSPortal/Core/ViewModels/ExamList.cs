using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class ExamList
    {
        public ExamList(int id, string title, string type, double score, string remarks, double highestScore, int courseId)
        {
            Id = id;
            Title = title;
            Type = type;
            Score = score;
            Remarks = remarks;
            HighestScore = highestScore;
            CourseId = courseId;
        }

        public ExamList(int id, string title, string examNumber, string type, string duration, string className, double highestScore, int courseId)
        {
            Id = id;
            Title = title;
            Type = type;
            ExamNumber = examNumber;
            Duration = duration;
            ClassName = className;
            HighestScore = highestScore;
            CourseId = courseId;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ExamNumber { get; set; }

        public string Type { get; set; }

        public int CourseId { get; set; }

        public string ClassName { get; set; }

        public string Duration { get; set; }

        public double HighestScore { get; set; }

        public double Score { get; set; }

        public string Remarks { get; set; }
    }
}