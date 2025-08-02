using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL.Services;
using DAL.Entities;

namespace ADNTesting
{
    /// <summary>
    /// Interaction logic for ManageKitWindow.xaml
    /// </summary>
    public partial class ManageKitWindow : Window
    {
        private KitService _kitService = new KitService();
        private ADNService _adnService = new ADNService();
        private KitComponent _selectedKit;
        public User UserCurrent { get; set; }

        public ManageKitWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadServices();
            LoadKits();
            ResetSelection();
        }

        private void LoadServices()
        {
            var services = _adnService.GetAllService();
            
            // Thêm một item "Tất cả" vào đầu danh sách lọc
            var allServices = new List<Service>(services);
            allServices.Insert(0, new Service { ServiceId = -1, ServiceName = "Tất cả dịch vụ" });
            cbxServiceFilter.ItemsSource = allServices;
            cbxServiceFilter.SelectedIndex = 0;
        }

        private void LoadKits()
        {
            dgvKits.ItemsSource = _kitService.GetAllKits();
        }

        private void LoadKitsByService(long serviceId)
        {
            if (serviceId == -1) // "Tất cả dịch vụ"
            {
                LoadKits();
            }
            else
            {
                dgvKits.ItemsSource = _kitService.GetKitsByService(serviceId);
            }
        }

        private void ResetSelection()
        {
            _selectedKit = null;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnCreate.IsEnabled = true;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadKits();
                return;
            }

            dgvKits.ItemsSource = _kitService.SearchKits(searchText);
        }

        private void cbxServiceFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxServiceFilter.SelectedItem != null && cbxServiceFilter.SelectedValue is long serviceId)
            {
                LoadKitsByService(serviceId);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            cbxServiceFilter.SelectedIndex = 0;
            LoadKits();
            ResetSelection();
        }

        private void dgvKits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedKit = dgvKits.SelectedItem as KitComponent;
            if (_selectedKit != null)
            {
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnCreate.IsEnabled = true;
            }
            else
            {
                ResetSelection();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            KitDetailWindow detailWindow = new KitDetailWindow();
            detailWindow.IsNewKit = true;
            detailWindow.UserCurrent = UserCurrent;
            
            if (detailWindow.ShowDialog() == true)
            {
                LoadKits();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedKit == null)
            {
                MessageBox.Show("Vui lòng chọn Kit để cập nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            KitDetailWindow detailWindow = new KitDetailWindow();
            detailWindow.IsNewKit = false;
            detailWindow.CurrentKit = _selectedKit;
            detailWindow.UserCurrent = UserCurrent;
            
            if (detailWindow.ShowDialog() == true)
            {
                LoadKits();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedKit == null)
            {
                MessageBox.Show("Vui lòng chọn Kit để xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa Kit này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _kitService.DeleteKit(_selectedKit);
                    MessageBox.Show("Xóa Kit thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadKits();
                    ResetSelection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa Kit: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 