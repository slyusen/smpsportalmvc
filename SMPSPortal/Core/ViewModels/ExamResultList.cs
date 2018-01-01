using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class ExamResultList
    {
        public ExamResultList()
        {

        }

        public ExamResultList(int id, string studentName, double score, string remark, int examId)
        {
            Id = id;
            StudentName = studentName;
            Score = score;
            Remark = remark;
            ExamId = examId;

        }
        public int Id { get; set; }

        public string StudentName { get; set; }

        public double Score { get; set; }

        public string Remark { get; set; }

        public int ExamId { get; set; }


    }

    public class ExamResultList2
    {
        public ExamResultList2(int id, string title, string type, double score, string remark, double highesScore, int courseId)
        {
            Id = id;
            Title = title;
            Type = type;
            Score = score;
            Remark = remark;
            HighestScore = highesScore;
            CourseId = courseId;
        }

        public int Id { get; set; }

        public double Score { get; set; }

        public string Title { get; set; }

        public string Remark { get; set; }

        public string Type { get; set; }

        public double HighestScore { get; set; }

        public int CourseId { get; set; }
    }
}