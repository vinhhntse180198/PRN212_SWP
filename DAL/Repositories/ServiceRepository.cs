using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ServiceRepository
    {
        private DnaTestingContext _context;

        public List<Service> GetAll()
        {
            _context = new();
            return _context.Services
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(s => s.ServiceTestPurposes)
                    .ThenInclude(stp => stp.TestPurpose)
                .Include(s => s.TestCategories)
                .Include(s => s.KitComponents)
                    .ThenInclude(kc => kc.SampleTypes)
                .ToList();
        }

        public List<Service> GetBySearch(string key)
        {
            List<Service> result = GetAll();
            if (string.IsNullOrWhiteSpace(key))
            {
                return result;
            }

            return result.Where(x => x.ServiceName.Contains(key, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void Create(Service x)
        {
            _context = new();
            _context.Services.Add(x);
            _context.SaveChanges();
        }

        public void Update(Service x)
        {
            _context = new();
            _context.Services.Update(x);
            _context.SaveChanges();
        }

        public void Delete(Service x)
        {
            _context = new();
            _context.Services.Remove(x);
            _context.SaveChanges();
        }


    }
}
