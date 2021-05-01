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
            #region InitSection
            this.SelectedContract = selectedContract;
            LoadComboBoxItemsSource();
            #endregion
        }
    }
}
