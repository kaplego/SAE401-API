using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;
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
    public class CommandeControllerTests
    {
        private _DBMilibooContext _context;
        private ICommandeRepository<Commande> _repository;
        private CommandeController _controller;
        private Client client1;
        private Commande cd1;

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
            _repository = new CommandeManager<Commande>(_context);
            _controller = new CommandeController(_repository);
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
            cd1 = new Commande()
            {
                Idclient = client1.Idclient,
                IdadresseLivr = 1,
                IdadresseFact = 1,
                Idcodepromo = 1,
                Idstatut = 1,
                Idtransporteur = 1,
                Datecommande = DateTime.UtcNow,
                Avecassurance = true,
                Aveclivraisonexpress = true,
                Instructionlivraison = "Test"
            };
            await _context.Commandes.AddAsync(cd1);
            await _context.SaveChangesAsync();
            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);
        }

        [TestMethod()]
        public async Task GetCommandeById_Normal()
        {
            var commande = await _controller.GetCommandeById(cd1.Idcommande);
            Assert.IsNotNull(commande, "Retour est null");
            Assert.IsInstanceOfType(commande, typeof(ActionResult<Commande>), "Pas un ActionResult");
            Assert.IsInstanceOfType(commande.Value, typeof(Commande), "Pas une coloration");
            Assert.AreEqual(cd1, commande.Value, "colorations égales");
        }

        [TestMethod()]
        public async Task GetCommandeById_Inexistant()
        {
            var commande = await _controller.GetCommandeById(0);
            Assert.IsNotNull(commande, "Retour est null");
            Assert.IsInstanceOfType(commande, typeof(ActionResult<Commande>), "Pas un ActionResult");
            Assert.IsNotNull(commande.Result, "Erreur est null");
            Assert.IsInstanceOfType(commande.Result, typeof(NotFoundResult), "Pas un NotFound");
            Assert.IsNull(commande.Value, "Valeur pas null");
        }


        [TestMethod()]
        public async Task PostCommandeTest_Normal()
        {
            CommandeDTO cd2 = new CommandeDTO()
            {
                Idclient = client1.Idclient,
                IdadresseLivr = 1,
                IdadresseFact = 1,
                Idcodepromo = 1,
                Idstatut = 1,
                Idtransporteur = 1,
                Datecommande = DateTime.UtcNow,
                Avecassurance = true,
                Aveclivraisonexpress = true,
                Instructionlivraison = "Test"
            };
            var result = await _controller.PostCommande(cd2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commande?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Commande valeur = (Commande)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(cd2.IdadresseLivr, valeur.IdadresseLivr, "commandes égales");
            try { _context.Commandes.Remove(valeur); } catch { }
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostCommandeTest_Invalide()
        {

            string longComment = new string('a', 2050);

            CommandeDTO cd3 = new CommandeDTO()
            {
                Idclient = client1.Idclient,
                IdadresseLivr = 1,
                IdadresseFact = 1,
                Idcodepromo = 1,
                Idstatut = 1,
                Idtransporteur = 1,
                Datecommande = DateTime.UtcNow,
                Avecassurance = true,
                Aveclivraisonexpress = true,
                Instructionlivraison = longComment
            };
            try
            {
                var result = await _controller.PostCommande(cd3);
            }
            catch (DbUpdateException ex)
            {
                try { _context.Commandes.RemoveRange(client1.CommandesNavigation.Where(x => x.Instructionlivraison == longComment)); }
                catch { throw ex; }
                throw ex;
            }
        }




        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Commandes.Remove(cd1);
            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }

    }
}