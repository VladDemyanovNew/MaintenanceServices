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
    class ContractCreatingSectionViewModel : ViewModelBase
    {
        #region Fields
        private BusinessSectionViewModel _Parent;
        public UnitOfWork _UnitOfWork = new UnitOfWork();
        #endregion

        #region Properties

        #region Contract
        private Contract _Contract;
        public Contract ContractProp
        {
            get => _Contract;
            set => Set(ref _Contract, value);
        }

        private string _SelectedContractCategory;
        public string SelectedContractCategory
        {
            get => _SelectedContractCategory;
            set => Set(ref _SelectedContractCategory, value);
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

        #endregion

        #region Commands

        #region CreateContractCommand
        public ICommand CreateContractCommand { get; }
        private async void OnCreateContractCommandExecuted(object p)
        {
            var aw = await _UnitOfWork.ContractCategoryRep.GetAllAsync();
            ContractProp.CategoryNavigation = aw.First(item => item.Description == SelectedContractCategory);
            await _UnitOfWork.ContractRep.AddAsync(ContractProp);
            _UnitOfWork.Save();
            _Parent.Contracts.Add(ContractProp);

            ContractProp = new Contract();
        }
        private bool CanCreateContractCommandExecuted(object p) => true;
        #endregion

        #region LoadDataCommand
        public ICommand LoadDataCommand { get; }
        private async void OnLoadDataCommandExecuted(object p)
        {
            var aw = await _UnitOfWork.ContractCategoryRep.GetAllAsync();
            ContractCategoryCollection = new ObservableCollection<ContractCategory>(aw);
            ComboBoxItemsSource = new ObservableCollection<string>();
            foreach (var item in ContractCategoryCollection)
            {
                ComboBoxItemsSource.Add(item.Description);
            }
        }
        private bool CanLoadDataCommandExecuted(object p) => true;
        #endregion

        #endregion

        public ContractCreatingSectionViewModel(BusinessSectionViewModel parent)
        {
            #region Commands
            CreateContractCommand = new RelayCommand(OnCreateContractCommandExecuted, CanCreateContractCommandExecuted);
            LoadDataCommand = new RelayCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecuted);
            #endregion

            #region InitSection
            _Parent = parent;
            _Contract = new Contract();
            #endregion
        }

    }
}
