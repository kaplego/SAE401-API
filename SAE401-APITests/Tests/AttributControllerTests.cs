using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Tests
{
    [TestClass()]
    public class AttributControllerTests
    {
        private _DBMilibooContext _context;
        private IAttributRepository<Attributproduit> _repository;
        private AttributController _controller;
        private Attributproduit at1;
        private Typeproduit tp1;


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
            _repository = new AttributManager(_context);
            _controller = new AttributController(_repository);

            tp1 = new Typeproduit()
            {
                Idtypeproduit = 34,
                Idcategorie = 1,
                Nomtypeproduit = "Test"
            };
            await _context.Typeproduits.AddAsync(tp1);
            await _context.SaveChangesAsync();

            at1 = new Attributproduit()
            {
                Idattribut = 47,
                Idtypeproduit = 34,
                Nomattribut = "Test"
            };
            await _context.Attributproduits.AddAsync(at1);
            await _context.SaveChangesAsync();



        }

        [TestMethod()]
        public async Task GetAllAttributsByTypeProduitTest_Normal()
        {
            var attributs = await _controller.GetAllAttributByType(tp1.Idtypeproduit);
            Assert.IsNotNull(attributs, "Retour est null");
            Assert.IsInstanceOfType(attributs, typeof(ActionResult<IEnumerable<Attributproduit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(attributs.Value, typeof(IEnumerable<Attributproduit>), "Pas des attributs ");
            Assert.AreEqual(at1, attributs.Value.First(), "attributs égales");
        }

        [TestMethod()]
        public async Task GetAllAttributsByTypeProduitTest_Innexistant()
        {
            var attributs = await _controller.GetAllAttributByType(0);
            Assert.IsNotNull(attributs, "Retour est null");
            Assert.IsInstanceOfType(attributs, typeof(ActionResult<IEnumerable<Attributproduit>>), "Pas un ActionResult");

        }



        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Attributproduits.Remove(at1);
            _context.Typeproduits.Remove(tp1);
            await _context.SaveChangesAsync();
        }


    }
}