using System;
using System.Collections.Generic;
using System.Text;
using VDemyanov.MaintenanceServices.Domain.Interfaces;

namespace VDemyanov.MaintenanceServices.Domain.Models.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
