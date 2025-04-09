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
    public class VilleControllerTests
    {
        private _DBMilibooContext _context;
        private IVilleRepository<Ville> _repository;
        private VilleController _controller;
        private Ville v1;


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
            _repository = new VilleManager(_context);
            _controller = new VilleController(_repository);

            v1 = new Ville()
            {
                Codeinsee = "99183",
                Nomville = "Test"

            };
            await _context.Villes.AddAsync(v1);
            await _context.SaveChangesAsync();

        }



        [TestMethod()]
        public async Task GetAllVillesTest_Normal()
        {
            var villes = await _controller.GetAllVille();
            Assert.IsNotNull(villes, "Retour est null");
            Assert.IsInstanceOfType(villes, typeof(ActionResult<IEnumerable<Ville>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(villes.Value, typeof(IEnumerable<Ville>), "Pas des villes ");
            Assert.AreEqual(v1, villes.Value.Last(), "villes égales");
        }


        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Villes.Remove(v1);
            await _context.SaveChangesAsync();
        }


    }




}