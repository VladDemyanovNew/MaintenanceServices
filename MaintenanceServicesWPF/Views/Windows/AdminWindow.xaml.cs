using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            this.Loaded += AdminWindow_Loaded;
        }

        private void AdminWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AdminWindowViewModel viewModel = new AdminWindowViewModel();
            viewModel.CodeBehind = this;
            this.DataContext = viewModel;
        }
    }
}
