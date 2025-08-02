using System;
using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class TestCategoryService
    {
        private TestCategoryRepository _repository = new();

        public List<TestCategory> GetAllTestCategories()
        {
            return _repository.GetAll();
        }

        public TestCategory? GetTestCategoryById(long id)
        {
            return _repository.GetById(id);
        }

        public void AddTestCategory(TestCategory testCategory)
        {
            if (string.IsNullOrWhiteSpace(testCategory.Name))
            {
                throw new ArgumentException("Tên danh mục xét nghiệm không được để trống");
            }

            if (_repository.IsNameExists(testCategory.Name))
            {
                throw new ArgumentException("Tên danh mục xét nghiệm đã tồn tại");
            }

            _repository.Add(testCategory);
        }

        public void UpdateTestCategory(TestCategory testCategory)
        {
            if (string.IsNullOrWhiteSpace(testCategory.Name))
            {
                throw new ArgumentException("Tên danh mục xét nghiệm không được để trống");
            }

            if (_repository.IsNameExists(testCategory.Name, testCategory.TestCategoryId))
            {
                throw new ArgumentException("Tên danh mục xét nghiệm đã tồn tại");
            }

            _repository.Update(testCategory);
        }

        public void DeleteTestCategory(long id)
        {
            _repository.Delete(id);
        }

        public List<TestCategory> SearchTestCategories(string searchText)
        {
            return _repository.Search(searchText);
        }

        public bool IsNameExists(string name, long excludeId = 0)
        {
            return _repository.IsNameExists(name, excludeId);
        }
    }
} 