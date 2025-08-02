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
using DAL.Entities;

namespace ADNTesting
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public User userCurrent { get; set; }
        public CustomerWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            ServiceWindow s = new ServiceWindow();
            s.userCurrent = userCurrent;
            s.ShowDialog();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btBooking_Click(object sender, RoutedEventArgs e)
        {
            DetailBookingWindow bookingWindow = new DetailBookingWindow();
            bookingWindow.UserCurrent = userCurrent;
            bookingWindow.ShowDialog();
        }

        private void btnTrackingOrder_Click(object sender, RoutedEventArgs e)
        {
            AppointmentBookedWindow appointmentBookedWindow = new AppointmentBookedWindow();
            appointmentBookedWindow.UserCurrent = userCurrent;
            appointmentBookedWindow.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl.Content = "Xin chào, " + userCurrent.FullName + "!";
        }
    }
}