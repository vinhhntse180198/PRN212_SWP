using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class SampleTypeRepository
    {
        private DnaTestingContext _context;

        public SampleTypeRepository()
        {
            _context = new DnaTestingContext();
        }

        public List<SampleType> GetAll()
        {
            _context = new();
            return _context.SampleTypes
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(st => st.KitComponent)
                .OrderBy(st => st.Id)
                .ToList();
        }

        public SampleType? GetById(long id)
        {
            _context = new();
            return _context.SampleTypes
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(s => s.KitComponent)
                .FirstOrDefault(s => s.Id == id);
        }

        public List<SampleType> GetByKitComponentId(long kitComponentId)
        {
            _context = new();
            return _context.SampleTypes
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(s => s.KitComponent)
                .Where(s => s.KitComponentId == kitComponentId)
                .ToList();
        }

        public void Add(SampleType sampleType)
        {
            sampleType.IsActive = true;
            _context.SampleTypes.Add(sampleType);
            _context.SaveChanges();
        }

        public void Update(SampleType sampleType)
        {
            _context.SampleTypes.Update(sampleType);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var sampleType = _context.SampleTypes.Find(id);
            if (sampleType != null)
            {
                sampleType.IsActive = false;
                _context.SaveChanges();
            }
        }

        public List<SampleType> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return GetAll();
            }

            return _context.SampleTypes
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(s => s.KitComponent)
                .Where(s => (s.Name != null && s.Name.ToLower().Contains(searchText.ToLower())) || 
                       (s.Description != null && s.Description.ToLower().Contains(searchText.ToLower())))
                .ToList();
        }
    }
} 