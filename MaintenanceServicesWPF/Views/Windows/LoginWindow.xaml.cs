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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.Loaded += LoginWindow_Loaded;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoginWindowViewModel viewModel = new LoginWindowViewModel();
            viewModel.CodeBehind = this;
            this.DataContext = viewModel;
        }
    }
}
