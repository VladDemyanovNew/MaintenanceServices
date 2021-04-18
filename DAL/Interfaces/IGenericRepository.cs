using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VDemyanov.MaintenanceServices.DAL.Interfaces
{
    interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken Cancel);

        Task<T> GetAsync(int id, CancellationToken Cancel);

        Task<T> AddAsync(T entity, CancellationToken Cancel);

        Task UpdateAsync(T entity, CancellationToken Cancel);

        Task RemoveAsync(int id, CancellationToken Cancel);
    }
}
