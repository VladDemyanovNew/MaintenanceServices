using System;
using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class ServiceEquipment : Entity
    {
        public ServiceEquipment()
        {
            ReportData = new HashSet<ReportData>();
        }

        public int? ServiceId { get; set; }
        public int? EquipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<ReportData> ReportData { get; set; }
    }
}
