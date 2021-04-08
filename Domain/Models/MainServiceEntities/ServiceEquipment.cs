using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class ServiceEquipment : Entity
    {
        public Equipment Equipment { get; set; }
        public Service Service { get; set; }
    }
}
