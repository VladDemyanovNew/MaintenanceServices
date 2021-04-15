using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class Equipment : Entity
    {
        [Required]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public EquipmentCategory Category { get; set; }
        public List<ServiceEquipment> ServiceEquipmentList { get; set; }
    }
}
