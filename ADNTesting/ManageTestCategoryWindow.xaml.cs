using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL.Services;
using DAL.Entities;

namespace ADNTesting
{
    public partial class ManageTestCategoryWindow : Window
    {
        private TestCategoryService _testCategoryService = new();
        private List<TestCategory> _allTestCategories = new();
        private TestCategory? _selectedTestCategory;

        public ManageTestCategoryWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _allTestCategories = _testCategoryService.GetAllTestCategories();
                dgTestCategories.ItemsSource = _allTestCategories;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text.ToLower();
                var filteredList = _allTestCategories.Where(tc => 
                    tc.Name.ToLower().Contains(searchText)).ToList();
                dgTestCategories.ItemsSource = filteredList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestCategoryFormWindow formWindow = new TestCategoryFormWindow();
                if (formWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm mới: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedTestCategory == null)
                {
                    MessageBox.Show("Vui lòng chọn một danh mục xét nghiệm để sửa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                TestCategoryFormWindow formWindow = new TestCategoryFormWindow(_selectedTestCategory);
                if (formWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedTestCategory == null)
                {
                    MessageBox.Show("Vui lòng chọn một danh mục xét nghiệm để xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa danh mục xét nghiệm '{_selectedTestCategory.Name}'?", 
                    "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    _testCategoryService.DeleteTestCategory(_selectedTestCategory.TestCategoryId);
                    LoadData();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgTestCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedTestCategory = dgTestCategories.SelectedItem as TestCategory;
            btnEdit.IsEnabled = _selectedTestCategory != null;
            btnDelete.IsEnabled = _selectedTestCategory != null;
        }
    }
} 