using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VDemyanov.MaintenanceServices.Domain.Models.Base;

namespace VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities
{
    public class Contract : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string FacilityAddress { get; set; }
        public int CategoryId { get; set; }
        public ContractCategory Category { get; set; }
        public List<Report> Reports { get; set; }
    }
}
