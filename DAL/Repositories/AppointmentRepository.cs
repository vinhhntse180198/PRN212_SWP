using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AppointmentRepository
    {
        private DnaTestingContext _context;

        public void AddAppointment(Appointment appointment)
        {
            _context = new();
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public List<Appointment> GetAll()
        {
            _context = new();
            return _context.Appointments
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(x => x.Results)
                .Include(x => x.KitComponent)
                .Include(x => x.CollectedSamples)
                    .ThenInclude(cs => cs.SampleType)
                .ToList();
        }

        public List<Appointment> GetAppointmentBySearch(string searchText)
        {
            List<Appointment> result = GetAll();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return result;
            }

            return result.Where(x => x.FullName.Contains(searchText) ||
                            x.Phone.Contains(searchText) ||
                            x.Email.Contains(searchText) ||
                            x.ServiceType.Contains(searchText))
                            .ToList();
        }
        
        public void UpdateAppointment(Appointment appointment)
        {
            _context = new();
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }
    }
}
