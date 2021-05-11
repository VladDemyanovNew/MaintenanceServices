using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.DAL.Interfaces;
using VDemyanov.MaintenanceServices.Domain.Interfaces;

namespace VDemyanov.MaintenanceServices.DAL.Services
{
    public class EFGenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private protected readonly ApplicationContext _db;

        public EFGenericRepository(ApplicationContext db)
        {
            this._db = db;
        }

        public async Task<T> AddAsync(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            //var createdEntity = await context.Set<T>().AddAsync(entity);
            _db.Entry(entity).State = EntityState.Added;
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return entity;
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            T entity = await _db.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public void Remove(int id)
        {
            T entity = _db.Set<T>().Find(id);

            if (entity != null)
                _db.Set<T>().Remove(entity);
        }

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default)
        {
            T entity = await _db.Set<T>().SingleOrDefaultAsync(e => e.Id == id, Cancel).ConfigureAwait(false);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken Cancel = default)
        {
            IEnumerable<T> entities = await _db.Set<T>().ToListAsync().ConfigureAwait(false);
            return entities;
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>();
        }

        public async Task UpdateAsync(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }
    }
}
