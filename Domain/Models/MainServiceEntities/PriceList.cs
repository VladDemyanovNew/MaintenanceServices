using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class PriceList : Entity
    {
        [Required]
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public List<Service> Services { get; set; }
    }
}
