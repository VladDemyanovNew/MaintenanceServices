using System;
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
    class ReportPresentationSectionViewModel : ViewModelBase
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

        #region ResultSum
        private double _ResultSum;
        public double ResultSum
        {
            get => _ResultSum;
            set
            {
                Set(ref _ResultSum, value);
            }
        }
        #endregion

        #region ResultDiscountSum
        private double _ResultDiscountSum;
        public double ResultDiscountSum
        {
            get => _ResultDiscountSum;
            set
            {
                Set(ref _ResultDiscountSum, value);
            }
        }
        #endregion

        #region Reports
        private ObservableCollection<Report> _Reports;
        public ObservableCollection<Report> Reports
        {
            get => _Reports;
            set
            {
                if (!Set(ref _Reports, value)) return;
                _SelectedReportView.Source = value;
                OnPropertyChanged(nameof(SelectedReportView));
            }
        }

        #region ReportView
        private CollectionViewSource _SelectedReportView = new CollectionViewSource();
        public ICollectionView SelectedReportView => _SelectedReportView?.View;
        #endregion

        #endregion

        #region SelectedReport
        private Report _SelectedReport;
        public Report SelectedReport
        {
            get => _SelectedReport;
            set => Set(ref _SelectedReport, value);
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

        #region ReportSearchText
        private string _ReportSearchText;
        public string ReportSearchText
        {
            get => _ReportSearchText;
            set
            {
                if (!Set(ref _ReportSearchText, value)) return;
                _SelectedReportView.View.Refresh();
            }
        }

        private void OnReportSearched(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Report report))
            {
                e.Accepted = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(_ReportSearchText))
                return;

            if (report.Id.ToString() is null)
            {
                e.Accepted = false;
                return;
            }

            var search_text = _ReportSearchText.ToLower();
            string reportId = report.Id.ToString().ToLower();
           

            if (reportId.Contains(search_text)) return;

            e.Accepted = false;
        }
        #endregion

        #endregion

        #region Commands

        #region LoadDataCommand
        public ICommand LoadDataCommand { get; }
        private async void OnLoadDataCommandExecuted(object p)
        {
            var aw = await _UnitOfWork.ReportRep.GetAllAsync();
            this.Reports = new ObservableCollection<Report>(aw.Where(item => item.Contract == SelectedContract.Id));
        }
        private bool CanLoadDataCommandExecuted(object p) => true;
        #endregion

        #region AddReportCommand
        public ICommand AddReportCommand { get; }
        private void OnAddReportCommandExecuted(object p) => _Parent.CurrentViewModel = new ReportCreatingSectionViewModel(_Parent);
        private bool CanAddReportCommandExecuted(object p) => true;
        #endregion

        #region RemoveReportCommand
        public ICommand RemoveReportCommand { get; }
        private void OnRemoveReportCommandExecuted(object p)
        {
            Report report = p as Report;

            _UnitOfWork.ReportDataRep.RemoveAllByReport(report);
            _UnitOfWork.ReportRep.Remove(report.Id);
            _UnitOfWork.Save();
            Reports.Remove(report);
        }
        private bool CanRemoveReportCommandExecuted(object p) => true;
        #endregion

        #region ShowReportCommand
        public ICommand ShowReportCommand { get; }
        private void OnShowReportCommandExecuted(object p)
        {
            List<ReportData> reportDataList = _UnitOfWork.ReportDataRep.GetAll().Where(item => item.Report == SelectedReport.Id).ToList();
            ReportDataProp = ConfigReportDataList(reportDataList);
            ResultSum = _UnitOfWork.ReportDataRep.GetResultSum(SelectedReport);
            ResultDiscountSum = (double)(ResultSum - ResultSum * SelectedReport.Discount / 100);
        }
        private bool CanShowReportCommandExecuted(object p) => true;

        private ObservableCollection<ReportData> ConfigReportDataList(List<ReportData> reportDataList)
        {
            foreach (var repData in reportDataList)
            {
                ServiceEquipment serviceEquipment = _UnitOfWork.ServiceEquipmentRep.GetAll().FirstOrDefault(servEq => servEq.Id == repData.ServiceEquipment);
                serviceEquipment.Service = _UnitOfWork.ServiceRep.GetAll().FirstOrDefault(serv => serv.Id == serviceEquipment.ServiceId);
                serviceEquipment.Equipment = _UnitOfWork.EquipmentRep.GetAll().FirstOrDefault(eq => eq.Id == serviceEquipment.EquipmentId);
                repData.ServiceEquipmentNavigation = serviceEquipment;
            }

            return new ObservableCollection<ReportData>(reportDataList);
        }
        #endregion

        #endregion

        public ReportPresentationSectionViewModel(BusinessSectionViewModel parent)
        {
            #region Commands
            LoadDataCommand = new RelayCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecuted);
            AddReportCommand = new RelayCommand(OnAddReportCommandExecuted, CanAddReportCommandExecuted);
            RemoveReportCommand = new RelayCommand(OnRemoveReportCommandExecuted, CanRemoveReportCommandExecuted);
            ShowReportCommand = new RelayCommand(OnShowReportCommandExecuted, CanShowReportCommandExecuted);
            #endregion

            #region Filters
            _SelectedReportView.Filter += OnReportSearched;
            #endregion

            #region InitSection
            this.SelectedContract = parent.SelectedContract;
            this._Parent = parent;
            #endregion
        }
    }
}
