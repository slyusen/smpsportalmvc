using System.ComponentModel.DataAnnotations.Schema;

namespace SmpsPortal.Core.Models
{
    public class ExamResult
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int ExamId { get; set; }

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        public double Score { get; set; }

        public string Remarks { get; set; }
    }
}