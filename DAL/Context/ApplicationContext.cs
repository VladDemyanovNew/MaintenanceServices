using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;

#nullable disable

namespace VDemyanov.MaintenanceServices.DAL.Context
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractCategory> ContractCategories { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<PriceList> PriceLists { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportData> ReportData { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceEquipment> ServiceEquipments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LENOVO_R2500U;Database=MAINTENANCE_SERVICE;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("CONTRACT");

                entity.HasIndex(e => e.Name, "UQ__CONTRACT__D9C1FA00F1D7EC60")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasColumnName("CATEGORY");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(100)
                    .HasColumnName("CLIENT_NAME");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("date")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.FacilityAddress)
                    .HasMaxLength(200)
                    .HasColumnName("FACILITY_ADDRESS");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("NAME")
                    .IsFixedLength(true);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("CONTRACT_CATEGORY_FK");
            });

            modelBuilder.Entity<ContractCategory>(entity =>
            {
                entity.ToTable("CONTRACT_CATEGORY");

                entity.HasIndex(e => e.Description, "UQ__CONTRACT__4193D92E30ED3023")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__CONTRACT__D9C1FA00534C47D2")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("NAME")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("EMPLOYEE");

                entity.HasIndex(e => e.Login, "UQ__EMPLOYEE__E39E2665A0764FBA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AvatarPath)
                    .HasMaxLength(100)
                    .HasColumnName("AVATAR_PATH");

                entity.Property(e => e.Bday)
                    .HasColumnType("date")
                    .HasColumnName("BDAY");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOGIN");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("NAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Position).HasColumnName("POSITION");

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Position)
                    .HasConstraintName("POSITION_FK");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("EQUIPMENT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasColumnName("CATEGORY");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("EQUIPMENT_CATEGORY_FK");
            });

            modelBuilder.Entity<EquipmentCategory>(entity =>
            {
                entity.ToTable("EQUIPMENT_CATEGORY");

                entity.HasIndex(e => e.Description, "UQ__EQUIPMEN__4193D92EE3C083BC")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__EQUIPMEN__D9C1FA00A794AA4D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("NAME")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("POSITION");

                entity.HasIndex(e => e.Name, "UQ__POSITION__D9C1FA00E7775FB3")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<PriceList>(entity =>
            {
                entity.ToTable("PRICE_LIST");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("date")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .HasColumnName("DESCRIPTION");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("REPORT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Contract).HasColumnName("CONTRACT");

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.HasOne(d => d.ContractNavigation)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.Contract)
                    .HasConstraintName("CONTRACT_FK");
            });

            modelBuilder.Entity<ReportData>(entity =>
            {
                entity.ToTable("REPORT_DATA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Number).HasColumnName("NUMBER");

                entity.Property(e => e.Report).HasColumnName("REPORT");

                entity.Property(e => e.ServiceEquipment).HasColumnName("SERVICE_EQUIPMENT");

                entity.HasOne(d => d.ReportNavigation)
                    .WithMany(p => p.ReportData)
                    .HasForeignKey(d => d.Report)
                    .HasConstraintName("REPORT_FK");

                entity.HasOne(d => d.ServiceEquipmentNavigation)
                    .WithMany(p => p.ReportData)
                    .HasForeignKey(d => d.ServiceEquipment)
                    .HasConstraintName("SERVICE_EQUIPMENT_FK");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("SERVICE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.Notation)
                    .HasMaxLength(300)
                    .HasColumnName("NOTATION");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("PRICE");

                entity.Property(e => e.PriceList).HasColumnName("PRICE_LIST");

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .HasColumnName("UNIT")
                    .IsFixedLength(true);

                entity.HasOne(d => d.PriceListNavigation)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.PriceList)
                    .HasConstraintName("PRICE_LIST_FK");
            });

            modelBuilder.Entity<ServiceEquipment>(entity =>
            {
                entity.ToTable("SERVICE_EQUIPMENT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EquipmentId).HasColumnName("EQUIPMENT_ID");

                entity.Property(e => e.ServiceId).HasColumnName("SERVICE_ID");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.ServiceEquipments)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("EQUIPMENT_ID_FK");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceEquipments)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("SERVICE_ID_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
