using System;
using System.Windows;
using BLL.Services;
using DAL.Entities;

namespace ADNTesting
{
    public partial class TestPurposeFormWindow : Window
    {
        private TestPurposeService _testPurposeService = new();
        private TestPurpose? _testPurpose;
        private bool _isEditMode = false;

        public TestPurposeFormWindow()
        {
            InitializeComponent();
        }

        public TestPurposeFormWindow(TestPurpose testPurpose)
        {
            InitializeComponent();
            _testPurpose = testPurpose;
            _isEditMode = true;
            LoadData();
        }

        private void LoadData()
        {
            if (_testPurpose != null)
            {
                txtTitle.Text = "Sửa Mục đích Xét nghiệm";
                txtTestPurposeName.Text = _testPurpose.TestPurposeName;
                txtTestPurposeDescription.Text = _testPurpose.TestPurposeDescription;
                chkIsActive.IsChecked = _testPurpose.IsActive;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtTestPurposeName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên mục đích xét nghiệm", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtTestPurposeName.Focus();
                    return;
                }

                // Create or update TestPurpose
                if (_isEditMode && _testPurpose != null)
                {
                    _testPurpose.TestPurposeName = txtTestPurposeName.Text.Trim();
                    _testPurpose.TestPurposeDescription = txtTestPurposeDescription.Text.Trim();
                    _testPurpose.IsActive = chkIsActive.IsChecked ?? true;
                    
                    _testPurposeService.UpdateTestPurpose(_testPurpose);
                }
                else
                {
                    var newTestPurpose = new TestPurpose
                    {
                        TestPurposeName = txtTestPurposeName.Text.Trim(),
                        TestPurposeDescription = txtTestPurposeDescription.Text.Trim(),
                        IsActive = chkIsActive.IsChecked ?? true
                    };
                    
                    _testPurposeService.AddTestPurpose(newTestPurpose);
                }

                MessageBox.Show(_isEditMode ? "Cập nhật thành công!" : "Thêm mới thành công!", 
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
} 