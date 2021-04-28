using System;
using System.Collections.Generic;
using System.Text;
using VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.Base;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.ViewModels.BusinessZoneVM
{
    class ContractCreatingZoneViewModel : ViewModelBase
    {
        #region Fields
        private BusinessZoneViewModel _Parent;
        #endregion



        public ContractCreatingZoneViewModel(BusinessZoneViewModel parent)
        {
            _Parent = parent;
            
        }
    }
}
