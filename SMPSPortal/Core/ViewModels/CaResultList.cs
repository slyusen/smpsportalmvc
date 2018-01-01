using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class CaResultList
    {
        public CaResultList()
        {

        }
        public CaResultList(int id, string studentName, double score, DateTime? dateSubmitted, int caId, string remark)
        {
            Id = id;
            StudentName = studentName;
            Score = score;
            DateSubmitted = dateSubmitted;
            CaId = caId;
            Remark = remark;
        }
        public int Id { get; set; }

        public string StudentName { get; set; }

        public double Score { get; set; }

        public DateTime? DateSubmitted { get; set; }

        public int CaId { get; set; }

        public string Remark { get; set; }
    }
}