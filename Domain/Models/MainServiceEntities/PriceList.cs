using System;
using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class PriceList : Entity
    {
        public PriceList()
        {
            Services = new HashSet<Service>();
        }

        public DateTime? CreationDate { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
