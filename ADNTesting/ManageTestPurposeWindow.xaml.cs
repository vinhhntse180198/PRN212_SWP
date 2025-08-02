using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL.Services;
using DAL.Entities;

namespace ADNTesting
{
    public partial class ManageTestPurposeWindow : Window
    {
        private TestPurposeService _testPurposeService = new();
        private List<TestPurpose> _allTestPurposes = new();
        private TestPurpose? _selectedTestPurpose;

        public ManageTestPurposeWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _allTestPurposes = _testPurposeService.GetAllTestPurposes();
                dgTestPurposes.ItemsSource = _allTestPurposes;
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
                var filteredList = _allTestPurposes.Where(tp => 
                    tp.TestPurposeName.ToLower().Contains(searchText) || 
                    tp.TestPurposeDescription.ToLower().Contains(searchText)).ToList();
                dgTestPurposes.ItemsSource = filteredList;
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
                TestPurposeFormWindow formWindow = new TestPurposeFormWindow();
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
                if (_selectedTestPurpose == null)
                {
                    MessageBox.Show("Vui lòng chọn một mục đích xét nghiệm để sửa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                TestPurposeFormWindow formWindow = new TestPurposeFormWindow(_selectedTestPurpose);
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
                if (_selectedTestPurpose == null)
                {
                    MessageBox.Show("Vui lòng chọn một mục đích xét nghiệm để xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa mục đích xét nghiệm '{_selectedTestPurpose.TestPurposeName}'?", 
                    "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    _testPurposeService.DeleteTestPurpose(_selectedTestPurpose.TestPurposeId);
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

        private void dgTestPurposes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedTestPurpose = dgTestPurposes.SelectedItem as TestPurpose;
            btnEdit.IsEnabled = _selectedTestPurpose != null;
            btnDelete.IsEnabled = _selectedTestPurpose != null;
        }
    }
} 