using System;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.AuthorizationEntities
{
    public class Employee : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Position PositionName { get; set; }
        public DateTime Birthday { get; set; }
        public string AvatarPath { get; set; }
    }
}
