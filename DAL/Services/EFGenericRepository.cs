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
        private readonly ApplicationContextFactory _contextFactory;

        public EFGenericRepository(ApplicationContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> AddAsync(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            using (ApplicationContext context = _contextFactory.CreateDbContext())
            {
                //var createdEntity = await context.Set<T>().AddAsync(entity);
                context.Entry(entity).State = EntityState.Added;
                await context.SaveChangesAsync(Cancel).ConfigureAwait(false);
                return entity;
            }
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            using (ApplicationContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }
        }

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default)
        {
            using (ApplicationContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().SingleOrDefaultAsync(e => e.Id == id, Cancel).ConfigureAwait(false);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken Cancel = default)
        {
            using (ApplicationContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync(Cancel).ConfigureAwait(false);
                return entities;
            }
        }

        public async Task UpdateAsync(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            using (ApplicationContext context = _contextFactory.CreateDbContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }
        }
    }
}
