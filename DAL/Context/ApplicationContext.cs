using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VDemyanov.MaintenanceServices.Domain.Models.AuthorizationEntities;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;

namespace VDemyanov.MaintenanceServices.DAL.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractCategory> ContractCategories { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportData> ReportDataSet { get; set; }
        public DbSet<Service> Services { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
