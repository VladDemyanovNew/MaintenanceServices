using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using VDemyanov.MaintenanceServices.DAL.Services;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Commands;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Infrastructure.Enums;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.Base;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.BusinessSectionVM
{
    class ContractInfoSectionViewModel : ViewModelBase
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
            set {
                Set(ref _SelectedContract, value);
            }
        }

        private string _SelectedContractDescription;
        public string SelectedContractDescription
        {
            get => _SelectedContractDescription;
            set => Set(ref _SelectedContractDescription, value);
        }
        #endregion

        #region UpdatingContract
        private Contract _UpdatingContract;
        public Contract UpdatingContract
        {
            get => _UpdatingContract;
            set
            {
                Set(ref _UpdatingContract, value);
            }
        }
        #endregion

        #region ContractCategoryCollection
        private ObservableCollection<ContractCategory> _ContractCategoryCollection;
        public ObservableCollection<ContractCategory> ContractCategoryCollection
        {
            get => _ContractCategoryCollection;
            set => Set(ref _ContractCategoryCollection, value);
        }
        #endregion

        #region ComboBoxItemsSource
        private ObservableCollection<string> _ComboBoxItemsSource;
        public ObservableCollection<string> ComboBoxItemsSource
        {
            get => _ComboBoxItemsSource;
            set => Set(ref _ComboBoxItemsSource, value);
        }
        #endregion

        #region SelectedContractCategory
        private string _SelectedContractCategory;
        public string SelectedContractCategory
        {
            get => _SelectedContractCategory;
            set => Set(ref _SelectedContractCategory, value);
        }
        #endregion

        #endregion

        #region Commands

        #region UpdateContractCommand
        public ICommand UpdateContractCommand { get; }
        private void OnUpdateContractCommandExecuted(object p) => UpdateContract((ContractProperty)p);
        private bool CanUpdateContractCommandExecuted(object p) => true;

        private async void UpdateContract(ContractProperty contractProperty)
        {
            switch (contractProperty)
            {
                case ContractProperty.NAME:
                    if (UpdatingContract.Name == null) break;
                    SelectedContract.Name = UpdatingContract.Name;
                    UpdateContractUtil();
                    break;
                case ContractProperty.CLIENT_NAME:
                    if (UpdatingContract.ClientName == null) break;
                    SelectedContract.ClientName = UpdatingContract.ClientName;
                    UpdateContractUtil();
                    break;
                case ContractProperty.CREATION_DATE:
                    if (UpdatingContract.CreationDate == null) break;
                    SelectedContract.CreationDate = UpdatingContract.CreationDate;
                    UpdateContractUtil();
                    break;
                case ContractProperty.FACILITY_ADDRESS:
                    if (UpdatingContract.FacilityAddress == null) break;
                    SelectedContract.FacilityAddress = UpdatingContract.FacilityAddress;
                    UpdateContractUtil();
                    break;
                case ContractProperty.CATEGORY:
                    if (SelectedContractCategory == null) break;
                    var aw = await _UnitOfWork.ContractCategoryRep.GetAllAsync();
                    SelectedContract.CategoryNavigation = aw.First(item => item.Description == SelectedContractCategory);
                    UpdateContractUtil();
                    break;
            }
        }

        private async void UpdateContractUtil()
        {
            await _UnitOfWork.ContractRep.UpdateAsync(SelectedContract);
            _UnitOfWork.Save();
            OnPropertyChanged(nameof(SelectedContract));
            _Parent.SelectedContracts.Refresh();
            UpdatingContract = new Contract();
        }
        #endregion

        #endregion

        #region Methods

        private async void LoadComboBoxItemsSource()
        {
            var aw = await _UnitOfWork.ContractCategoryRep.GetAllAsync();
            ContractCategoryCollection = new ObservableCollection<ContractCategory>(aw);
            ComboBoxItemsSource = new ObservableCollection<string>();
            foreach (var item in ContractCategoryCollection)
            {
                ComboBoxItemsSource.Add(item.Description);
            }
        }

        #endregion

        public ContractInfoSectionViewModel(BusinessSectionViewModel parent)
        {
            #region Commands
            UpdateContractCommand = new RelayCommand(OnUpdateContractCommandExecuted, CanUpdateContractCommandExecuted);
            #endregion

            #region InitSection
            _Parent = parent;
            this.SelectedContract = parent.SelectedContract;
            LoadComboBoxItemsSource();
            UpdatingContract = new Contract();
            #endregion
        }
    }
}
