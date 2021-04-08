using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class ReportData : Entity
    {
        public Report Report { get; set; }
        public ServiceEquipment ServiceEquipment { get; set; }
        public double Number { get; set; }
    }
}
