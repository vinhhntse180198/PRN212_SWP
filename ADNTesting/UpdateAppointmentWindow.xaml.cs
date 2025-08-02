using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL.Services;
using DAL.Entities;
using Microsoft.Win32;

namespace ADNTesting
{
    /// <summary>
    /// Interaction logic for UpdateAppointmentWindow.xaml
    /// </summary>
    public partial class UpdateAppointmentWindow : Window
    {
        private AppointmentService _appointmentService = new();
        private ResultService _resultService = new ResultService();
        public Appointment appointment { get; set; }
        public User UserCurrent { get; set; }

        public UpdateAppointmentWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAppointmentData();
            LoadResultData();
        }

        private void LoadAppointmentData()
        {
            if (appointment != null)
            {
                txtAppointmentId.Text = appointment.AppointmentId.ToString();
                txtFullName.Text = appointment.FullName;
                txtEmail.Text = appointment.Email;
                txtPhone.Text = appointment.Phone;
                txtService.Text = appointment.ServiceType;
                txtAppointmentDate.Text = appointment.AppointmentDate.ToString("dd/MM/yyyy HH:mm");
                txtLocation.Text = appointment.CollectionLocation;

                // Chọn trạng thái hiện tại trong combobox
                if (!string.IsNullOrEmpty(appointment.Status))
                {
                    foreach (ComboBoxItem item in cbxStatus.Items)
                    {
                        if (item.Content.ToString() == appointment.Status)
                        {
                            cbxStatus.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        private void LoadResultData()
        {
            if (appointment != null)
            {
                Result result = _resultService.GetResultByAppointmentId(appointment.AppointmentId);
                if (result != null)
                {
                    txtResultFile.Text = result.ResultFile;
                    txtResultData.Text = result.ResultData;
                }
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            
            if (openFileDialog.ShowDialog() == true)
            {
                txtResultFile.Text = System.IO.Path.GetFileName(openFileDialog.FileName);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Cập nhật trạng thái lịch hẹn
                if (cbxStatus.SelectedItem != null)
                {
                    string newStatus = ((ComboBoxItem)cbxStatus.SelectedItem).Content.ToString();
                    appointment.Status = newStatus;
                    _appointmentService.UpdateAppointment(appointment);
                }

                // Cập nhật hoặc tạo mới kết quả
                Result result = _resultService.GetResultByAppointmentId(appointment.AppointmentId);
                bool isNewResult = false;

                if (result == null)
                {
                    result = new Result();
                    result.AppointmentId = appointment.AppointmentId;
                    result.UserId = UserCurrent.UserId;
                    result.ResultDate = DateOnly.FromDateTime(DateTime.Now);
                    isNewResult = true;
                }

                result.ResultFile = txtResultFile.Text;
                result.ResultData = txtResultData.Text;
                result.Status = appointment.Status;

                if (isNewResult)
                {
                    _resultService.AddResult(result);
                }
                else
                {
                    _resultService.UpdateResult(result);
                }

                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 