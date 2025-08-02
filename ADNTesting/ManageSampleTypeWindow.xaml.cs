using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL.Services;
using DAL.Entities;

namespace ADNTesting
{
    public partial class ManageSampleTypeWindow : Window
    {
        private SampleTypeService _sampleTypeService = new();
        private KitService _kitService = new();
        private List<SampleType> _allSampleTypes = new();
        private SampleType? _selectedSampleType;

        public ManageSampleTypeWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _allSampleTypes = _sampleTypeService.GetAllSampleTypes();
                dgSampleTypes.ItemsSource = _allSampleTypes;
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
                var filteredList = _allSampleTypes.Where(st => 
                    st.Name.ToLower().Contains(searchText) || 
                    st.Description.ToLower().Contains(searchText)).ToList();
                dgSampleTypes.ItemsSource = filteredList;
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
                SampleTypeFormWindow formWindow = new SampleTypeFormWindow();
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
                if (_selectedSampleType == null)
                {
                    MessageBox.Show("Vui lòng chọn một loại mẫu để sửa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SampleTypeFormWindow formWindow = new SampleTypeFormWindow(_selectedSampleType);
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
                if (_selectedSampleType == null)
                {
                    MessageBox.Show("Vui lòng chọn một loại mẫu để xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa loại mẫu '{_selectedSampleType.Name}'?", 
                    "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    _sampleTypeService.DeleteSampleType(_selectedSampleType.Id);
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

        private void dgSampleTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedSampleType = dgSampleTypes.SelectedItem as SampleType;
            btnEdit.IsEnabled = _selectedSampleType != null;
            btnDelete.IsEnabled = _selectedSampleType != null;
        }
    }
} 