using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using DAL.Entities;
using BLL.Services;

namespace ADNTesting
{
    public partial class ResultFormWindow : Window
    {
        private Appointment _appointment;
        private User _user;
        private ResultService _resultService = new ResultService();
        private string _selectedFilePath = "";

        public ResultFormWindow(Appointment appointment, User user)
        {
            InitializeComponent();
            _appointment = appointment;
            _user = user;
            LoadAppointmentInfo();
            dpResultDate.SelectedDate = DateTime.Now;
        }

        private void LoadAppointmentInfo()
        {
            if (_appointment != null)
            {
                txtAppointmentInfo.Text = $"ID: {_appointment.AppointmentId}\n" +
                                         $"Họ tên: {_appointment.FullName}\n" +
                                         $"SĐT: {_appointment.Phone}\n" +
                                         $"Email: {_appointment.Email}\n" +
                                         $"Ngày hẹn: {_appointment.AppointmentDate:dd/MM/yyyy}\n" +
                                         $"Dịch vụ: {_appointment.Service?.ServiceName}";
            }
        }

        private void btnBrowseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Chọn file kết quả";

            if (openFileDialog.ShowDialog() == true)
            {
                _selectedFilePath = openFileDialog.FileName;
                txtResultFile.Text = Path.GetFileName(_selectedFilePath);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtResultData.Text))
                {
                    MessageBox.Show("Vui lòng nhập dữ liệu kết quả!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (dpResultDate.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng chọn ngày kết quả!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Result result = new Result
                {
                    AppointmentId = _appointment.AppointmentId,
                    UserId = _user.UserId,
                    ResultDate = DateOnly.FromDateTime(dpResultDate.SelectedDate.Value),
                    ResultData = txtResultData.Text,
                    ResultFile = _selectedFilePath,
                    Note = txtNote.Text,
                    Status = "Completed"
                };

                _resultService.AddResult(result);
                MessageBox.Show("Kết quả đã được tạo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo kết quả: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
} 