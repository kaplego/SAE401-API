using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE401_APITests.Tests
{
    [TestClass()]
    public class DepartementControllerTests
    {
        [TestClass()]
        public class CodePromoControllerTests
        {
            private _DBMilibooContext _context;
            private IDepartementRepository<Departement> _repository;
            private DepartementController _controller;
            private Departement d1;


            [TestInitialize]
            public async Task TestInitialize()
            {
                Env.Load(Path.Combine(
                    Directory.GetParent(Directory.GetParent(
                    Directory.GetParent(Directory.GetCurrentDirectory()
                    .ToString()).ToString()).ToString()).ToString(), ".env"));
                var builder = new DbContextOptionsBuilder<_DBMilibooContext>().UseNpgsql(
                    Environment.GetEnvironmentVariable("CONNECTION_STRING"))
                    .EnableSensitiveDataLogging(true);
                _context = new _DBMilibooContext(builder.Options);
                _repository = new DepartementManager(_context);
                _controller = new DepartementController(_repository);



                d1 = new Departement()
                {
                    Iddepartement = 977,
                    Nomdepartement = "Test"

                };
                await _context.Departements.AddAsync(d1);
                await _context.SaveChangesAsync();



            }

            [TestMethod()]
            public async Task GetAllDdepartementsTest_Normal()
            {
                var departements = await _controller.GetAllDepartement();
                Assert.IsNotNull(departements, "Retour est null");
                Assert.IsInstanceOfType(departements, typeof(ActionResult<IEnumerable<Departement>>), "Pas un ActionResult");
                Assert.IsInstanceOfType(departements.Value, typeof(IEnumerable<Departement>), "Pas des departements ");
                Assert.AreEqual(d1, departements.Value.Last(), "departements égales");
            }



            [TestCleanup()]
            public async Task TestCleanup()
            {
                await _context.SaveChangesAsync();
                _context.Departements.Remove(d1);
                await _context.SaveChangesAsync();
            }
        }
    }
}