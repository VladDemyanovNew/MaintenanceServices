using System;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class Contract : Entity
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string ClientName { get; set; }
        public string FacilityAddress { get; set; }
        public Category Category { get; set; }
    }
}
