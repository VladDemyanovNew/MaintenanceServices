using System.ComponentModel.DataAnnotations;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class ReportData : Entity
    {
        public int ReportId { get; set; }
        public Report Report { get; set; }
        public int ServiceEquipmentId { get; set; }
        public ServiceEquipment ServiceEquipment { get; set; }
        [Required]
        public double Number { get; set; }
    }
}
