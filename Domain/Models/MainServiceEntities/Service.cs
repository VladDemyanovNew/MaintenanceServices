using System;
using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class Service : Entity
    {
        public Service()
        {
            ServiceEquipments = new HashSet<ServiceEquipment>();
        }

        public int? PriceList { get; set; }
        public string Unit { get; set; }
        public decimal? Price { get; set; }
        public string Notation { get; set; }
        public string Name { get; set; }

        public virtual PriceList PriceListNavigation { get; set; }
        public virtual ICollection<ServiceEquipment> ServiceEquipments { get; set; }
    }
}
