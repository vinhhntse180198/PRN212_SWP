using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class ReportService
    {
        public List<ReportModel> GetAllReports()
        {
            using var context = new DnaTestingContext();

            var query = from a in context.Appointments
                        join s in context.Services on a.ServiceId equals s.ServiceId
                        where a.AppointmentDate != null
                        select new ReportModel
                        {
                            SampleReceivedDate = a.AppointmentDate,
                            CustomerName = a.FullName,
                            TestType = a.ServiceType,
                            SampleCount = a.CollectedSamples.Count, // hoặc 1 nếu mỗi appointment là 1 mẫu
                            TotalPrice = (decimal)s.Price,
                            Status = a.Status,
                            ResultFile = a.FirstResultFile
                        };

            return query.ToList();
        }
    }
}