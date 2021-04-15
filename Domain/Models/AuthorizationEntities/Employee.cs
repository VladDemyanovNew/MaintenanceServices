using System;
using System.Collections.Generic;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

#nullable disable

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public partial class Employee : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int? Position { get; set; }
        public DateTime? Bday { get; set; }
        public string AvatarPath { get; set; }

        public virtual Position PositionNavigation { get; set; }
    }
}
