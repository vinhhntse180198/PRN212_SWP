using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TestPurposeRepository
    {
        private DnaTestingContext _context;

        public TestPurposeRepository()
        {
            _context = new DnaTestingContext();
        }

        public List<TestPurpose> GetAll()
        {
            _context = new();
            return _context.TestPurposes
                .AsNoTracking()
                .IgnoreQueryFilters()
                .ToList();
        }

        public TestPurpose? GetById(long id)
        {
            _context = new();
            return _context.TestPurposes
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefault(tp => tp.TestPurposeId == id);
        }

        public void Create(TestPurpose testPurpose)
        {
            _context = new();
            _context.TestPurposes.Add(testPurpose);
            _context.SaveChanges();
        }

        public void Update(TestPurpose testPurpose)
        {
            _context = new();
            _context.TestPurposes.Update(testPurpose);
            _context.SaveChanges();
        }

        public void Delete(TestPurpose testPurpose)
        {
            _context = new();
            _context.TestPurposes.Remove(testPurpose);
            _context.SaveChanges();
        }

        public List<TestPurpose> GetBySearch(string key)
        {
            _context = new();
            return _context.TestPurposes
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(tp => tp.TestPurposeName.Contains(key, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<TestPurpose> GetByServiceId(long serviceId)
        {
            _context = new();
            return _context.TestPurposes
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(tp => tp.ServiceTestPurposes.Any(stp => stp.ServiceId == serviceId))
                .ToList();
        }

        public void Add(TestPurpose testPurpose)
        {
            Create(testPurpose);
        }

        public List<TestPurpose> Search(string searchText)
        {
            return GetBySearch(searchText);
        }

        public bool IsNameExists(string name, long excludeId = 0)
        {
            _context = new();
            return _context.TestPurposes
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Any(tp => tp.TestPurposeName.Equals(name, StringComparison.OrdinalIgnoreCase) && tp.TestPurposeId != excludeId);
        }
    }
} 