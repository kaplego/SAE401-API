using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Tests
{
    [TestClass()]
    public class ProduitsimilaireControllerTests
    {
        private _DBMilibooContext _context;
        private IProduitsimilaireRepository<Produitsimilaire> _repository;
        private ProduitsimilaireController _controller;
        private Produitsimilaire p1;


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
            _repository = new ProduitsimilaireManager(_context);
            _controller = new ProduitsimilaireController(_repository);



            p1 = new Produitsimilaire()
            {
                IdproduitRef = 1,
                IdproduitSim = 4
            };
            await _context.Produitsimilaires.AddAsync(p1);
            await _context.SaveChangesAsync();
        }

        [TestMethod()]
        public async Task PostProduitsimilaireTest_Normal()
        {

            ProduitsimilaireDTO p2 = new ProduitsimilaireDTO()
            {
                IdproduitRef = 2,
                IdproduitSim = 4
            };


            var result = await _controller.PostProduitsimilaire(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produitsimilaire?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Produitsimilaire valeur = (Produitsimilaire)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.IdproduitSim, valeur.IdproduitSim, "produit similaire égales");
            try { _context.Produitsimilaires.Remove(valeur); } catch { }
        }

        [TestMethod()]
        public async Task DeletProduitsimilaireTest_Normal()
        {

            Produitsimilaire p3 = new Produitsimilaire()
            {
                IdproduitRef = 3,
                IdproduitSim = 4
            };
            await _context.Produitsimilaires.AddAsync(p3);
            await _context.SaveChangesAsync();
            var result = await _controller.DeleteProduitsimilaire(p3.IdproduitRef, p3.IdproduitSim);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteProduitTest_Introuvable()
        {
            var result = await _controller.DeleteProduitsimilaire(0, 0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }





        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Produitsimilaires.Remove(p1);
            await _context.SaveChangesAsync();
        }
    }
}