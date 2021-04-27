using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VDemyanov.MaintenanceServices.DAL;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.DAL.Services;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesTests
{
    public class DAL_Tests
    {
        //private DbContextOptions<ApplicationContext> _options;
        private UnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            //_options = GetDbContextOptions();
            _unitOfWork = new UnitOfWork();
        }

        [Test]
        public void TestingGetAll()
        {
            List<Contract> contrcts = _unitOfWork.ContractRep.GetAllAsync().Result.ToList();
            foreach (Contract c in contrcts)
            {
                Console.WriteLine(c.Name);
            }
        }

        [Test]
        public void TestingRemove()
        {
            _unitOfWork.ContractRep.Remove(8);
            _unitOfWork.Save();
        }

        /*
        #region CRUD testing with repository
        [Test]
        public void TestContracts()
        {
            
            List<Contract> contrcts = _contractRep.GetAllAsync().Result.ToList();

            foreach(Contract c in contrcts)
            {
                Console.WriteLine(c.Name);
            }
        }

        [Test]
        public void TestAddEmployee()
        {
           
            Position pos = _positionRep.GetAllAsync().Result.Where(item => item.Name == "��������������").FirstOrDefault();

            Employee employee2 = new Employee 
            { 
                Login = "TestLogin",
                Password = "TestPassword",
                Name = "TestName",
                Bday = DateTime.Now,
                PositionNavigation = pos
            };

            _employeeRep.AddAsync(employee2).Wait();
        }

        [Test]
        public void TestRemoveEmployee()
        {
            _employeeRep.RemoveAsync(3).Wait();
        }

        [Test]
        public void TestGetById()
        {
            Employee empl = _employeeRep.GetAsync(7).Result;
            Console.WriteLine(empl.Login);
        }

        [Test]
        public void TestUpdate()
        {
            Employee empl = _employeeRep.GetAsync(8).Result;
            empl.Name = "TestUpdate";
            _employeeRep.UpdateAsync(empl).Wait();
        }
        #endregion

        #region CRUD testing without repository 
        [Test]
        public void TestAdd()
        {
            using (ApplicationContext db = new ApplicationContext(_options))
            {
                Position p = db.Positions.FirstOrDefault(p => p.Name == "��������������");
                Employee employee2 = new Employee { Login = "testVladLast3", Password = "TestPassword2", Name = "Ildar", Bday = DateTime.Now, PositionNavigation = p };

                // ����������
                db.Employees.Add(employee2);
                db.SaveChanges();
            }
        }
 
        [Test]
        public void TestGet()
        {
            using (ApplicationContext db = new ApplicationContext(_options))
            {
                // �������� ������� �� �� � ������� �� �������
                var employees = db.Employees.ToList();
                Console.WriteLine("������ ��������:");
                foreach (Employee e in employees)
                {
                    Console.WriteLine($"{e.Id}.{e.Login} - {e.Password}");
                }
            }
            Assert.Pass();
        }
        #endregion

        #region option connecting testing
        [Test]
        public void TestOptionConnecting()
        {

            var builder = new ConfigurationBuilder();
            // ��������� ���� � �������� ��������
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // �������� ������������ �� ����� appsettings.json
            builder.AddJsonFile("appsettings.json");
            // ������� ������������
            var config = builder.Build();
            // �������� ������ �����������
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                var employees = db.Employees.ToList();
                Console.WriteLine("������ ��������:");
                foreach (Employee e in employees)
                {
                    Console.WriteLine($"{e.Id}.{e.Login} - {e.Password}");
                }
            }
        }
        #endregion

        private DbContextOptions<ApplicationContext> GetDbContextOptions()
        {
            var builder = new ConfigurationBuilder();
            // ��������� ���� � �������� ��������
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // �������� ������������ �� ����� appsettings.json
            builder.AddJsonFile("appsettings.json");
            // ������� ������������
            var config = builder.Build();
            // �������� ������ �����������
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            return options;
        }*/
    }
}