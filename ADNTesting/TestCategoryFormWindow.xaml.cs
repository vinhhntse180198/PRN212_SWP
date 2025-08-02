using System;
using System.Windows;
using BLL.Services;
using DAL.Entities;

namespace ADNTesting
{
    public partial class TestCategoryFormWindow : Window
    {
        private TestCategoryService _testCategoryService = new();
        private TestCategory? _testCategory;
        private bool _isEditMode = false;

        public TestCategoryFormWindow()
        {
            InitializeComponent();
        }

        public TestCategoryFormWindow(TestCategory testCategory)
        {
            InitializeComponent();
            _testCategory = testCategory;
            _isEditMode = true;
            LoadData();
        }

        private void LoadData()
        {
            if (_testCategory != null)
            {
                txtTitle.Text = "Sửa Danh mục Xét nghiệm";
                txtTestCategoryName.Text = _testCategory.Name;
                chkIsActive.IsChecked = _testCategory.IsActive;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtTestCategoryName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên danh mục xét nghiệm", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtTestCategoryName.Focus();
                    return;
                }

                // Create or update TestCategory
                if (_isEditMode && _testCategory != null)
                {
                    _testCategory.Name = txtTestCategoryName.Text.Trim();
                    _testCategory.IsActive = chkIsActive.IsChecked ?? true;
                    
                    _testCategoryService.UpdateTestCategory(_testCategory);
                }
                else
                {
                    var newTestCategory = new TestCategory
                    {
                        Name = txtTestCategoryName.Text.Trim(),
                        IsActive = chkIsActive.IsChecked ?? true
                    };
                    
                    _testCategoryService.AddTestCategory(newTestCategory);
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