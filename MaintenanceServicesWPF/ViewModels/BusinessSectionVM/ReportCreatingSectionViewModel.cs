using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
                Set(ref _ReportData, value);
            }
        }
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

        #region Equipments
        private ObservableCollection<Equipment> _Equipments;
        public ObservableCollection<Equipment> Equipments
        {
            get => _Equipments;
            set
            {
                Set(ref _Equipments, value);
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

            /*var aw2 = await _UnitOfWork.ServiceRep.GetAllAsync();
            this.Services = new ObservableCollection<Service>(aw2);*/

            /*var aw3 = await _UnitOfWork.EquipmentRep.GetAllAsync();
            this.Equipments = new ObservableCollection<Equipment>(aw3);*/
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
            var equipments = await _UnitOfWork.EquipmentRep.GetEquipmentsByService(service);
            (test[0] as ReportData).Equipments = new ObservableCollection<Equipment>(equipments);
        }
        private bool CanSelectedServiceCommandExecuted(object p) => true;
        #endregion

        #region ServiceAddCommand
        public ICommand ServiceAddCommand { get; }
        private void OnServiceAddCommandExecuted(object p)  // testing
        {
            ReportData reportData = new ReportData();
            reportData.ServiceEquipmentNavigation = new ServiceEquipment();
            reportData.ReportNavigation = ReportProp;
            reportData.Equipments = new ObservableCollection<Equipment>();
            this.ReportDataProp.Add(reportData);
        }
        private bool CanServiceAddCommandExecuted(object p) => true;
        #endregion

        #region ServiceRemoveCommand
        public ICommand ServiceRemoveCommand { get; }
        private void OnServiceRemoveCommandExecuted(object p)
        {
            if (ReportDataProp.Count != 0)
                this.ReportDataProp.RemoveAt(ReportDataProp.Count - 1);
        }
        private bool CanServiceRemoveCommandExecuted(object p) => true;
        #endregion

        #region ReportCreateCommand
        public ICommand ReportCreateCommand { get; }
        private void OnReportCreateCommandExecuted(object p)
        {

        }
        private bool CanReportCreateCommandExecuted(object p) => true;
        #endregion

        #endregion


        public ReportCreatingSectionViewModel(BusinessSectionViewModel parent)
        {
            #region Commands
            LoadDataCommand = new RelayCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecuted);
            ServiceAddCommand = new RelayCommand(OnServiceAddCommandExecuted, CanServiceAddCommandExecuted);
            ServiceRemoveCommand = new RelayCommand(OnServiceRemoveCommandExecuted, CanServiceRemoveCommandExecuted);
            ReportCreateCommand = new RelayCommand(OnReportCreateCommandExecuted, CanReportCreateCommandExecuted);
            SelectedPriceListCommand = new RelayCommand(OnSelectedPriceListCommandExecuted, CanSelectedPriceListCommandExecuted);
            SelectedServiceCommand = new RelayCommand(OnSelectedServiceCommandExecuted, CanSelectedServiceCommandExecuted);
            #endregion

            #region InitSection
            this._Parent = parent;
            this.SelectedContract = parent.SelectedContract;
            this.ReportProp = new Report();
            this._Report.ContractNavigation = this.SelectedContract;
            this.ReportDataProp = new ObservableCollection<ReportData>();
            ReportData test = new ReportData();
            test.ServiceEquipmentNavigation = new ServiceEquipment();
            
            #endregion
        }
    }
}
