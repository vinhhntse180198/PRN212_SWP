using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class FeedbackService
    {
        private FeedbackRepository _repo = new();
        public List<Feedback> GetAllFeedbacksByService(long serviceId)
        {
            return _repo.GetAllFeedbacksByService(serviceId);
        }

        public void AddFeedback(Feedback f)
        {
            _repo.AddFeedback(f);
        }
    }
}