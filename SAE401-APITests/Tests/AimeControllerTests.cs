using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Tests
{
    [TestClass()]
    public class AimeControllerTests
    {
        private _DBMilibooContext _context;
        private IAimeRepository<Aime> _repository;
        private AimeController _controller;
        private Client client1;
        private Aime a1;

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
            _repository = new AimeManager<Aime>(_context);
            _controller = new AimeController(_repository);
            client1 = new Client()
            {
                Nomclient = "NOM",
                Prenomclient = "Prenom" + DateTime.UtcNow.ToString(),
                Emailclient = "email@email.email",
                Telportableclient = "33123456789",
                Datecreationcompte = DateTime.UtcNow,
                Hashmdp = "mdp",
                Pointfideliteclient = 0,
                Newslettermiliboo = true,
                Newsletterpartenaires = true
            };
            await _context.Clients.AddAsync(client1);
            await _context.SaveChangesAsync();
            a1 = new Aime()
            {
                Idclient = client1.Idclient,
                Idproduit = 1

            };
            await _context.Aimes.AddAsync(a1);
            await _context.SaveChangesAsync();
            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);
        }

        [TestMethod()]
        public async Task PostAimeTest_Normal()
        {
            AimeDTO a2 = new AimeDTO()
            {
                Idclient = client1.Idclient,
                Idproduit = 2

            };
            var result = await _controller.PostAime(a2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Aime?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Aime valeur = (Aime)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(a2.Idproduit, valeur.Idproduit, "Cartes bancaires égales");
            try { _context.Aimes.Remove(valeur); } catch { }
        }

        [TestMethod()]
        public async Task DeleteAimeTest_Normal()
        {
            Aime a3 = new Aime()
            {
                Idclient = client1.Idclient,
                Idproduit = 3
            };
            await _context.Aimes.AddAsync(a3);
            await _context.SaveChangesAsync();
            var result = await _controller.DeleteAime(a3.Idclient, a3.Idproduit);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteAimeTest_Introuvable()
        {
            var result = await _controller.DeleteAime(0, 0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }


        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Aimes.RemoveRange(client1.AimesNavigation);
            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }

    }
}