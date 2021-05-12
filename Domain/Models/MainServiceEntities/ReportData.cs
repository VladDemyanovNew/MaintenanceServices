using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class ReportData : Entity
    {
        public int? Report { get; set; }
        public int? ServiceEquipment { get; set; }
        public double? Number { get; set; }

        public virtual Report ReportNavigation { get; set; }
        public virtual ServiceEquipment ServiceEquipmentNavigation { get; set; }
        [NotMapped]
        public ObservableCollection<Equipment> Equipments { get; set; }
        [NotMapped]
        public ObservableCollection<Service> Services { get; set; }
    }
}
