using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VDemyanov.MaintenanceServices.DAL.Services;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Commands;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Utils;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.Base;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Views.Windows;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels
{
    class LoginWindowViewModel : ViewModelBase
    {
        #region Fields
        public UnitOfWork _UnitOfWork = new UnitOfWork();
        #endregion

        #region Properties
        public Window CodeBehind { get; set; }

        #region Login
        private string _Login;
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }
        #endregion

        #endregion

        #region Commands

        #region LoginCommand
        public ICommand LoginCommand { get; }
        private void OnLoginCommandExecuted(object p)
        {
            var passwordBox = p as PasswordBox;
            if (passwordBox == null)
                return;

            var password = passwordBox.Password;

            LoginUser(this.Login, password);
        }
        private bool CanLoginCommandExecuted(object p) => true;

        private void LoginUser(string login, string password)
        {
            bool ok = false;
            if (!String.IsNullOrWhiteSpace(login)) // логин
            {
                if (!String.IsNullOrWhiteSpace(password)) // пароль
                {
                    ok = true;
                }
                else
                {
                    MessageBox.Show("Введите пароль!");
                }
            }
            else
            {
                MessageBox.Show("Введите логин!");
            }
            if (!ok) return;

            password = Cryptographer.GetMD5Hash(password);
            bool check = _UnitOfWork.EmployeeRep.CheckByLoginAndPassword(login, password);

            if (check)
            {
                string pos = _UnitOfWork.EmployeeRep.CheckPositionByLogin(login);
                if (pos == "admin")
                {
                    AdminWindow view = new AdminWindow();
                    view.Show();
                    this.CodeBehind.Close();
                }
                else if (pos == "user")
                {
                    MainWindow view = new MainWindow();
                    view.Show();
                    this.CodeBehind.Close();
                }
            }
            else
            {
                MessageBox.Show("Вы ввели неправильный логин или пароль!");
                return;
            }

           
        }
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
