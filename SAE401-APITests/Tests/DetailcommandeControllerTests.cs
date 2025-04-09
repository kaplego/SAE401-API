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
    public class DetailcommandeControllerTests
    {
        private _DBMilibooContext _context;
        private IDetailcommandeRepository<Detailcommande> _repository;
        private DetailcommandeController _controller;
        private Client client1;
        private Commande cmd1;
        private Detailcommande detail1;


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
            _repository = new DetailcommandeManager<Detailcommande>(_context);
            _controller = new DetailcommandeController(_repository);
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
            cmd1 = new Commande()
            {
                Idclient = client1.Idclient,
                IdadresseLivr = 1,
                IdadresseFact = 1,
                Idstatut = 1,
                Idtransporteur = 1,
                Avecassurance = true,
                Aveclivraisonexpress = true
            };
            await _context.Commandes.AddAsync(cmd1);
            await _context.SaveChangesAsync();
            detail1 = new Detailcommande()
            {
                Idproduit = 1,
                Idcouleur = 7,
                Idcommande = cmd1.Idcommande,
                Quantitecommande = 1
            };
            await _context.Detailcommandes.AddAsync(detail1);
            await _context.SaveChangesAsync();
            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);
        }



        [TestMethod()]
        public async Task PostDetailcommandeTest_Normal()
        {
            DetailcommandeDTO c2 = new DetailcommandeDTO()
            {
                Idproduit = 1,
                Idcouleur = 5,
                Idcommande = cmd1.Idcommande,
                Quantitecommande = 1
            };
            var result = await _controller.PostDetailcommande(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailcommande?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Detailcommande valeur = (Detailcommande)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c2.Quantitecommande, valeur.Quantitecommande, "detail commandes égales");
            try { _context.Detailcommandes.Remove(valeur); } catch { }
        }






        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Detailcommandes.Remove(detail1);
            _context.Commandes.Remove(cmd1);
            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }
    }
}