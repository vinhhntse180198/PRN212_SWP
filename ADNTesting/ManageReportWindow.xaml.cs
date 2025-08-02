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
    /// Interaction logic for ManageReportWindow.xaml
    /// </summary>
    public partial class ManageReportWindow : Window
    {
        public User userCurrent { get; set; }
        private List<ReportModel> _allReports = new(); // Lưu toàn bộ dữ liệu

        public ManageReportWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var service = new ReportService();
            _allReports = service.GetAllReports(); // Lấy tất cả báo cáo
            LoadData(_allReports);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            DateTime? start = StartDatePicker.SelectedDate;
            DateTime? end = EndDatePicker.SelectedDate;

            var filtered = _allReports.AsEnumerable();

            if (start.HasValue)
                filtered = filtered.Where(r => r.SampleReceivedDate >= start.Value);

            if (end.HasValue)
                filtered = filtered.Where(r => r.SampleReceivedDate < end.Value.AddDays(1)); // Lọc đến hết ngày

            LoadData(filtered.ToList());
        }

        private void LoadData(List<ReportModel> reports)
        {
            // Sắp xếp giảm dần theo ngày hẹn
            var sortedReports = reports.OrderByDescending(r => r.SampleReceivedDate).ToList();

            ReportDataGrid.ItemsSource = sortedReports;

            // Tổng số xét nghiệm
            TotalTestCountTextBox.Text = sortedReports.Count.ToString();

            // Tổng doanh thu
            TotalRevenueTextBox.Text = sortedReports.Sum(r => r.TotalPrice).ToString("N0");

            // Tổng số khách hàng duy nhất
            TotalCustomerCountTextBox.Text = sortedReports.Select(r => r.CustomerName).Distinct().Count().ToString();

            // Tổng số mẫu
            TotalSampleCountTextBox.Text = sortedReports.Sum(r => r.SampleCount).ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}