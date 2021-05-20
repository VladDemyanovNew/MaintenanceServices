using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VDemyanov.MaintenanceServices.DAL.Services;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Commands;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Utils;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.Base;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Views.Windows;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels
{
    class AdminWindowViewModel : ViewModelBase
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

        #region CheckBox
        private bool _CheckBox = false;
        public bool CheckBox
        {
            get => _CheckBox;
            set => Set(ref _CheckBox, value);
        }
        #endregion

        #endregion

        #region Commands

        #region BackToLoginCommand
        public ICommand BackToLoginCommand { get; }
        private void OnBackToLoginCommandExecuted(object p)
        {
            LoginWindow view = new LoginWindow();
            view.Show();
            this.CodeBehind.Close();
        }
        private bool CanBackToLoginCommandExecuted(object p) => true;
        #endregion

        #region RegisterUserCommand
        public ICommand RegisterUserCommand { get; }
        private void OnRegisterUserCommandExecuted(object p)
        {
            object[] param = (object[])p;
            var passwordBox1 = param[0] as PasswordBox;
            var passwordBox2 = param[1] as PasswordBox;
            if (passwordBox1 == null || passwordBox2 == null)
                return;
            var password = passwordBox1.Password;
            var confirmPassword = passwordBox2.Password;

            RegisterUser(this.Login, password, confirmPassword);
        }
        private bool CanRegisterUserCommandExecuted(object p) => true;

        private void RegisterUser(string login, string password, string confirmPassword)
        {
            bool ok = false;
            if (!String.IsNullOrWhiteSpace(login)) // логин
            {
                if (!String.IsNullOrWhiteSpace(password)) // пароль
                {
                    if (!String.IsNullOrWhiteSpace(confirmPassword)) //подтверждение пароля
                    {
                        if (password != confirmPassword)
                        {
                            MessageBox.Show("Пароли не совпадают!");
                        }
                        else
                        {
                            ok = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите подтверждение пароля!");
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль пользователя!");
                }
            }
            else
            {
                MessageBox.Show("Введите логин пользователя!");
            }
            if (!ok) return;



            bool check = _UnitOfWork.EmployeeRep.CheckByLogin(login);

            if (check) // если пользователь с таким логином уже существует
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return;
            }
            else
            {
                Position pos;

                if (CheckBox) // admin
                    pos = _UnitOfWork.PositionRep.GetAll().Where(item => item.Name == "admin").FirstOrDefault();
                else // user
                    pos = _UnitOfWork.PositionRep.GetAll().Where(item => item.Name == "user").FirstOrDefault();

                Employee employee = new Employee
                {
                    Login = login,
                    Password = Cryptographer.GetMD5Hash(password),
                    PositionNavigation = pos
                };

                _UnitOfWork.EmployeeRep.Add(employee);
                _UnitOfWork.Save();

                MessageBox.Show("Пользователь зарегестрирован!");
            }
        }
        #endregion

        #endregion

        public AdminWindowViewModel()
        {
            #region Commands
            BackToLoginCommand = new RelayCommand(OnBackToLoginCommandExecuted, CanBackToLoginCommandExecuted);
            RegisterUserCommand = new RelayCommand(OnRegisterUserCommandExecuted, CanRegisterUserCommandExecuted);
            #endregion
        }
    }
}
