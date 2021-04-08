using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class Report : Entity
    {
        public Contract Contract { get; set; }
        public double Discount { get; set; }
    }
}
