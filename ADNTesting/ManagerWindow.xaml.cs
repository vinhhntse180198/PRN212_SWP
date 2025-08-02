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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public User UserCurrent { get; set; }

        public ManagerWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (UserCurrent != null)
            {
                txtWelcome.Text = $"Xin chào, {UserCurrent.FullName}";
            }
        }

        private void btnManageServices_Click(object sender, RoutedEventArgs e)
        {
            ManageServiceWindow manageServiceWindow = new ManageServiceWindow();
            manageServiceWindow.ShowDialog();
        }

        private void btnViewAppointments_Click(object sender, RoutedEventArgs e)
        {
            AppointmentBookedWindow appointmentWindow = new AppointmentBookedWindow();
            appointmentWindow.UserCurrent = UserCurrent;
            appointmentWindow.ShowDialog();
        }

        private void btnManageSampleTypes_Click(object sender, RoutedEventArgs e)
        {
            ManageSampleTypeWindow manageSampleTypeWindow = new ManageSampleTypeWindow();
            manageSampleTypeWindow.ShowDialog();
        }

        private void btnManageTestPurposes_Click(object sender, RoutedEventArgs e)
        {
            ManageTestPurposeWindow manageTestPurposeWindow = new ManageTestPurposeWindow();
            manageTestPurposeWindow.ShowDialog();
        }

        private void btnManageTestCategories_Click(object sender, RoutedEventArgs e)
        {
            ManageTestCategoryWindow manageTestCategoryWindow = new ManageTestCategoryWindow();
            manageTestCategoryWindow.ShowDialog();
        }

        private void btnManageKits_Click(object sender, RoutedEventArgs e)
        {
            ManageKitWindow manageKitWindow = new ManageKitWindow();
            manageKitWindow.UserCurrent = UserCurrent;
            manageKitWindow.ShowDialog();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnViewReports_Click(object sender, RoutedEventArgs e)
        {
            ManageReportWindow manageReportWindow = new ManageReportWindow();
            manageReportWindow.userCurrent = UserCurrent;
            manageReportWindow.ShowDialog();
        }
    }
} 