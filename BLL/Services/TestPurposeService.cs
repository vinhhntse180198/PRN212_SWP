using System;
using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class TestPurposeService
    {
        private TestPurposeRepository _repository = new();

        public List<TestPurpose> GetAllTestPurposes()
        {
            return _repository.GetAll();
        }

        public TestPurpose? GetTestPurposeById(long id)
        {
            return _repository.GetById(id);
        }

        public void AddTestPurpose(TestPurpose testPurpose)
        {
            if (string.IsNullOrWhiteSpace(testPurpose.TestPurposeName))
            {
                throw new ArgumentException("Tên mục đích xét nghiệm không được để trống");
            }

            if (_repository.IsNameExists(testPurpose.TestPurposeName))
            {
                throw new ArgumentException("Tên mục đích xét nghiệm đã tồn tại");
            }

            _repository.Add(testPurpose);
        }

        public void UpdateTestPurpose(TestPurpose testPurpose)
        {
            if (string.IsNullOrWhiteSpace(testPurpose.TestPurposeName))
            {
                throw new ArgumentException("Tên mục đích xét nghiệm không được để trống");
            }

            if (_repository.IsNameExists(testPurpose.TestPurposeName, testPurpose.TestPurposeId))
            {
                throw new ArgumentException("Tên mục đích xét nghiệm đã tồn tại");
            }

            _repository.Update(testPurpose);
        }

        public void DeleteTestPurpose(long id)
        {
            var testPurpose = _repository.GetById(id);
            if (testPurpose != null)
            {
                _repository.Delete(testPurpose);
            }
        }

        public List<TestPurpose> SearchTestPurposes(string searchText)
        {
            return _repository.Search(searchText);
        }

        public bool IsNameExists(string name, long excludeId = 0)
        {
            return _repository.IsNameExists(name, excludeId);
        }
    }
} 