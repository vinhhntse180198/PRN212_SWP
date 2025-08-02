using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ResultRepository
    {
        private DnaTestingContext _context;

        public void Add(Result result)
        {
            _context = new();
            _context.Results.Add(result);
            _context.SaveChanges();
        }

        public void Update(Result result)
        {
            _context = new();
            _context.Results.Update(result);
            _context.SaveChanges();
        }

        public Result GetByAppointmentId(long appointmentId)
        {
            _context = new();
            return _context.Results.FirstOrDefault(r => r.AppointmentId == appointmentId);
        }
    }
} 