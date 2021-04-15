using System;
using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class Contract : Entity
    {
        public Contract()
        {
            Reports = new HashSet<Report>();
        }

        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ClientName { get; set; }
        public string FacilityAddress { get; set; }
        public int? Category { get; set; }

        public virtual ContractCategory CategoryNavigation { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
