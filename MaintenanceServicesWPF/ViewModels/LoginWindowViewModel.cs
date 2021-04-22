using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Commands;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.Base;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Views.Windows;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels
{
    class LoginWindowViewModel : ViewModelBase
    {
        #region Properties
        public Window CodeBehind { get; set; }
        #endregion

        #region Commands

        #region LoginCommand
        public ICommand LoginCommand { get; }
        private void OnLoginCommandExecuted(object p)
        {
            MainWindow view = new MainWindow();
            view.Show();
            this.CodeBehind.Close();
        }
        private bool CanLoginCommandExecuted(object p) => true;
        #endregion

        #endregion

        public LoginWindowViewModel()
        {
            

            #region Commands
            LoginCommand = new RelayCommand(OnLoginCommandExecuted, CanLoginCommandExecuted);
            #endregion
        }

       
    }
}
