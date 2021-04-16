using System;
using System.Collections.Generic;
using System.Text;

namespace VDemyanov.MaintenanceServices.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Create(T item);
        T FindById(int id);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Remove(T item);
        void Update(T item);
    }
}
