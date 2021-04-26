using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.DAL.Services;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;
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

        #region Contracts
        private ObservableCollection<Contract> _Contracts;
        public ObservableCollection<Contract> Contracts
        {
            get => _Contracts;
            set
            {
                if (!Set(ref _Contracts, value)) return;
                _SelectedContracts.Source = value;
                OnPropertyChanged(nameof(SelectedContracts));
            }
        }
        #endregion

        #region ContractsView
        private readonly CollectionViewSource _SelectedContracts = new CollectionViewSource();
        public ICollectionView SelectedContracts => _SelectedContracts?.View;
        #endregion

        #endregion

        #region Fields
        private ContractCreatingZoneViewModel _ContractCreatingZoneViewModel = new ContractCreatingZoneViewModel();
        private ContractUpdatingZoneViewModel _ContractUpdatingZoneViewModel = new ContractUpdatingZoneViewModel();
        private ReportCreatingZoneViewModel _ReportCreatingZoneViewModel = new ReportCreatingZoneViewModel();
        private ReportPresentationZoneViewModel _ReportPresentationZoneViewModel = new ReportPresentationZoneViewModel();
        private UnitOfWork _UnitOfWork;
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

        #warning test
        #region test

        private List<Contract> contrcts;

        private string _TestProp = "test";
        public string TestProp
        {
            get => _TestProp;
            set => Set(ref _TestProp, value);
        }

        public ICommand TestCommand { get; }
        private void OnTestCommandExecuted(object p) 
        {
            contrcts = _UnitOfWork.ContractRep.GetAll().ToList();
            TestProp = contrcts.First().Name;
        }
        private bool CanTestCommandExecuted(object p) => true;
        #endregion

        public BusinessZoneViewModel()
        {
            #region Commands
            NavCommand = new RelayCommand(OnNavCommandExecuted, CanNavCommandExecuted);
            TestCommand = new RelayCommand(OnTestCommandExecuted, CanTestCommandExecuted);
            #endregion

            _UnitOfWork = new UnitOfWork();
            Contracts = new ObservableCollection<Contract>(_UnitOfWork.ContractRep.GetAll());
            //List<Contract> contrcts = _contractRep.GetAllAsync().Result.ToList();

        }
    }
}
