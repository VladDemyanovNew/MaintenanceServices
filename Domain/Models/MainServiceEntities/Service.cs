using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class Service : Entity
    {
        public PriceList PriceList { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public string Notation { get; set; }
        public string Name { get; set; }
    }
}
