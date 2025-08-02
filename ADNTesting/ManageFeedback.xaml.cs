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
    /// Interaction logic for ManageFeedback.xaml
    /// </summary>
    public partial class ManageFeedback : Window
    {
        private FeedbackService _feedbackService = new FeedbackService();
        private ADNService _adnService = new ADNService();
        public User userCurrent { get; set; }
        public Service serviceCurrent { get; set; }

        public ManageFeedback(User user, Service service)
        {
            InitializeComponent();
            userCurrent = user;
            serviceCurrent = service;
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            dgvFeedback.ItemsSource = null;
            if (serviceCurrent != null)
                dgvFeedback.ItemsSource = _feedbackService.GetAllFeedbacksByService(serviceCurrent.ServiceId);
        }

        private void btnCreateFeedback_Click(object sender, RoutedEventArgs e)
        {
            FeedbackWindow feedback = new();
            feedback.userCurrent = userCurrent;
            feedback.serviceCurrent = serviceCurrent;
            feedback.ShowDialog();
            FillDataGrid();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}