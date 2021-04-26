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
        private EFGenericRepository<Equipment> _equipmentRep;
        private EFGenericRepository<EquipmentCategory> _equipmentCategoryRep;
        private EFGenericRepository<PriceList> _priceListRep;
        private EFGenericRepository<Report> _reportRep;
        private EFGenericRepository<ReportData> _reportDataRep;
        private EFGenericRepository<Service> _serviceRep;
        private EFGenericRepository<ServiceEquipment> _serviceEquipmentRep;

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

        public EFGenericRepository<Equipment> EquipmentRep
        {
            get
            {
                if (_equipmentRep == null)
                    _equipmentRep = new EFGenericRepository<Equipment>(_db);
                return _equipmentRep;
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

        public EFGenericRepository<Report> ReportRep
        {
            get
            {
                if (_reportRep == null)
                    _reportRep = new EFGenericRepository<Report>(_db);
                return _reportRep;
            }
        }

        public EFGenericRepository<ReportData> ReportDataRep
        {
            get
            {
                if (_reportDataRep == null)
                    _reportDataRep = new EFGenericRepository<ReportData>(_db);
                return _reportDataRep;
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
