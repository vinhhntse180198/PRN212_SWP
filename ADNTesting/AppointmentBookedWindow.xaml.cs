using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL.Services;
using DAL.Entities;

namespace ADNTesting
{
    /// <summary>
    /// Interaction logic for AppointmentBookedWindow.xaml
    /// </summary>
    public partial class AppointmentBookedWindow : Window, INotifyPropertyChanged
    {
        private AppointmentService _appointmentService = new();
        public User UserCurrent { get; set; }
        
        private Visibility _staffButtonVisibility = Visibility.Collapsed;
        public Visibility StaffButtonVisibility
        {
            get => _staffButtonVisibility;
            set
            {
                _staffButtonVisibility = value;
                OnPropertyChanged(nameof(StaffButtonVisibility));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public AppointmentBookedWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGird();
            
            // Hiển thị nút cập nhật nếu là Staff hoặc Manager
            if (UserCurrent != null && (UserCurrent.Role.ToLower() == "staff" || UserCurrent.Role.ToLower() == "manager"))
            {
                StaffButtonVisibility = Visibility.Visible;
            }
            else
            {
                StaffButtonVisibility = Visibility.Collapsed;
            }
        }

        public void FillDataGird()
        {
            dgv.ItemsSource = null;
            
            // Nếu người dùng là khách hàng, chỉ hiển thị lịch hẹn của họ
            // Nếu là staff/admin, hiển thị tất cả lịch hẹn
            if (UserCurrent != null && UserCurrent.Role.ToLower() == "customer")
            {
                dgv.ItemsSource = _appointmentService.GetAppointmentsForCustomer()
                    .Where(x => x.UserId == UserCurrent.UserId)
                    .OrderByDescending(x => x.AppointmentId)
                    .ToList();
            }
            else
            {
                // Staff/Admin xem tất cả lịch hẹn
                dgv.ItemsSource = _appointmentService.GetAppointmentsForCustomer()
                    .OrderByDescending(x => x.AppointmentId)
                    .ToList();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgv.ItemsSource = null;
            var searchResults = _appointmentService.GetAppointmentsBySearch(txtSearch.Text);
            
            // Nếu người dùng là khách hàng, chỉ hiển thị lịch hẹn của họ
            if (UserCurrent != null && UserCurrent.Role.ToLower() == "customer")
            {
                dgv.ItemsSource = searchResults
                    .Where(x => x.UserId == UserCurrent.UserId)
                    .OrderByDescending(x => x.AppointmentId)
                    .ToList();
            }
            else
            {
                // Staff/Admin xem tất cả lịch hẹn tìm kiếm được
                dgv.ItemsSource = searchResults
                    .OrderByDescending(x => x.AppointmentId)
                    .ToList();
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            ViewAppointmentDetail view = new();
            Appointment? selected = dgv.SelectedItem as Appointment;
            if(selected == null)
            {
                MessageBox.Show("Hãy chọn một cuộc hẹn để xem chi tiết!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            view.selected = selected;
            view.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Appointment selected = dgv.SelectedItem as Appointment;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn một lịch hẹn để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Mở cửa sổ cập nhật trạng thái và kết quả
            UpdateAppointmentWindow updateWindow = new UpdateAppointmentWindow();
            updateWindow.appointment = selected;
            updateWindow.UserCurrent = UserCurrent;
            updateWindow.ShowDialog();
            
            // Làm mới danh sách sau khi cập nhật
            FillDataGird();
        }
    }
}
