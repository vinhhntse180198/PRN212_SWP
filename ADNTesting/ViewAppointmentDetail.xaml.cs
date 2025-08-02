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
    /// Interaction logic for ViewAppointmentDetail.xaml
    /// </summary>
    public partial class ViewAppointmentDetail : Window
    {
        public Appointment selected { get; set; }
        public ViewAppointmentDetail()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtFullName.IsEnabled = false;
            dpDob.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtProvince.IsEnabled = false;
            txtDistrict.IsEnabled = false;
            txtTestPurpose.IsEnabled = false;
            txtServiceType.IsEnabled = false;
            dpAppointmentDate.IsEnabled = false;
            txtCollectionTime.IsEnabled = false;
            txtTestCategory.IsEnabled = false;
            txtCollectionAddress.IsEnabled = false;
            txtKitComponent.IsEnabled = false;
            txtSampleType.IsEnabled = false;
            FillElements();
        }

        public void FillElements()
        {
            txtFullName.Text = selected.FullName;
            dpDob.SelectedDate = selected.Dob.ToDateTime(TimeOnly.MinValue);
            txtPhone.Text = selected.Phone;
            txtEmail.Text = selected.Email;
            txtProvince.Text = selected.Province;
            txtDistrict.Text = selected.District;
            txtTestPurpose.Text = selected.TestPurpose;
            txtServiceType.Text = selected.ServiceType;
            dpAppointmentDate.SelectedDate = selected.AppointmentDate;
            txtCollectionTime.Text = selected.CollectionSampleTime?.ToString() ?? string.Empty;
            txtTestCategory.Text = selected.TestCategory;
            txtCollectionAddress.Text = selected.CollectionLocation;
            txtKitComponent.Text = selected.KitComponent?.ComponentName ?? string.Empty;
            txtSampleType.Text = string.Join(", ",
                                selected.CollectedSamples
                                .Select(s => s.SampleType?.Name)
                                .Where(name => !string.IsNullOrEmpty(name))
                                );
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
