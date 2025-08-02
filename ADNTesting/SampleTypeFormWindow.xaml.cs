using System;
using System.Collections.Generic;
using System.Windows;
using BLL.Services;
using DAL.Entities;

namespace ADNTesting
{
    public partial class SampleTypeFormWindow : Window
    {
        private SampleTypeService _sampleTypeService = new();
        private KitService _kitService = new();
        private SampleType? _sampleType;
        private bool _isEditMode = false;

        public SampleTypeFormWindow()
        {
            InitializeComponent();
            LoadKitComponents();
        }

        public SampleTypeFormWindow(SampleType sampleType)
        {
            InitializeComponent();
            _sampleType = sampleType;
            _isEditMode = true;
            LoadKitComponents();
            LoadData();
        }

        private void LoadKitComponents()
        {
            try
            {
                var kitComponents = _kitService.GetAllKits();
                cbxKitComponent.ItemsSource = kitComponents;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách kit components: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData()
        {
            if (_sampleType != null)
            {
                txtTitle.Text = "Sửa Loại Mẫu";
                txtName.Text = _sampleType.Name;
                txtDescription.Text = _sampleType.Description;
                cbxKitComponent.SelectedValue = _sampleType.KitComponentId;
                chkIsActive.IsChecked = _sampleType.IsActive;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên loại mẫu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtName.Focus();
                    return;
                }

                if (cbxKitComponent.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn kit component", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    cbxKitComponent.Focus();
                    return;
                }

                // Create or update SampleType
                if (_isEditMode && _sampleType != null)
                {
                    _sampleType.Name = txtName.Text.Trim();
                    _sampleType.Description = txtDescription.Text.Trim();
                    _sampleType.KitComponentId = (long)cbxKitComponent.SelectedValue;
                    _sampleType.IsActive = chkIsActive.IsChecked ?? true;
                    
                    _sampleTypeService.UpdateSampleType(_sampleType);
                }
                else
                {
                    var newSampleType = new SampleType
                    {
                        Name = txtName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        KitComponentId = (long)cbxKitComponent.SelectedValue,
                        IsActive = chkIsActive.IsChecked ?? true
                    };
                    
                    _sampleTypeService.AddSampleType(newSampleType);
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