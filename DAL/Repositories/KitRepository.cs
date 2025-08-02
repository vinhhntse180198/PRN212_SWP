using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class KitRepository
    {
        private DnaTestingContext _context;

        public List<KitComponent> GetAll()
        {
            _context = new();
            return _context.KitComponents
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(kc => kc.Service)
                .Include(kc => kc.SampleTypes)
                .ToList();
        }

        public List<KitComponent> GetByService(long serviceId)
        {
            _context = new();
            return _context.KitComponents
                .Include(k => k.Service)
                .Include(k => k.SampleTypes)
                .Where(k => k.ServiceId == serviceId)
                .ToList();
        }

        public List<KitComponent> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return GetAll();
            }

            _context = new();
            return _context.KitComponents
                .Include(k => k.Service)
                .Include(k => k.SampleTypes)
                .Where(k => (k.ComponentName != null && k.ComponentName.ToLower().Contains(searchText.ToLower())) || 
                       (k.Introduction != null && k.Introduction.ToLower().Contains(searchText.ToLower())))
                .ToList();
        }

        public void Create(KitComponent kit)
        {
            _context = new();
            _context.KitComponents.Add(kit);
            _context.SaveChanges();
        }

        public void Update(KitComponent kit)
        {
            _context = new();
            _context.KitComponents.Update(kit);
            _context.SaveChanges();
        }

        public void Delete(KitComponent kit)
        {
            _context = new();
            
            // Kiểm tra xem Kit có được sử dụng trong bất kỳ lịch hẹn nào không
            bool isUsed = _context.Appointments.Any(a => a.KitComponentId == kit.KitComponentId);
            if (isUsed)
            {
                throw new InvalidOperationException("Không thể xóa Kit này vì đang được sử dụng trong lịch hẹn.");
            }
            
            // Xóa các SampleType liên quan
            var sampleTypes = _context.SampleTypes.Where(s => s.KitComponentId == kit.KitComponentId).ToList();
            _context.SampleTypes.RemoveRange(sampleTypes);
            
            // Xóa Kit
            _context.KitComponents.Remove(kit);
            _context.SaveChanges();
        }
    }
} 