using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class ServiceEquipment : Entity
    {
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public List<ReportData> ReportDataList { get; set; }
    }
}
