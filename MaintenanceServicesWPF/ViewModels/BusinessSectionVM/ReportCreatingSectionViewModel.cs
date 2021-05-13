using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using VDemyanov.MaintenanceServices.DAL.Services;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Commands;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.Base;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.BusinessSectionVM
{
    class ReportCreatingSectionViewModel : ViewModelBase
    {
        #region Fields
        public UnitOfWork _UnitOfWork = new UnitOfWork();
        private BusinessSectionViewModel _Parent;
        #endregion

        #region Properties

        #region SelectedContract
        private Contract _SelectedContract;
        public Contract SelectedContract
        {
            get => _SelectedContract;
            set
            {
                Set(ref _SelectedContract, value);
            }
        }
        #endregion

        #region PriceListComboBox
        private ObservableCollection<PriceList> _PriceListComboBox;
        public ObservableCollection<PriceList> PriceListComboBox
        {
            get => _PriceListComboBox;
            set
            {
                Set(ref _PriceListComboBox, value);
            }
        }
        #endregion

        #region ReportData
        private ObservableCollection<ReportData> _ReportData;
        public ObservableCollection<ReportData> ReportDataProp
        {
            get => _ReportData;
            set
            {
                //Set(ref _ReportData, value);
                if (!Set(ref _ReportData, value)) return;
                _SelectedReportData.Source = value;
                OnPropertyChanged(nameof(SelectedReportData));
            }
        }

        #region ReportDataView
        private CollectionViewSource _SelectedReportData = new CollectionViewSource();
        public ICollectionView SelectedReportData => _SelectedReportData?.View;
        #endregion

        #endregion

        #region Report
        private Report _Report;
        public Report ReportProp
        {
            get => _Report;
            set
            {
                Set(ref _Report, value);
            }
        }
        #endregion

        #region Services
        private ObservableCollection<Service> _Services;
        public ObservableCollection<Service> Services
        {
            get => _Services;
            set
            {
                Set(ref _Services, value);
            }
        }
        #endregion

        #region SelectedPriceList
        private PriceList _SelectedPriceList;
        public PriceList SelectedPriceList
        {
            get => _SelectedPriceList;
            set
            {
                Set(ref _SelectedPriceList, value);
            }
        }
        #endregion

        #endregion

        #region Commands

        #region LoadDataCommand
        public ICommand LoadDataCommand { get; }
        private async void OnLoadDataCommandExecuted(object p)
        {
            var aw1 = await _UnitOfWork.PriceListRep.GetAllAsync();
            this.PriceListComboBox = new ObservableCollection<PriceList>(aw1);
        }
        private bool CanLoadDataCommandExecuted(object p) => true;
        #endregion

        #region SelectedPriceListCommand
        public ICommand SelectedPriceListCommand { get; }
        private async void OnSelectedPriceListCommandExecuted(object p)
        {
            var aw = await _UnitOfWork.ServiceRep.GetAllAsync();
            this.Services = new ObservableCollection<Service>(aw.Where(item => item.PriceListNavigation.Equals(SelectedPriceList)));
        }
        private bool CanSelectedPriceListCommandExecuted(object p) => true;
        #endregion

        #region SelectedServiceCommand
        public ICommand SelectedServiceCommand { get; }
        private async void OnSelectedServiceCommandExecuted(object p)
        {
            object[] test = (object[])p;
            Service service = test[1] as Service;
            if (service==null)
                return;
            var equipments = await _UnitOfWork.EquipmentRep.GetEquipmentsByService(service);

            (test[0] as ReportData).Equipments = new ObservableCollection<Equipment>(equipments);
            OnPropertyChanged(nameof(SelectedReportData));
            SelectedReportData.Refresh();
            (test[0] as ReportData).ServiceEquipmentNavigation.Service = service;
        }
        private bool CanSelectedServiceCommandExecuted(object p) => true;
        #endregion

        #region ReportDataAddCommand
        public ICommand ReportDataAddCommand { get; }
        private void OnReportDataAddCommandExecuted(object p)  // testing
        {
            ReportData reportData = new ReportData();
            reportData.ServiceEquipmentNavigation = new ServiceEquipment();
            reportData.ReportNavigation = ReportProp;
            reportData.Equipments = new ObservableCollection<Equipment>();
            reportData.Services = Services;

            this.ReportDataProp.Add(reportData);
        }
        private bool CanReportDataAddCommandExecuted(object p) => true;
        #endregion

        #region ReportDataRemoveCommand
        public ICommand ReportDataRemoveCommand { get; }
        private void OnReportDataRemoveCommandExecuted(object p)
        {
            if (ReportDataProp.Count != 0)
                this.ReportDataProp.RemoveAt(ReportDataProp.Count - 1);
        }
        private bool CanReportDataRemoveCommandExecuted(object p) => true;
        #endregion

        #region ReportCreateCommand
        public ICommand ReportCreateCommand { get; }
        private async void OnReportCreateCommandExecuted(object p)
        {
            await _UnitOfWork.ReportRep.AddAsync(ReportProp);
            
            foreach (ReportData item in ReportDataProp)
            {
                await _UnitOfWork.ServiceEquipmentRep.AddAsync(item.ServiceEquipmentNavigation);
                await _UnitOfWork.ReportDataRep.AddAsync(item);
            }
        }
        private bool CanReportCreateCommandExecuted(object p) => true;
        #endregion

        #endregion


        public ReportCreatingSectionViewModel(BusinessSectionViewModel parent)
        {
            #region Commands
            LoadDataCommand = new RelayCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecuted);
            ReportDataAddCommand = new RelayCommand(OnReportDataAddCommandExecuted, CanReportDataAddCommandExecuted);
            ReportDataRemoveCommand = new RelayCommand(OnReportDataRemoveCommandExecuted, CanReportDataRemoveCommandExecuted);
            ReportCreateCommand = new RelayCommand(OnReportCreateCommandExecuted, CanReportCreateCommandExecuted);
            SelectedPriceListCommand = new RelayCommand(OnSelectedPriceListCommandExecuted, CanSelectedPriceListCommandExecuted);
            SelectedServiceCommand = new RelayCommand(OnSelectedServiceCommandExecuted, CanSelectedServiceCommandExecuted);
            #endregion

            #region InitSection
            this._Parent = parent;
            this.SelectedContract = parent.SelectedContract;
            this.ReportProp = new Report();
            this._Report.ContractNavigation = this.SelectedContract;
            this._Report.Discount = 0;
            this.ReportDataProp = new ObservableCollection<ReportData>();
            #endregion
        }
    }
}
