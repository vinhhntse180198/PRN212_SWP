using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class FeedbackRepository
    {
        private DnaTestingContext _context;
        public List<Feedback> GetAllFeedbacksByService(long serviceId)
        {
            _context = new();
            return _context.Feedbacks
                .Include(f => f.User) // Bắt buộc phải có dòng này!
                .Where(f => f.ServiceId == serviceId)
                .OrderByDescending(f => f.FeedbackDate)
                .ToList();
        }

        public void AddFeedback(Feedback feedback)
        {
            _context = new();
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }
    }
}