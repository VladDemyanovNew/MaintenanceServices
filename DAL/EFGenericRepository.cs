using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDemyanov.MaintenanceServices.DAL.Interfaces;

namespace VDemyanov.MaintenanceServices.DAL
{
    public class EFGenericRepository<T> : IGenericRepository<T> where T : class
    {
        DbContext _context;
        DbSet<T> _dbSet;

        public EFGenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        public T FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Remove(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
