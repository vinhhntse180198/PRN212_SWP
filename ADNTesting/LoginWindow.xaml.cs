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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserService _userService = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
{
    User? user = _userService.CheckAuthen(txtUsername.Text, pbPassword.Password);
    if (user == null)
    {
        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }

    // Phân quyền theo Role
    switch (user.Role?.ToLower())
    {
        case "customer":
            CustomerWindow customerWindow = new();
                            customerWindow.userCurrent = user;
            customerWindow.Show();
            break;

        case "staff":
            StaffWindow staffWindow = new();
                            staffWindow.UserCurrent = user;
            staffWindow.Show();
            break;

        case "manager":
            ManagerWindow managerWindow = new();
                            managerWindow.UserCurrent = user;
            managerWindow.Show();
            break;

        default:
            MessageBox.Show("Không xác định được vai trò người dùng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
    }

    this.Hide();
}


        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
