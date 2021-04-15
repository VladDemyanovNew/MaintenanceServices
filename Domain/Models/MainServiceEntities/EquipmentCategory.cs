using System;
using System.Collections.Generic;
using System.Text;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class EquipmentCategory : Category
    {
        public List<Equipment> Equipments { get; set; }
    }
}
