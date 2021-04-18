using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.DAL.Interfaces;

namespace VDemyanov.MaintenanceServices.DAL
{
    public class GenericRepository<T> : IGenericRep<T> where T : class
    {
        private readonly SampleContextFactory _contextFactory;

        public GenericRepository(SampleContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using(ApplicationContext context = _contextFactory.CreateDbContext())
            {
                //var createdEntity = await context.Set<T>().AddAsync(entity);
                context.Entry(entity).State = EntityState.Added;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (ApplicationContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public Task<T> Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
