using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.DAL.Interfaces;
using VDemyanov.MaintenanceServices.Domain.Interfaces;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;

namespace VDemyanov.MaintenanceServices.DAL.Services
{
    public class EquipmentRepository : EFGenericRepository<Equipment>
    {

        public EquipmentRepository(ApplicationContext db) : base(db) { }

        public async Task<IEnumerable<Equipment>> GetEquipmentsByService(Service entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            IEnumerable<Equipment> result = await _db.Set<Equipment>()
                .FromSqlInterpolated($"SELECT * FROM EQUIPMENT WHERE ID IN (SELECT DISTINCT EQUIPMENT_ID FROM SERVICE_EQUIPMENT WHERE SERVICE_ID = {entity.Id})")
                .ToListAsync().ConfigureAwait(false);

            return result;
        }
    }
}
