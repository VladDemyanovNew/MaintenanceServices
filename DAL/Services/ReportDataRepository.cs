using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;

namespace VDemyanov.MaintenanceServices.DAL.Services
{
    public class ReportDataRepository : EFGenericRepository<ReportData>
    {
        public ReportDataRepository(ApplicationContext db) : base(db) { }

        public void RemoveAllByReport(Report report, CancellationToken Cancel = default)
        {
            if (report is null) throw new ArgumentNullException(nameof(report));

            IEnumerable<ReportData> repData = _db.Set<ReportData>().Where(item => item.Report == report.Id);
            _db.Set<ReportData>().RemoveRange(repData);
        }

    }
}
