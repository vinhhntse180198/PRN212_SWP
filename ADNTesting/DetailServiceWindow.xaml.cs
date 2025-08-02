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

    public partial class DetailServiceWindow : Window
    {
        public Service? EditOne { get; set; }

        private ADNService _ADNservice = new();
        public DetailServiceWindow()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckVar())
                return;
            Service x = new();
            x.ServiceName = txtServiceName.Text;
            x.Description = txtDescription.Text;
            x.Price = double.Parse(txtPrice.Text);

            if (EditOne == null)
            {
                _ADNservice.Create(x);
            }
            if (EditOne != null)
            {
                x.ServiceId = EditOne.ServiceId;
                _ADNservice.Update(x);
            }
            this.Close();
        }

        public void FillElements()
        {
            if (EditOne != null)
            {
                txtServiceName.Text = EditOne.ServiceName;
                txtDescription.Text = EditOne.Description;
                txtPrice.Text = EditOne.Price.ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (EditOne == null)
                lbl.Content = "Add New Service";
            else
                lbl.Content = "Edit Service";
            FillElements();
        }

        public bool CheckVar()
        {
            if (string.IsNullOrWhiteSpace(txtServiceName.Text))
            {
                MessageBox.Show("Service Name is required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Description is required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text) || !double.TryParse(txtPrice.Text, out _))
            {
                MessageBox.Show("Price is required and must be a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (double.Parse(txtPrice.Text) < 0)
            {
                MessageBox.Show("Price cannot be negative.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
