using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ReportModel
    {
        public DateTime SampleReceivedDate { get; set; }
        public string CustomerName { get; set; }
        public string TestType { get; set; }
        public int SampleCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string ResultFile
        {
            get; set;
        }
    }
}