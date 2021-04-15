using System;
using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class Position : Entity
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
