using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class AppointmentService
    {
        private AppointmentRepository _repo = new();

        public void AddAppointment(Appointment x)
        {
            _repo.AddAppointment(x);
        }

        public List<Appointment> GetAllAppointments()
        {
            return _repo.GetAll();
        }

        public List<Appointment> GetAppointmentsForCustomer()
        {
            return _repo.GetAll();
        }

        public List<Appointment> GetAppointmentsBySearch(string searchText)
        {
            return _repo.GetAppointmentBySearch(searchText);
        }
        
        public void UpdateAppointment(Appointment appointment)
        {
            _repo.UpdateAppointment(appointment);
        }
    }
}
