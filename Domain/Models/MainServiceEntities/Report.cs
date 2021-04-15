using System;
using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class Report : Entity
    {
        public Report()
        {
            ReportData = new HashSet<ReportData>();
        }

        public int? Contract { get; set; }
        public double? Discount { get; set; }

        public virtual Contract ContractNavigation { get; set; }
        public virtual ICollection<ReportData> ReportData { get; set; }
    }
}
