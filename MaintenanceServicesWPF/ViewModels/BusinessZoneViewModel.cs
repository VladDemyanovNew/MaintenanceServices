using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Commands;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Enums;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.Base;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.BusinessZoneVM;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels
{
    class BusinessZoneViewModel : ViewModelBase
    {
        #region Properties

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
        private ContractCreatingZoneViewModel _ContractCreatingZoneViewModel = new ContractCreatingZoneViewModel();
        private ContractUpdatingZoneViewModel _ContractUpdatingZoneViewModel = new ContractUpdatingZoneViewModel();
        private ReportCreatingZoneViewModel _ReportCreatingZoneViewModel = new ReportCreatingZoneViewModel();
        private ReportPresentationZoneViewModel _ReportPresentationZoneViewModel = new ReportPresentationZoneViewModel();
        #endregion

        #region Commands

        #region NavCommand
        public ICommand NavCommand { get; }
        private void OnNavCommandExecuted(object p) => OnNav((ViewType)p);
        private bool CanNavCommandExecuted(object p) => true;

        private void OnNav(ViewType destination)
        {

            switch (destination)
            {
                case ViewType.CONTRACT_CREATING_ZONE:
                    CurrentViewModel = _ContractCreatingZoneViewModel;
                    break;
                case ViewType.CONTRACT_UPDATING_ZONE:
                    CurrentViewModel = _ContractUpdatingZoneViewModel;
                    break;
                case ViewType.REPORT_CREATING_ZONE:
                    CurrentViewModel = _ReportCreatingZoneViewModel;
                    break;
                case ViewType.REPORT_PRESENTATION_ZONE:
                    CurrentViewModel = _ReportPresentationZoneViewModel;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #endregion

        public BusinessZoneViewModel()
        {
            #region Commands
            NavCommand = new RelayCommand(OnNavCommandExecuted, CanNavCommandExecuted);
            #endregion
        }
    }
}
