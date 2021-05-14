using System;
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

        #region Reports
        private ObservableCollection<Report> _Reports;
        public ObservableCollection<Report> Reports
        {
            get => _Reports;
            set
            {
                Set(ref _Reports, value);
            }
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

        #endregion

        public ReportPresentationSectionViewModel(BusinessSectionViewModel parent)
        {
            #region Commands
            LoadDataCommand = new RelayCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecuted);
            AddReportCommand = new RelayCommand(OnAddReportCommandExecuted, CanAddReportCommandExecuted);
            RemoveReportCommand = new RelayCommand(OnRemoveReportCommandExecuted, CanRemoveReportCommandExecuted);
            #endregion

            #region InitSection
            this.SelectedContract = parent.SelectedContract;
            this._Parent = parent;
            #endregion
        }
    }
}
