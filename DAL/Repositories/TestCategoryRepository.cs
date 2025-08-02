using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TestCategoryRepository
    {
        private DnaTestingContext _context;

        public TestCategoryRepository()
        {
            _context = new DnaTestingContext();
        }

        public List<TestCategory> GetAll()
        {
            _context = new();
            return _context.TestCategories
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(tc => tc.Service)
                .ToList();
        }

        public TestCategory? GetById(long id)
        {
            return _context.TestCategories
                .Include(tc => tc.Service)
                .FirstOrDefault(tc => tc.TestCategoryId == id && tc.IsActive == true);
        }

        public void Add(TestCategory testCategory)
        {
            testCategory.IsActive = true;
            _context.TestCategories.Add(testCategory);
            _context.SaveChanges();
        }

        public void Update(TestCategory testCategory)
        {
            _context.TestCategories.Update(testCategory);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var testCategory = _context.TestCategories.Find(id);
            if (testCategory != null)
            {
                testCategory.IsActive = false;
                _context.SaveChanges();
            }
        }

        public List<TestCategory> Search(string searchText)
        {
            return _context.TestCategories
                .Where(tc => tc.IsActive == true && 
                            tc.Name.Contains(searchText))
                .ToList();
        }

        public bool IsNameExists(string name, long excludeId = 0)
        {
            return _context.TestCategories
                .Any(tc => tc.Name.ToLower() == name.ToLower() && 
                          tc.TestCategoryId != excludeId && 
                          tc.IsActive == true);
        }
    }
} 