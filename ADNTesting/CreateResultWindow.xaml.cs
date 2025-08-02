using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL.Services;
using DAL.Entities;

namespace ADNTesting
{
    public partial class CreateResultWindow : Window
    {
        private AppointmentService _appointmentService = new();
        private ResultService _resultService = new();
        private List<Appointment> _allAppointments = new();
        private Appointment? _selectedAppointment;
        public User UserCurrent { get; set; }

        public CreateResultWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _allAppointments = _appointmentService.GetAppointmentsForCustomer();
                dgAppointments.ItemsSource = _allAppointments;
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
                var filteredList = _allAppointments.Where(a => 
                    a.FullName.ToLower().Contains(searchText) || 
                    a.Phone.Contains(searchText) ||
                    a.Email.ToLower().Contains(searchText)).ToList();
                dgAppointments.ItemsSource = filteredList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCreateResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedAppointment == null)
                {
                    MessageBox.Show("Vui lòng chọn một lịch hẹn để tạo kết quả", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Kiểm tra xem đã có kết quả chưa
                var existingResult = _resultService.GetResultByAppointmentId(_selectedAppointment.AppointmentId);
                if (existingResult != null)
                {
                    MessageBox.Show("Lịch hẹn này đã có kết quả!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ResultFormWindow resultFormWindow = new ResultFormWindow(_selectedAppointment, UserCurrent);
                if (resultFormWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo kết quả: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void dgAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedAppointment = dgAppointments.SelectedItem as Appointment;
            btnCreateResult.IsEnabled = _selectedAppointment != null;
        }
    }
} 