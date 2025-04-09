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
    public class PaiementControllerTests
    {

        private _DBMilibooContext _context;
        private IPaiementRepository<Paiement> _repository;
        private PaiementController _controller;
        private Client client1;
        private Paiement p1;
        private Commande c1;

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
            _repository = new PaiementManager<Paiement>(_context);
            _controller = new PaiementController(_repository);
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
            p1 = new Paiement()
            {
                Idcartebancaire = 21,
                Idcommande = 1,
                Idtypepaiement = 1,
                Datepaiement = DateTime.UtcNow,
                Montantpaiement = 10,
                Indicepaiement = "Test"
            };
            await _context.Paiements.AddAsync(p1);
            await _context.SaveChangesAsync();

            c1 = new Commande
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
            await _context.Commandes.AddAsync(c1);
            await _context.SaveChangesAsync();

            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);
        }


        [TestMethod()]
        public async Task PostPaiementTest_Normal()
        {
            PaiementDTO p2 = new PaiementDTO()
            {
                Idcartebancaire = 23,
                Idcommande = c1.Idcommande,
                Idtypepaiement = 1,
                Datepaiement = DateTime.UtcNow,
                Montantpaiement = 10,
                Indicepaiement = "Test"
            };

            var result = await _controller.PostPaiement(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Paiement?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Paiement valeur = (Paiement)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.Indicepaiement, valeur.Indicepaiement, "paiements égales");
            try { _context.Paiements.Remove(valeur); } catch { }
        }


        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostPaiementTest_Invalide()
        {
            PaiementDTO p3 = new PaiementDTO()
            {
                Idcartebancaire = 1152,
                Idcommande = c1.Idcommande,
                Idtypepaiement = 1,
                Datepaiement = DateTime.UtcNow,
                Montantpaiement = 10000000000000000000,
                Indicepaiement = "Test"
            };
            try
            {
                var result = await _controller.PostPaiement(p3);
            }
            catch (DbUpdateException ex)
            {
                _context.Paiements.Remove((Paiement)ex.Entries.First().Entity);
                throw ex;
            }
        }



        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Paiements.Remove(p1);
            _context.Commandes.Remove(c1);

            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }



    }
}