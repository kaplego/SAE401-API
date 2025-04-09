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
    public class RegroupementControllerTests
    {
        private _DBMilibooContext _context;
        private IRegroupementRepository<Regroupementproduit> _repository;
        private RegroupementController _controller;
        private Regroupementproduit r1;


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
            _repository = new RegroupementManager(_context);
            _controller = new RegroupementController(_repository);



            r1 = new Regroupementproduit()
            {
                Idregroupement = 5,
                Nomregroupement = "Test"
            };
            await _context.Regroupementproduits.AddAsync(r1);
            await _context.SaveChangesAsync();



        }

        [TestMethod()]
        public async Task GetAllRegroupementsTest_Normal()
        {
            var regroupements = await _controller.GetAllRegroupement();
            Assert.IsNotNull(regroupements, "Retour est null");
            Assert.IsInstanceOfType(regroupements, typeof(ActionResult<IEnumerable<Regroupementproduit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(regroupements.Value, typeof(IEnumerable<Regroupementproduit>), "Pas des regroupements ");
            Assert.AreEqual(r1, regroupements.Value.Last(), "regrouepements égales");
        }



        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Regroupementproduits.Remove(r1);
            await _context.SaveChangesAsync();
        }


    }
}