using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Commands;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Enums;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.Base;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Views.Windows;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {

        #region Properties

        /// <summary>
        /// Хранит ссылку на представление,
        /// к которому привязан MainWindowViewModel
        /// </summary>
        public Window CodeBehind { get; set; }

        #region CurrentViewModel
        private ViewModelBase _CurrentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _CurrentViewModel;
            set => Set(ref _CurrentViewModel, value);
        }
        #endregion

        #endregion

        #region Fields
        private HomeViewModel _HomeViewModel = new HomeViewModel();
        private ReportsViewModel _ReportsViewModel = new ReportsViewModel();
        #endregion

        #region Commands

        #region OpenLoginWindowCommand
        public ICommand OpenLoginWindowCommand { get; }
        private void OnOpenLoginWindowCommandExecuted(object p)
        {
            LoginWindow view = new LoginWindow();
            view.Show();
            this.CodeBehind.Close();
        }
        private bool CanOpenLoginWindowCommandExecuted(object p) => true;
        #endregion

        #region NavCommand
        public ICommand NavCommand { get; }
        private void OnNavCommandExecuted(object p) => OnNav((ViewType)p);
        private bool CanNavCommandExecuted(object p) => true;

        private void OnNav(ViewType destination)
        {

            switch (destination)
            {
                case ViewType.REPORTS:
                    CurrentViewModel = _ReportsViewModel;
                    break;
                case ViewType.HOME:
                default:
                    CurrentViewModel = _HomeViewModel;
                    break;
            }
        }
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Commands
            OpenLoginWindowCommand = new RelayCommand(OnOpenLoginWindowCommandExecuted, CanOpenLoginWindowCommandExecuted);
            NavCommand = new RelayCommand(OnNavCommandExecuted, CanNavCommandExecuted);
            #endregion

            #region InitFields
            CurrentViewModel = _HomeViewModel;
            #endregion InitFields
        }
    }
}
