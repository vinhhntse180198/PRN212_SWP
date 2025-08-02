using System;
using System.Collections.Generic;
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

namespace ADNTesting
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserService _userService = new();
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckVar())
                return;
            User x = new();
            x.FullName = txtFullName.Text;
            x.Gender = cbxGender.SelectedItem as string;
            x.DateOfBirth = dpDateOfBirth.SelectedDate.HasValue ? DateOnly.FromDateTime(dpDateOfBirth.SelectedDate.Value) : null;
            x.Email = txtEmail.Text;
            x.Phone = txtPhone.Text;
            x.Address = txtAddress.Text;
            x.Username = txtUsername.Text;
            x.Password = txtPassword.Text;

            _userService.AddUser(x);
            this.Close();
        }

        public bool CheckVar()
        {
            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtFullName.Focus();
                return false;
            }

            // Kiểm tra giới tính
            if (cbxGender.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxGender.Focus();
                return false;
            }

            // Kiểm tra ngày sinh
            if (!dpDateOfBirth.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpDateOfBirth.Focus();
                return false;
            }
            else if (dpDateOfBirth.SelectedDate.Value > DateTime.Today)
            {
                MessageBox.Show("Ngày sinh không được ở tương lai.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpDateOfBirth.Focus();
                return false;
            }

            // Kiểm tra email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }
            else if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Email không hợp lệ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }

            if (_userService.CheckEmailExist(txtEmail.Text))
            {
                MessageBox.Show("Email đã tồn tại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }

            // Kiểm tra số điện thoại
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPhone.Focus();
                return false;
            }
            else if (!txtPhone.Text.All(char.IsDigit) || txtPhone.Text.Length < 8)
            {
                MessageBox.Show("Số điện thoại không hợp lệ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPhone.Focus();
                return false;
            }

            // Kiểm tra tên đăng nhập
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUsername.Focus();
                return false;
            }

            if (_userService.CheckUsernameExist(txtUsername.Text))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUsername.Focus();
                return false;
            }

            // Kiểm tra mật khẩu
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void FillComboBox()
        {
            cbxGender.ItemsSource = new List<String> { "Nam", "Nữ", "Khác" };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
        }
    }
}