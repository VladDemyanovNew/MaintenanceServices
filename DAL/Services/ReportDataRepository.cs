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

        public double GetResultSum(Report report)
        {
            if (report is null) throw new ArgumentNullException(nameof(report));

            double result = (double)(from rep in _db.ReportData
                           join serv_eq in _db.ServiceEquipments on rep.ServiceEquipment equals serv_eq.Id
                           join serv in _db.Services on serv_eq.ServiceId equals serv.Id
                           where rep.Report == report.Id
                           select new
                           {
                               Sum = rep.Number * (double)serv.Price
                           }).Sum(s => s.Sum);
            return result;
        }

    }
}
