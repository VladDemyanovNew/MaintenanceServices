using System;
using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class EquipmentCategory : Entity
    {
        public EquipmentCategory()
        {
            Equipment = new HashSet<Equipment>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
