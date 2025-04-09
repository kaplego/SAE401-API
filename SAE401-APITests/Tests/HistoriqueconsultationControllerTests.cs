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
    public class HistoriqueconsultationControllerTests
    {
        private _DBMilibooContext _context;
        private IHistoriqueconsultationRepository<Historiqueconsultation> _repository;
        private HistoriqueconsultationController _controller;
        private Historiqueconsultation h1;
        private Client client1;



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
            _repository = new HistoriqueconsultationManager<Historiqueconsultation>(_context);
            _controller = new HistoriqueconsultationController(_repository);
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
            h1 = new Historiqueconsultation()
            {
                Idproduit = 1,
                Idclient = 1,
                Dateconsultation = DateTime.UtcNow
            };
            await _context.Historiqueconsultations.AddAsync(h1);
            await _context.SaveChangesAsync();
            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);

        }


        [TestMethod()]
        public async Task PostHistoriqueTest_Normal()
        {
            HistoriqueconsultationDTO h2 = new HistoriqueconsultationDTO()
            {
                Idproduit = 2,
                Idclient = client1.Idclient,
                Dateconsultation = DateTime.UtcNow
            };
            var result = await _controller.PostHistoriqueconsultation(h2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Historiqueconsultation?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Historiqueconsultation valeur = (Historiqueconsultation)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(h2.Dateconsultation, valeur.Dateconsultation, "historique de consultation égales");
            try { _context.Historiqueconsultations.Remove(valeur); } catch { }
        }

        [TestMethod()]
        public async Task DeleteHistoriqueTest_Normal()
        {

            Historiqueconsultation h3 = new Historiqueconsultation()
            {
                Idproduit = 3,
                Idclient = client1.Idclient,
                Dateconsultation = DateTime.UtcNow
            };

            await _context.Historiqueconsultations.AddAsync(h3);
            await _context.SaveChangesAsync();
            var result = await _controller.DeleteHistoriqueconsultation(h3.Idproduit, h3.Idclient);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteHistoriqueTest_Introuvable()
        {
            var result = await _controller.DeleteHistoriqueconsultation(0, 0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }




        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Historiqueconsultations.Remove(h1);

            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }
    }
}