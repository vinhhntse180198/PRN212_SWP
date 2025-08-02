using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class ResultService
    {
        private ResultRepository _repository = new ResultRepository();

        public void AddResult(Result result)
        {
            _repository.Add(result);
        }

        public void UpdateResult(Result result)
        {
            _repository.Update(result);
        }

        public Result GetResultByAppointmentId(long appointmentId)
        {
            return _repository.GetByAppointmentId(appointmentId);
        }
    }
} 