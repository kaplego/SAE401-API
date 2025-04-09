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
    public class PaysControllerTests
    {
        private _DBMilibooContext _context;
        private IPaysRepository<Pay> _repository;
        private PaysController _controller;
        private Pay p1;


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
            _repository = new PaysManager(_context);
            _controller = new PaysController(_repository);



            p1 = new Pay()
            {
                Idpays = 7,
                Nompays = "Test"
            };
            await _context.Pays.AddAsync(p1);
            await _context.SaveChangesAsync();



        }

        [TestMethod()]
        public async Task GetAllPaysTest_Normal()
        {
            var pays = await _controller.GetAllPays();
            Assert.IsNotNull(pays, "Retour est null");
            Assert.IsInstanceOfType(pays, typeof(ActionResult<IEnumerable<Pay>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(pays.Value, typeof(IEnumerable<Pay>), "Pas des pays ");
            Assert.AreEqual(p1, pays.Value.Last(), "pays égales");
        }



        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Pays.Remove(p1);
            await _context.SaveChangesAsync();
        }
    }
}