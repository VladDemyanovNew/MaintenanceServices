using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VDemyanov.MaintenanceServices.DAL.Services;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Commands;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Utils;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.Base;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels
{
    class DatabaseViewModel : ViewModelBase
    {
        #region Fields
        public UnitOfWork _UnitOfWork = new UnitOfWork();
        private LoginWindowViewModel _Parent;
        private Employee _Employee;
        #endregion

        #region Properties

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

        #region UpdateLoginCommand
        public ICommand UpdateLoginCommand { get; }
        private async void OnUpdateLoginCommandExecuted(object p)
        {
            if (!ValidUniqLogin(this.Login))
                return;
            _Employee.Login = this.Login;
            await _UnitOfWork.EmployeeRep.UpdateAsync(_Employee);
            MessageBox.Show("Логин успешно обновлён!");
        }
        private bool CanUpdateLoginCommandExecuted(object p) => true;

        private bool ValidUniqLogin(string login)
        {
            if (String.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Заполните поле с логином!");
                return false;
            }

            if (_UnitOfWork.EmployeeRep.CheckByLogin(login))
            {
                MessageBox.Show("Логин уже занят!");
                return false;
            }

            return true;       
        }

        #endregion

        #region UpdatePasswordCommand
        public ICommand UpdatePasswordCommand { get; }
        private async void OnUpdatePasswordCommandExecuted(object p)
        {
            object[] param = (object[])p;
            var passwordBox1 = param[0] as PasswordBox;
            var passwordBox2 = param[1] as PasswordBox;
            if (passwordBox1 == null || passwordBox2 == null)
                return;
            var password = passwordBox1.Password;
            var confirmPassword = passwordBox2.Password;

            if (!ValidPassword(password, confirmPassword))
                return;

            this._Employee.Password = Cryptographer.GetMD5Hash(password);
            await _UnitOfWork.EmployeeRep.UpdateAsync(_Employee);
            MessageBox.Show("Пароль успешно обновлён!");
        }
        private bool CanUpdatePasswordCommandExecuted(object p) => true;

        private bool ValidPassword(string password, string confirmPassword)
        {
            if (!String.IsNullOrWhiteSpace(password)) // пароль
            {
                if (!String.IsNullOrWhiteSpace(confirmPassword)) //подтверждение пароля
                {
                    if (password != confirmPassword)
                    {
                        MessageBox.Show("Пароли не совпадают!");
                        return false;
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Введите подтверждение пароля!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите пароль пользователя!");
                return false;
            }
        }

        #endregion

        #endregion

        public DatabaseViewModel(LoginWindowViewModel parent)
        {
            #region Commands
            UpdateLoginCommand = new RelayCommand(OnUpdateLoginCommandExecuted, CanUpdateLoginCommandExecuted);
            UpdatePasswordCommand = new RelayCommand(OnUpdatePasswordCommandExecuted, CanUpdatePasswordCommandExecuted);
            #endregion

            #region InitFields
            _Parent = parent;
            _Employee = _UnitOfWork.EmployeeRep.GetByLogin(parent.Login);
            #endregion InitFields
        }
    }
}
