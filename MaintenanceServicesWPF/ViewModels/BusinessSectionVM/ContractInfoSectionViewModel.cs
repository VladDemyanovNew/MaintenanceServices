using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    SelectedContract.Name = UpdatingContract.Name;
                    OnPropertyChanged(nameof(SelectedContract));
                    await _UnitOfWork.ContractRep.UpdateAsync(SelectedContract);
                    _UnitOfWork.Save();
                    UpdatingContract = new Contract();
                    break;
                case ContractProperty.CLIENT_NAME:
                    break;
                case ContractProperty.CREATION_DATE:
                    break;
                case ContractProperty.FACILITY_ADDRESS:
                    break;
                case ContractProperty.CATEGORY:
                    break;
            }
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

        public ContractInfoSectionViewModel(Contract selectedContract)
        {
            #region Commands
            UpdateContractCommand = new RelayCommand(OnUpdateContractCommandExecuted, CanUpdateContractCommandExecuted);
            #endregion

            #region InitSection
            this.SelectedContract = selectedContract;
            LoadComboBoxItemsSource();
            UpdatingContract = new Contract();
            #endregion
        }
    }
}
