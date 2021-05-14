using System;
using System.Collections.Generic;
using System.Text;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;

namespace VDemyanov.MaintenanceServices.DAL.Services
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationContext _db = new ApplicationContextFactory().CreateDbContext();

        private EFGenericRepository<Employee> _employeeRep;
        private EFGenericRepository<Position> _positionRep;
        private EFGenericRepository<Contract> _contractRep;
        private EFGenericRepository<ContractCategory> _contractCategoryRep;
        private EFGenericRepository<EquipmentCategory> _equipmentCategoryRep;
        private EFGenericRepository<PriceList> _priceListRep;
        private EFGenericRepository<Service> _serviceRep;
        private EFGenericRepository<ServiceEquipment> _serviceEquipmentRep;
        private EquipmentRepository _equipmentRep;
        private ReportRepository _reportRep;
        private ReportDataRepository _reportDataRep;

        public EFGenericRepository<Employee> EmployeeRep
        {
            get
            {
                if (_employeeRep == null)
                    _employeeRep = new EFGenericRepository<Employee>(_db);
                return _employeeRep;
            }
        }

        public EFGenericRepository<Position> PositionRep
        {
            get
            {
                if (_positionRep == null)
                    _positionRep = new EFGenericRepository<Position>(_db);
                return _positionRep;
            }
        }

        public EFGenericRepository<Contract> ContractRep
        {
            get
            {
                if (_contractRep == null)
                    _contractRep = new EFGenericRepository<Contract>(_db);
                return _contractRep;
            }
        }

        public EFGenericRepository<ContractCategory> ContractCategoryRep
        {
            get
            {
                if (_contractCategoryRep == null)
                    _contractCategoryRep = new EFGenericRepository<ContractCategory>(_db);
                return _contractCategoryRep;
            }
        }

        public EFGenericRepository<EquipmentCategory> EquipmentCategoryRep
        {
            get
            {
                if (_equipmentCategoryRep == null)
                    _equipmentCategoryRep = new EFGenericRepository<EquipmentCategory>(_db);
                return _equipmentCategoryRep;
            }
        }

        public EFGenericRepository<PriceList> PriceListRep
        {
            get
            {
                if (_priceListRep == null)
                    _priceListRep = new EFGenericRepository<PriceList>(_db);
                return _priceListRep;
            }
        }

        public EFGenericRepository<Service> ServiceRep
        {
            get
            {
                if (_serviceRep == null)
                    _serviceRep = new EFGenericRepository<Service>(_db);
                return _serviceRep;
            }
        }

        public EFGenericRepository<ServiceEquipment> ServiceEquipmentRep
        {
            get
            {
                if (_serviceEquipmentRep == null)
                    _serviceEquipmentRep = new EFGenericRepository<ServiceEquipment>(_db);
                return _serviceEquipmentRep;
            }
        }

        public EquipmentRepository EquipmentRep
        {
            get
            {
                if (_equipmentRep == null)
                    _equipmentRep = new EquipmentRepository(_db);
                return _equipmentRep;
            }
        }

        public ReportRepository ReportRep
        {
            get
            {
                if (_reportRep == null)
                    _reportRep = new ReportRepository(_db);
                return _reportRep;
            }
        }

        public ReportDataRepository ReportDataRep
        {
            get
            {
                if (_reportDataRep == null)
                    _reportDataRep = new ReportDataRepository(_db);
                return _reportDataRep;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
