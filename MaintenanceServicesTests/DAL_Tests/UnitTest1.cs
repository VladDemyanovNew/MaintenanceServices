using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using VDemyanov.MaintenanceServices.DAL;
using VDemyanov.MaintenanceServices.DAL.Context;
using VDemyanov.MaintenanceServices.Domain.Models.MainServiceEntities;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesTests
{
    public class DAL_Tests
    {
        [SetUp]
        public void Setup()
        {
            /*var builder = new ConfigurationBuilder();
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
                .Options;*/


            /*const string connection = "Server = (localdb)\\mssqllocaldb; Database = MAINTENANCE_SERVICE; Trusted_Connection = True;";
            using var db = new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(connection).Options);*/
            
           
        }

        [Test]
        public void Test2()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Employee employee2 = new Employee { Login = "IDemyanov", Password = "TestPassword2", Name = "Ildar", Bday = DateTime.Now, PositionNavigation = db.Positions.FirstOrDefault(p => p.Name == "Admin") };

                // Добавление
                db.Employees.Add(employee2);
                db.SaveChanges();
            }
        }

        [Test]
        public void Test1()
        {
            using (ApplicationContext db = new ApplicationContext())
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
    }
}