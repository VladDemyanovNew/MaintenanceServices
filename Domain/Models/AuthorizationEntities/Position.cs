using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.AuthorizationEntities
{
    public class Position : Entity
    {
        [Required]
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
