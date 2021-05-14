using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;

namespace VDemyanov.MaintenanceServices.DAL.Services
{
    public class ReportRepository : EFGenericRepository<Report>
    {
        public ReportRepository(ApplicationContext db) : base(db) { }

    }
}
