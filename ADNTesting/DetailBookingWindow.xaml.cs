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
using System.Xaml;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories;

namespace ADNTesting
{
    /// <summary>
    /// Interaction logic for DetailBookingWindow.xaml
    /// </summary>
    public partial class DetailBookingWindow : Window
    {
        private AppointmentService _appointmentService = new AppointmentService();
        private ADNService _adnService = new ADNService();
        private ServiceRepository serviceRepository = new ServiceRepository();
        public Service ServiceCurrent { get; set; }
        public User UserCurrent { get; set; }

        private List<Service> allServices;
        public DetailBookingWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBoxServiceType();
            FillElements();
            FillComboBoxTestPurpose();
            FillComboBoxProvince();
            FillComboBoxDistrict();
            FillComboBoxTestCategory();
            FillComboBoxCollectionAddress();
            FillComboBoxKitComponent();
        }

        public void FillComboBoxSampleType()
        {
            var selectedKit = cbxKitComponent.SelectedItem as KitComponent;
            if (selectedKit != null)
            {
                cbxSampleType.ItemsSource = selectedKit.SampleTypes.ToList();
                cbxSampleType.DisplayMemberPath = "Name";
                cbxSampleType.SelectedValuePath = "Id"; // Use "Id" as per your SampleType class
            }
            else
            {
                cbxSampleType.ItemsSource = null;
            }
        }

        public void FillComboBoxKitComponent()
        {
            var selectedService = cbxServiceType.SelectedItem as Service;
            if (selectedService != null)
            {
                cbxKitComponent.ItemsSource = selectedService.KitComponents.ToList();
                cbxKitComponent.DisplayMemberPath = "ComponentName";
                cbxKitComponent.SelectedValuePath = "KitComponentId";
            }
            else
            {
                cbxKitComponent.ItemsSource = null;
            }
        }

        public void FillComboBoxCollectionAddress()
        {
            var collectionAddresses = new List<string> { "Tại nhà", "Tại cơ sở y tế"};
            cbxTestAddress.ItemsSource = collectionAddresses;
        }

        public void FillComboBoxTestCategory()
        {
            var selectedService = cbxServiceType.SelectedItem as Service;
            if (selectedService != null)
            {
                var testCategories = selectedService.TestCategories
                    .Where(tc => tc.IsActive == true)
                    .Select(tc => tc.Name)
                    .ToList();
                cbxTestCategory.ItemsSource = testCategories;
            }
            else
            {
                cbxTestCategory.ItemsSource = null;
            }
        }

        public void FillComboBoxDistrict()
        {
            var districts = new List<string> {
        "Quận 1",
        "Quận 2",
        "Quận 3",
        "Quận 4",
        "Quận 5",
        "Quận 6",
        "Quận 7",
        "Quận 8",
        "Quận 9",
        "Quận 10",
        "Quận 11",
        "Quận 12",
        "Bình Thạnh",
        "Gò Vấp",
        "Phú Nhuận",
        "Tân Bình",
        "Tân Phú",
        "Thủ Đức"
    };
            cbxDistrict.ItemsSource = districts;
        }

        public void FillComboBoxProvince()
        {
            var provinces = new List<string> {"An Giang", "Bà Rịa - Vũng Tàu",
        "Bắc Giang",
        "Bắc Kạn",
        "Bạc Liêu",
        "Bắc Ninh",
        "Bến Tre",
        "Bình Định",
        "Bình Dương",
        "Bình Phước",
        "Bình Thuận",
        "Cà Mau",
        "Cần Thơ",
        "Cao Bằng",
        "Đà Nẵng",
        "Đắk Lắk",
        "Đắk Nông",
        "Điện Biên",
        "Đồng Nai",
        "Đồng Tháp",
        "Gia Lai",
        "Hà Giang",
        "Hà Nam",
        "Hà Nội",
        "Hà Tĩnh",
        "Hải Dương",
        "Hải Phòng",
        "Hậu Giang",
        "Hòa Bình",
        "Hưng Yên",
        "Khánh Hòa",
        "Kiên Giang",
        "Kon Tum",
        "Lai Châu",
        "Lâm Đồng",
        "Lạng Sơn",
        "Lào Cai",
        "Long An",
        "Nam Định",
        "Nghệ An",
        "Ninh Bình",
        "Ninh Thuận",
        "Phú Thọ",
        "Phú Yên",
        "Quảng Bình",
        "Quảng Nam",
        "Quảng Ngãi",
        "Quảng Ninh",
        "Quảng Trị",
        "Sóc Trăng",
        "Sơn La",
        "Tây Ninh",
        "Thái Bình",
        "Thái Nguyên",
        "Thanh Hóa",
        "Thừa Thiên Huế",
        "Tiền Giang",
        "TP Hồ Chí Minh",
        "Trà Vinh",
        "Tuyên Quang",
        "Vĩnh Long",
        "Vĩnh Phúc",
        "Yên Bái" };
            cbxProvince.ItemsSource = provinces;
        }

        public void FillComboBoxServiceType()
        {
            allServices = serviceRepository.GetAll();
            cbxServiceType.ItemsSource = null;
            cbxServiceType.ItemsSource = allServices;
            cbxServiceType.DisplayMemberPath = "ServiceName";
            cbxServiceType.SelectedValuePath = "ServiceId";
        }

        public void FillElements()
        {
            if (UserCurrent != null)
            {
                txtFullName.Text = UserCurrent.FullName;
                dpDob.SelectedDate = UserCurrent.DateOfBirth?.ToDateTime(TimeOnly.MinValue);
                txtPhone.Text = UserCurrent.Phone;
                txtEmail.Text = UserCurrent.Email;
                cbxDistrict.Text = UserCurrent.Address?.ToString();
            }

            if (ServiceCurrent != null)
            {
                cbxServiceType.SelectedItem = allServices.FirstOrDefault(s => s.ServiceId == ServiceCurrent.ServiceId);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnBooking_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckVar())
                return;
            Appointment x = new();
            x.FullName = txtFullName.Text;
            x.Dob = DateOnly.FromDateTime(dpDob.SelectedDate ?? DateTime.Now);
            x.Phone = txtPhone.Text;
            x.Email = txtEmail.Text;
            x.Province = cbxProvince.SelectedItem.ToString();
            x.District = cbxDistrict.SelectedItem.ToString();
            x.TestPurpose = cbxTestPurpose.SelectedItem?.ToString();
            var selectedService = cbxServiceType.SelectedItem as Service;
            x.ServiceType = selectedService?.ServiceName;
            x.AppointmentDate = dpAppointmentDate.SelectedDate ?? DateTime.Now;
            var timeOnly = TimeOnly.Parse(txtCollectionTime.Text);
            x.CollectionSampleTime = DateTime.Today.Add(timeOnly.ToTimeSpan());
            x.TestCategory = cbxTestCategory.SelectedItem?.ToString();
            x.CollectionLocation = cbxTestAddress.SelectedItem?.ToString();
            x.KitComponentId = long.Parse(cbxKitComponent.SelectedValue.ToString());
            x.AppointmentDate = dpAppointmentDate.SelectedDate ?? DateTime.Now;
                            x.UserId = UserCurrent?.UserId;


            // Thêm CollectedSample với SampleType
            var collectedSamples = new List<CollectedSample>();
            if (cbxSampleType.SelectedValue != null)
            {
                collectedSamples.Add(new CollectedSample
                {
                    SampleTypeId = Convert.ToInt64(cbxSampleType.SelectedValue)
                    // Có thể gán thêm các trường khác nếu cần
                });
            }
            x.CollectedSamples = collectedSamples;



            MessageBox.Show("Đặt lịch hẹn thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            _appointmentService.AddAppointment(x);
            this.Close();

        }

        public bool CheckVar()
        {
            // Check required text fields
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtFullName.Focus();
                return false;
            }
            if (dpDob.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpDob.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPhone.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }
            if (cbxProvince.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tỉnh/thành phố.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxProvince.Focus();
                return false;
            }
            if (cbxDistrict.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn quận/huyện.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxDistrict.Focus();
                return false;
            }
            if (cbxServiceType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại dịch vụ.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxServiceType.Focus();
                return false;
            }
            if (cbxTestPurpose.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn mục đích xét nghiệm.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxTestPurpose.Focus();
                return false;
            }
            if (cbxTestCategory.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại xét nghiệm.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxTestCategory.Focus();
                return false;
            }
            if (cbxTestAddress.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn địa điểm lấy mẫu.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxTestAddress.Focus();
                return false;
            }
            if (cbxKitComponent.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bộ kit xét nghiệm.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxKitComponent.Focus();
                return false;
            }
            if (dpAppointmentDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày hẹn.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpAppointmentDate.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCollectionTime.Text))
            {
                MessageBox.Show("Vui lòng nhập giờ lấy mẫu.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCollectionTime.Focus();
                return false;
            }
            // Validate time format
            if (!TimeOnly.TryParse(txtCollectionTime.Text, out _))
            {
                MessageBox.Show("Giờ lấy mẫu không hợp lệ. Định dạng đúng: HH:mm", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCollectionTime.Focus();
                return false;
            }

            return true;
        }


        public void FillComboBoxTestPurpose()
        {
            Service? selectedService = cbxServiceType.SelectedItem as Service;

            if (selectedService?.ServiceTestPurposes != null)
            {
                var testPurposeNames = selectedService.ServiceTestPurposes
                    .Where(stp => stp.IsActive == true && stp.TestPurpose != null && stp.TestPurpose.IsActive == true)
                    .Select(stp => stp.TestPurpose.TestPurposeName)
                    .ToList();

                cbxTestPurpose.ItemsSource = testPurposeNames;
            }
            else
            {
                cbxTestPurpose.ItemsSource = null;
            }
        }

        private void CbxKitComponent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBoxSampleType();
        }

        private void cbxServiceType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            FillComboBoxTestPurpose();
            FillComboBoxTestCategory();
            FillComboBoxKitComponent();
        }
    }
}
