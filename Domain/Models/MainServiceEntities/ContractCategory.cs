using System;
using System.Collections.Generic;
using System.Text;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class ContractCategory : Category
    {
        public List<Contract> Contracts { get; set; }
    }
}
