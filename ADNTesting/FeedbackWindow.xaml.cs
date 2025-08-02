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
    /// Interaction logic for FeedbackWindow.xaml
    /// </summary>
    public partial class FeedbackWindow : Window
    {
        private FeedbackService feedbackService = new FeedbackService();
        public User userCurrent { get; set; }
        public Service serviceCurrent { get; set; }
        public FeedbackWindow()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (cbxRating.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn số sao đánh giá!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtContent.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung đánh giá!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Feedback feedback = new Feedback();
            feedback.UserId = userCurrent.UserId;
            feedback.ServiceId = serviceCurrent.ServiceId;
            feedback.Content = txtContent.Text;
            feedback.Rating = int.Parse(cbxRating.SelectedItem.ToString());
            feedback.FeedbackDate = DateTime.Now;

            feedbackService.AddFeedback(feedback);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}