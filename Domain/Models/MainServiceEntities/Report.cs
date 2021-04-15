using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class Report : Entity
    {
        public double Discount { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public List<ReportData> ReportDataList { get; set; }
    }
}
