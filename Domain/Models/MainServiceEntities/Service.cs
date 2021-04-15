using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class Service : Entity
    {
        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Notation { get; set; }
        [Required]
        public string Name { get; set; }
        public List<ServiceEquipment> ServiceEquipmentList { get; set; }
    }
}
