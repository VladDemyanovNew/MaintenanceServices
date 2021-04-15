using System;
using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class Equipment : Entity
    {
        public Equipment()
        {
            ServiceEquipments = new HashSet<ServiceEquipment>();
        }

        public string Name { get; set; }
        public int? Category { get; set; }

        public virtual EquipmentCategory CategoryNavigation { get; set; }
        public virtual ICollection<ServiceEquipment> ServiceEquipments { get; set; }
    }
}
