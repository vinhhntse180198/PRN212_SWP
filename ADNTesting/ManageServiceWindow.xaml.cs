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
    /// Interaction logic for ManageServiceWindow.xaml
    /// </summary>
    public partial class ManageServiceWindow : Window
    {
        private ADNService _service = new();
        public ManageServiceWindow()
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
            dgv.ItemsSource = _service.GetAllService();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgv.ItemsSource = null;
            dgv.ItemsSource = _service.GetBySearch(txtSearch.Text);
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DetailServiceWindow d = new();
            d.ShowDialog();
            FillDataGrid();
        }

        

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Service? selected = dgv.SelectedItem as Service;
            if(selected == null)
            {
                MessageBox.Show("Please selected a service to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this service?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _service.Delete(selected);
            }
            FillDataGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Service? selected = dgv.SelectedItem as Service;
            if(selected == null)
            {
                MessageBox.Show("Please select a service to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DetailServiceWindow d = new();
            d.EditOne = selected;
            d.ShowDialog();
            FillDataGrid();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
