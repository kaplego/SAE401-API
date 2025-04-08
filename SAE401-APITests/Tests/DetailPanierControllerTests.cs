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
    public class DetailPanierControllerTests
    {
        private _DBMilibooContext _context;
        private IDetailPanierRepository<Detailpanier> _repository;
        private DetailPanierController _controller;
        private Client client1;
        private Detailpanier d1;

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
            _repository = new DetailPanierManager<Detailpanier>(_context);
            _controller = new DetailPanierController(_repository);
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
            d1 = new Detailpanier()
            {
                Idproduit = 1,
                Idcouleur = 7,
                Idclient = client1.Idclient,
                Quantitepanier = 1

            };
            await _context.Detailpaniers.AddAsync(d1);
            await _context.SaveChangesAsync();
            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);
        }



        [TestMethod()]
        public async Task PostDetailpanierTest_Normal()
        {
            DetailpanierDTO d2 = new DetailpanierDTO()
            {
                Idproduit = 1,
                Idcouleur = 8,
                Idclient = client1.Idclient,
                Quantitepanier = 1

            };
            var result = await _controller.PostDetailPanier(d2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpanier?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Detailpanier valeur = (Detailpanier)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(d2.Quantitepanier, valeur.Quantitepanier, "détail panier composition égales");
            try { _context.Detailpaniers.Remove(valeur); } catch { }
        }

        [TestMethod()]
        public async Task PutDetailPanierTest_Normal()
        {
            DetailpanierDTO d3 = new DetailpanierDTO()
            {
                Idproduit = d1.Idproduit,
                Idcouleur = d1.Idcouleur,
                Idclient = client1.Idclient,
                Quantitepanier = 2

            };
            var result = await _controller.PutDetailPanier(d1.Idproduit, d1.Idcouleur, d1.Idclient, d3);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpanier?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Detailpanier valeur = (Detailpanier)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(d3.Quantitepanier, valeur.Quantitepanier, "détail panier  égales (titulaire)");
            Assert.AreEqual(d1.Idcouleur, valeur.Idcouleur, "détail panier non-modifiées (id)");
            Assert.AreEqual(d1.Idproduit, valeur.Idproduit, "détail panier non-modifiées (id)");

        }


        [TestMethod()]
        public async Task PutDetailPanierTest_Innégal()
        {
            DetailpanierDTO d4 = new DetailpanierDTO()
            {
                Idproduit = d1.Idproduit,
                Idcouleur = d1.Idcouleur,
                Idclient = client1.Idclient,
                Quantitepanier = 2

            };
            var result = await _controller.PutDetailPanier(-1, -1, -1, d4);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpanier?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Résultat pas BadRequest");
        }


        [TestMethod()]
        public async Task PutDetailpanierTest_Introuvable()
        {
            DetailpanierDTO d5 = new DetailpanierDTO()
            {
                Idproduit = -1,
                Idcouleur = -1,
                Idclient = -1,
                Quantitepanier = 2

            };
            var result = await _controller.PutDetailPanier(-1, -1, -1, d5);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpanier?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }

        [TestMethod()]
        public async Task DeleteDetailpanierTest_Normal()
        {

            Detailpanier d6 = new Detailpanier()
            {
                Idproduit = 1,
                Idcouleur = 5,
                Idclient = client1.Idclient,
                Quantitepanier = 1

            };
            await _context.Detailpaniers.AddAsync(d6);
            await _context.SaveChangesAsync();
            var result = await _controller.DeleteDetailPanier(d6.Idproduit, d6.Idcouleur, d6.Idclient);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteDetailPanierTest_Introuvable()
        {
            var result = await _controller.DeleteDetailPanier(0, 0, 0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }



        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Detailpaniers.Remove(d1);
            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }


    }
}