using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VDemyanov.MaintenanceServices.DAL.Interfaces
{
    interface IGenericRep<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        Task<T> Create(T entity);

        Task<T> Update(int id, T entity);

        Task<bool> Delete(int id);
    }
}
