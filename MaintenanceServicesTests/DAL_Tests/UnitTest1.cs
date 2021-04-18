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
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesTests
{
    public class DAL_Tests
    {
        private DbContextOptions<ApplicationContext> _options;
        private EFGenericRepository<Employee> _employeeRepo;
        private EFGenericRepository<Position> _posRepo;

        [SetUp]
        public void Setup()
        {
            _options = GetDbContextOptions();
            _employeeRepo = new EFGenericRepository<Employee>(new ApplicationContext(_options));
            _posRepo = new EFGenericRepository<Position>(new ApplicationContext(_options));
        }

        [Test]
        public void TestAddEmployee()
        {
            GenericRepository<Employee> genericEmployeeRepository = new GenericRepository<Employee>(new SampleContextFactory());
            GenericRepository<Position> genericPositionRepository = new GenericRepository<Position>(new SampleContextFactory());

            Position testpos = genericPositionRepository.GetAll().Result.Where(item => item.Name == "Адмнинистратор").FirstOrDefault();

            Employee employee2 = new Employee 
            { 
                Login = "VladTest",
                Password = "TestPassword2",
                Name = "Ildar",
                Bday = DateTime.Now,
                PositionNavigation = testpos
            };

            genericEmployeeRepository.Create(employee2).Wait();

            /*IEnumerable<Employee> empls = genericEmployeeRepository.GetAll().Result;
            foreach (Employee e in empls)
            {
                Console.WriteLine($"{e.Id}.{e.Login} - {e.Password}");
            }*/
        }

        [Test]
        public void TestAddPriceList()
        {
            GenericRepository<PriceList> genericEmployeeRepository = new GenericRepository<PriceList>(new SampleContextFactory());

            PriceList priceList = new PriceList
            {
                CreationDate = DateTime.Now,
                Description = "TestAddPriceList"
            };

            genericEmployeeRepository.Create(priceList).Wait();
        }

        [Test]
        public void TestOptionConnecting()
        {

            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            
            using(ApplicationContext db = new ApplicationContext(options))
            {
                var employees = db.Employees.ToList();
                Console.WriteLine("Список объектов:");
                foreach (Employee e in employees)
                {
                    Console.WriteLine($"{e.Id}.{e.Login} - {e.Password}");
                }
            }
        }

        [Test]
        public void TestAdd()
        {
            using (ApplicationContext db = new ApplicationContext(_options))
            {
                Position p = db.Positions.FirstOrDefault(p => p.Name == "Адмнинистратор");
                Employee employee2 = new Employee { Login = "testVladLast3", Password = "TestPassword2", Name = "Ildar", Bday = DateTime.Now, PositionNavigation = p };

                // Добавление
                db.Employees.Add(employee2);
                db.SaveChanges();
            }
        }

        [Test]
        public void TestGet()
        {
            using (ApplicationContext db = new ApplicationContext(_options))
            {
                // получаем объекты из бд и выводим на консоль
                var employees = db.Employees.ToList();
                Console.WriteLine("Список объектов:");
                foreach (Employee e in employees)
                {
                    Console.WriteLine($"{e.Id}.{e.Login} - {e.Password}");
                }
            }
            Assert.Pass();
        }
    
        private DbContextOptions<ApplicationContext> GetDbContextOptions()
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            return options;
        }
    }
}