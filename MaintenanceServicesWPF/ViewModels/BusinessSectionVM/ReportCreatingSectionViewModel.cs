using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Report _Report;
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

        #endregion

        #region Commands

        #region LoadDataCommand
        public ICommand LoadDataCommand { get; }
        private async void OnLoadDataCommandExecuted(object p)
        {
            var aw1 = await _UnitOfWork.PriceListRep.GetAllAsync();
            this.PriceListComboBox = new ObservableCollection<PriceList>(aw1);

            var aw2 = await _UnitOfWork.ServiceRep.GetAllAsync();
            this.Services = new ObservableCollection<Service>(aw2);

            var aw3 = await _UnitOfWork.EquipmentRep.GetAllAsync();
            this.Equipments = new ObservableCollection<Equipment>(aw3);
        }
        private bool CanLoadDataCommandExecuted(object p) => true;
        #endregion

        #endregion


        public ReportCreatingSectionViewModel(BusinessSectionViewModel parent)
        {
            #region Commands
            LoadDataCommand = new RelayCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecuted);
            #endregion

            #region InitSection
            this._Parent = parent;
            this.SelectedContract = parent.SelectedContract;
            this._Report = new Report();
            this._Report.ContractNavigation = this.SelectedContract;
            this.ReportDataProp = new ObservableCollection<ReportData>();
            this.ReportDataProp.Add(new ReportData());
            #endregion
        }
    }

    public class testStr
    {
        
    }
}
