using System;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class PriceList : Entity
    {
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
    }
}
