using System;
using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class ContractCategory : Entity
    {
        public ContractCategory()
        {
            Contracts = new HashSet<Contract>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
