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
    public class TypePaiementControllerTests
    {
        private _DBMilibooContext _context;
        private ITypePaiementRepository<Typepaiement> _repository;
        private TypePaiementController _controller;
        private Typepaiement t1;


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
            _repository = new TypePaiementManager(_context);
            _controller = new TypePaiementController(_repository);



            t1 = new Typepaiement()
            {
                Idtypepaiement = 4,
                Nomtypepaiement = "Test"

            };
            await _context.Typepaiements.AddAsync(t1);
            await _context.SaveChangesAsync();
        }


        [TestMethod()]
        public async Task GetAllTypePaiementsTest_Normal()
        {
            var typepaiements = await _controller.GetAllTypePaiement();
            Assert.IsNotNull(typepaiements, "Retour est null");
            Assert.IsInstanceOfType(typepaiements, typeof(ActionResult<IEnumerable<Typepaiement>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(typepaiements.Value, typeof(IEnumerable<Typepaiement>), "Pas des type paiements ");
            Assert.AreEqual(t1, typepaiements.Value.Last(), "type paiements égales");
        }


        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Typepaiements.Remove(t1);
            await _context.SaveChangesAsync();
        }
    }
}