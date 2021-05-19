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

        public double GetResultSum()
        {
            double result =  Convert.ToDouble(_db.Set<ReportData>()
            .FromSqlInterpolated($"SELECT SUM(REPORT_DATA.NUMBER * SERVICE.PRICE) FROM REPORT_DATA JOIN SERVICE_EQUIPMENT ON SERVICE_EQUIPMENT.ID = REPORT_DATA.SERVICE_EQUIPMENT JOIN SERVICE ON SERVICE.ID = SERVICE_EQUIPMENT.SERVICE_ID; "));
            return result;
        }

    }
}
