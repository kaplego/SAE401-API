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
    public class TransporteurControllerTests
    {
        private _DBMilibooContext _context;
        private ITransporteurRepository<Transporteur> _repository;
        private TransporteurController _controller;
        private Transporteur t1;


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
            _repository = new TransporteurManager(_context);
            _controller = new TransporteurController(_repository);



            t1 = new Transporteur()
            {
                Idtransporteur = 6,
                Nomtransporteur = "Test"
            };
            await _context.Transporteurs.AddAsync(t1);
            await _context.SaveChangesAsync();
        }


        [TestMethod()]
        public async Task GetAllTransporteursTest_Normal()
        {
            var transporteurs = await _controller.GetAllTransporteur();
            Assert.IsNotNull(transporteurs, "Retour est null");
            Assert.IsInstanceOfType(transporteurs, typeof(ActionResult<IEnumerable<Transporteur>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(transporteurs.Value, typeof(IEnumerable<Transporteur>), "Pas des transporteurs ");
            Assert.AreEqual(t1, transporteurs.Value.Last(), "transporteurs égales");
        }


        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Transporteurs.Remove(t1);
            await _context.SaveChangesAsync();
        }


    }
}