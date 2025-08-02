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
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        public User UserCurrent { get; set; }

        public StaffWindow()
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

        private void btnAppointments_Click(object sender, RoutedEventArgs e)
        {
            AppointmentBookedWindow appointmentWindow = new AppointmentBookedWindow();
            appointmentWindow.UserCurrent = UserCurrent;
            appointmentWindow.ShowDialog();
        }
        
        private void btnCreateResults_Click(object sender, RoutedEventArgs e)
        {
            CreateResultWindow createResultWindow = new CreateResultWindow();
            createResultWindow.UserCurrent = UserCurrent;
            createResultWindow.ShowDialog();
        }

        private void btnKits_Click(object sender, RoutedEventArgs e)
        {
            ManageKitWindow kitWindow = new ManageKitWindow();
            kitWindow.UserCurrent = UserCurrent;
            kitWindow.ShowDialog();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
} 