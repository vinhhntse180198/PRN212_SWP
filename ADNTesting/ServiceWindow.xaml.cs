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
    /// Interaction logic for ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public User userCurrent { get; set; }
        private ADNService _adnService = new ADNService();
        public ServiceWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            dgv.ItemsSource = null;
            dgv.ItemsSource = _adnService.GetAllService();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgv.ItemsSource = null;
            dgv.ItemsSource = _adnService.GetBySearch(txtSearch.Text);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            Service? selected = dgv.SelectedItem as Service;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để đặt lịch!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DetailBookingWindow bookingWindow = new DetailBookingWindow();
            bookingWindow.UserCurrent = userCurrent;
            bookingWindow.ServiceCurrent = selected;
            bookingWindow.ShowDialog();
        }

        private void btnComment_Click(object sender, RoutedEventArgs e)
        {
            Service? selected = dgv.SelectedItem as Service;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để đánh giá!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ManageFeedback feedback = new(userCurrent, selected);
            feedback.ShowDialog();
        }
    }
}