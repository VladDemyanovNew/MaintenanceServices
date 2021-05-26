using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.DAL.Services;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Commands;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Enums;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.Base;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.BusinessSectionVM;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels
{
    class BusinessSectionViewModel : ViewModelBase
    {
        #region Fields
        public UnitOfWork _UnitOfWork;
        #endregion

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

        #region SelectedContract
        private Contract _SelectedContract;
        public Contract SelectedContract
        {
            get => _SelectedContract;
            set => Set(ref _SelectedContract, value);
        }
        #endregion

        #region ContractSearchText
        private string _ContractSearchText;
        public string ContractSearchText
        {
            get => _ContractSearchText;
            set
            {
                if (!Set(ref _ContractSearchText, value)) return;
                _SelectedContracts.View.Refresh();
            }
        }

        private void OnContractSearched(object sender, FilterEventArgs e)
        {

            if (!(e.Item is Contract contract))
            {
                e.Accepted = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(_ContractSearchText))
                return;

            if (contract.Name is null || contract.ClientName is null || contract.FacilityAddress is null)
            {
                e.Accepted = false;
                return;
            }

            var search_text = _ContractSearchText.ToLower();
            string name = contract.Name.ToLower();
            string clientName = contract.ClientName.ToLower();
            string facilityAddress = contract.FacilityAddress.ToLower();

            if (name.Contains(search_text)) return;
            if (clientName.Contains(search_text)) return;
            if (facilityAddress.Contains(search_text)) return;

            e.Accepted = false;
        }
        #endregion

        #endregion

        #region Commands

        #region NavCommand
        public ICommand NavCommand { get; }
        private void OnNavCommandExecuted(object p) => OnNav((ViewType)p);
        private bool CanNavCommandExecuted(object p) => true;
        private async void OnNav(ViewType destination)
        {

            switch (destination)
            {
                case ViewType.CONTRACT_CREATING_SECTION:
                    CurrentViewModel = new ContractCreatingSectionViewModel(this);
                    break;
                case ViewType.CONTRACT_INFO_SECTION:
                    if (SelectedContract != null)
                    {
                        SelectedContract.CategoryNavigation = await _UnitOfWork.ContractCategoryRep.GetAsync(SelectedContract.Category.Value);
                        CurrentViewModel = new ContractInfoSectionViewModel(this);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region OpenReportCreatingSectionCommand
        public ICommand OpenReportCreatingSectionCommand { get; }
        private void OnOpenReportCreatingSectionCommandExecuted(object p)
        {
            Contract contract = p as Contract;
            SelectedContract = contract;
            CurrentViewModel = new ReportCreatingSectionViewModel(this);
        }
        private bool CanOpenReportCreatingSectionCommandExecuted(object p) => true;
        #endregion

        #region OpenReportPresentationSectionCommand
        public ICommand OpenReportPresentationSectionCommand { get; }
        private void OnOpenReportPresentationSectionCommandExecuted(object p)
        {
            Contract contract = p as Contract;
            SelectedContract = contract;
            CurrentViewModel = new ReportPresentationSectionViewModel(this);
        }
        private bool CanOpenReportPresentationSectionCommandExecuted(object p) => true;
        #endregion

        #region RemoveContractCommand
        public ICommand RemoveContractCommand { get; }
        private void OnRemoveContractCommandExecuted(object p)
        {
            Contract contract = p as Contract;

            if (SelectedContract == contract)
                OnNav(ViewType.CONTRACT_CREATING_SECTION);


            List<Report> reports = _UnitOfWork.ReportRep.GetAll().Where(report => report.Contract == contract.Id).ToList();
            foreach(Report rep in reports)
            {
                _UnitOfWork.ReportDataRep.RemoveAllByReport(rep);
                _UnitOfWork.ReportRep.Remove(rep.Id);
            }

            _UnitOfWork.ContractRep.Remove(contract.Id);
            _UnitOfWork.Save();
            Contracts.Remove(contract);
        }
        private bool CanRemoveContractCommandExecuted(object p) => true;
        #endregion

        #region LoadDataCommand
        public ICommand LoadDataCommand { get; }
        private async void OnLoadDataCommandExecuted(object p)
        {
            var aw = await _UnitOfWork.ContractRep.GetAllAsync();
            Contracts = new ObservableCollection<Contract>(aw);
        }
        private bool CanLoadDataCommandExecuted(object p) => true;
        #endregion

        #endregion

        #region Methods
        public async void LoadData()
        {
            _UnitOfWork = new UnitOfWork();
            var aw = await _UnitOfWork.ContractRep.GetAllAsync();
            Contracts = new ObservableCollection<Contract>(aw);
            CurrentViewModel = new ContractCreatingSectionViewModel(this);

        }
        #endregion

        public BusinessSectionViewModel()
        {
            #region Commands
            NavCommand = new RelayCommand(OnNavCommandExecuted, CanNavCommandExecuted);
            RemoveContractCommand = new RelayCommand(OnRemoveContractCommandExecuted, CanRemoveContractCommandExecuted);
            LoadDataCommand = new RelayCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecuted);
            OpenReportCreatingSectionCommand = new RelayCommand(OnOpenReportCreatingSectionCommandExecuted, CanOpenReportCreatingSectionCommandExecuted);
            OpenReportPresentationSectionCommand = new RelayCommand(OnOpenReportPresentationSectionCommandExecuted, CanOpenReportPresentationSectionCommandExecuted);
            #endregion

            #region Filters
            _SelectedContracts.Filter += OnContractSearched;
            #endregion

            #region InitSection
            LoadData();
            #endregion
        }
    }
}
