using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class Equipment : Entity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
