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

        #region HumburgerIsChecked
        private bool _HumburgerIsChecked;
        public bool HumburgerIsChecked
        {
            get => _HumburgerIsChecked;
            set => Set(ref _HumburgerIsChecked, value);
        }
        #endregion

        #endregion

        #region Fields
        private BusinessZoneViewModel _BusinessZoneViewModel = new BusinessZoneViewModel();
        private DatabaseViewModel _DatabaseViewModel = new DatabaseViewModel();
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
                case ViewType.DATABASE:
                    CurrentViewModel = _DatabaseViewModel;
                    break;
                case ViewType.BUSINESS_ZONE:
                default:
                    CurrentViewModel = _BusinessZoneViewModel;
                    break;
            }
        }
        #endregion

        #region MainContent_PreviewMouseLeftButtonDownCommand
        public ICommand MainContent_PreviewMouseLeftButtonDownCommand { get; }
        private void OnMainContent_PreviewMouseLeftButtonDownCommandExecuted(object p)
        {
            HumburgerIsChecked = false;
        }
        private bool CanMainContent_PreviewMouseLeftButtonDownCommandExecuted(object p) => true;
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Commands
            OpenLoginWindowCommand = new RelayCommand(OnOpenLoginWindowCommandExecuted, CanOpenLoginWindowCommandExecuted);
            NavCommand = new RelayCommand(OnNavCommandExecuted, CanNavCommandExecuted);
            MainContent_PreviewMouseLeftButtonDownCommand = new RelayCommand(OnMainContent_PreviewMouseLeftButtonDownCommandExecuted, CanMainContent_PreviewMouseLeftButtonDownCommandExecuted);
            #endregion

            #region InitFields
            CurrentViewModel = _BusinessZoneViewModel;
            HumburgerIsChecked = false;
            #endregion InitFields
        }

    }
}
