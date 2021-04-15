using System;
using System.ComponentModel.DataAnnotations;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.AuthorizationEntities
{
    public class Employee : Entity
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string AvatarPath { get; set; }
        public int PositionId { get; set; }
        public Position PositionName { get; set; }
    }
}
