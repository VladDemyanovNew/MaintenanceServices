using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;

namespace VDemyanov.MaintenanceServices.DAL.Services
{
    public class EmployeeRepository : EFGenericRepository<Employee>
    {
        public EmployeeRepository(ApplicationContext db) : base(db) { }

        public bool CheckByLogin(string login)
        {
            if (login is null) throw new ArgumentNullException(nameof(login));

            bool result = _db.Employees.Any(empl => empl.Login == login);

            return result;
        }

        public bool CheckByLoginAndPassword(string login, string password)
        {
            if (login is null || password is null) throw new ArgumentNullException(nameof(login));

            bool result = _db.Employees.Any(empl => empl.Login == login && empl.Password == password);

            return result;
        }

        public string CheckPositionByLogin(string login)
        {
            if (login is null) throw new ArgumentNullException(nameof(login));

            string pos = (from empl in _db.Employees
                         join empl_pos in _db.Positions on empl.Position equals empl_pos.Id
                         where empl.Login == login
                         select new
                         {
                             Position = empl_pos.Name
                         }).FirstOrDefault().Position;
            return pos;
        }
    }
}
