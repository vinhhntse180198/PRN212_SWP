using System;
using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class SampleTypeService
    {
        private SampleTypeRepository _repository = new();

        public List<SampleType> GetAllSampleTypes()
        {
            return _repository.GetAll();
        }

        public SampleType? GetSampleTypeById(long id)
        {
            return _repository.GetById(id);
        }

        public List<SampleType> GetSampleTypesByKitComponent(long kitComponentId)
        {
            return _repository.GetByKitComponentId(kitComponentId);
        }

        public void AddSampleType(SampleType sampleType)
        {
            if (string.IsNullOrWhiteSpace(sampleType.Name))
            {
                throw new ArgumentException("Tên loại mẫu không được để trống");
            }

            if (sampleType.KitComponentId <= 0)
            {
                throw new ArgumentException("Phải chọn kit component");
            }

            _repository.Add(sampleType);
        }

        public void UpdateSampleType(SampleType sampleType)
        {
            if (string.IsNullOrWhiteSpace(sampleType.Name))
            {
                throw new ArgumentException("Tên loại mẫu không được để trống");
            }

            if (sampleType.KitComponentId <= 0)
            {
                throw new ArgumentException("Phải chọn kit component");
            }

            _repository.Update(sampleType);
        }

        public void DeleteSampleType(long id)
        {
            _repository.Delete(id);
        }

        public List<SampleType> SearchSampleTypes(string searchText)
        {
            return _repository.Search(searchText);
        }
    }
} 