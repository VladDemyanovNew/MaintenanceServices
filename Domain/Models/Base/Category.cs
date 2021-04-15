using System;
using System.Collections.Generic;
using System.Text;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.Base
{
    public abstract class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
