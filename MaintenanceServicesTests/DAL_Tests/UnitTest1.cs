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

                // ����������
                db.Employees.Add(employee2);
                db.SaveChanges();
            }
        }

        [Test]
        public void Test1()
        {
            using (ApplicationContext db = new ApplicationContext())
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
    }
}