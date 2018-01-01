using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class Exam
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Duration { get; set; }

        public string Questions { get; set; }

        public string Answers { get; set; }

        public double HighestScore { get; set; }

        public DateTime DateGiven { get; set; }

        public ExamType Type { get; set; }

        public string ExamNumber { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public string TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Employee Teacher { get; set; }

        public int YearTermId { get; set; }

        [ForeignKey("YearTermId")]
        public YearTerm YearTerm { get; set; }

        public int SchoolClassId { get; set; }

        [ForeignKey("SchoolClassId")]
        public SchoolClass SchoolClass { get; set; }

        

        public ICollection<ExamResult> Results { get; private set; }

        public Exam()
        {
            

            Results = new Collection<ExamResult>();
        }

        public void Modify(string title, string questions, string answers, string duration, double highestScore, ExamType type, DateTime dateGiven)
        {
            Title = title;
            Questions = questions;
            Answers = answers;
            Duration = duration;
            HighestScore = highestScore;
            Type = type;
            DateGiven = dateGiven;
        }
    }
}