using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class AcademicResultList
    {
        public AcademicResultList()
        {

        }
        public AcademicResultList(int id, string studentName, double examScore, double totalCaScore, string courseCode, string classCode, string grade)
        {
            Id = id;
            StudentName = studentName;
            ExamScore = examScore;
            TotalCAScore = totalCaScore;
            CourseCode = courseCode;
            ClassCode = classCode;
            Grade = grade;
        }
        public int Id { get; set; }

        public string StudentName { get; set; }

        public string CourseCode { get; set; }

        public double ExamScore { get; set; }

        public double TotalCAScore { get; set; }

        public string ClassCode { get; set; }

        public string Grade { get; set; }
    }
}