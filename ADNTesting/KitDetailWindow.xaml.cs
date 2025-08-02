using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BLL.Services;
using DAL.Entities;

namespace ADNTesting
{
    /// <summary>
    /// Interaction logic for KitDetailWindow.xaml
    /// </summary>
    public partial class KitDetailWindow : Window
    {
        private KitService _kitService = new KitService();
        private ADNService _adnService = new ADNService();
        
        public KitComponent CurrentKit { get; set; }
        public bool IsNewKit { get; set; }
        public User UserCurrent { get; set; }

        public KitDetailWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadServices();
            
            if (IsNewKit)
            {
                txtTitle.Text = "Thêm mới Kit xét nghiệm";
                txtKitId.Text = "(Tự động)";
                txtQuantity.Text = "1";
            }
            else
            {
                txtTitle.Text = "Cập nhật Kit xét nghiệm";
                LoadKitDetails();
            }
        }

        private void LoadServices()
        {
            var services = _adnService.GetAllService();
            cbxService.ItemsSource = services;
        }

        private void LoadKitDetails()
        {
            if (CurrentKit != null)
            {
                txtKitId.Text = CurrentKit.KitComponentId.ToString();
                txtKitName.Text = CurrentKit.ComponentName;
                txtDescription.Text = CurrentKit.Introduction;
                txtQuantity.Text = CurrentKit.Quantity.ToString();
                
                // Chọn dịch vụ tương ứng
                var services = cbxService.ItemsSource as IEnumerable<Service>;
                if (services != null)
                {
                    var service = services.FirstOrDefault(s => s.ServiceId == CurrentKit.ServiceId);
                    cbxService.SelectedItem = service;
                }
                
                // Hiển thị danh sách loại mẫu
                if (CurrentKit.SampleTypes != null && CurrentKit.SampleTypes.Any())
                {
                    lbSampleTypes.ItemsSource = CurrentKit.SampleTypes;
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtKitName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên Kit", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtKitName.Focus();
                return false;
            }

            if (cbxService.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxService.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtQuantity.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                if (IsNewKit)
                {
                    // Tạo mới Kit
                    var newKit = new KitComponent
                    {
                        ComponentName = txtKitName.Text.Trim(),
                        Introduction = txtDescription.Text.Trim(),
                        Quantity = int.Parse(txtQuantity.Text),
                        ServiceId = (long)cbxService.SelectedValue
                    };

                    _kitService.CreateKit(newKit);
                    MessageBox.Show("Tạo Kit thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Cập nhật Kit hiện có
                    CurrentKit.ComponentName = txtKitName.Text.Trim();
                    CurrentKit.Introduction = txtDescription.Text.Trim();
                    CurrentKit.Quantity = int.Parse(txtQuantity.Text);
                    CurrentKit.ServiceId = (long)cbxService.SelectedValue;

                    _kitService.UpdateKit(CurrentKit);
                    MessageBox.Show("Cập nhật Kit thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
} 