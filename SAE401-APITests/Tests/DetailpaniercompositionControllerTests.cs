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
    public class DetailpaniercompositionControllerTests
    {
        private _DBMilibooContext _context;
        private IDetailPanierCompositionRepository<Detailpaniercomposition> _repository;
        private DetailpaniercompositionController _controller;
        private Client client1;
        private Detailpaniercomposition d1;

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
            _repository = new DetailpaniercompositionManager<Detailpaniercomposition>(_context);
            _controller = new DetailpaniercompositionController(_repository);
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
            d1 = new Detailpaniercomposition()
            {
                Idcomposition = 1,
                Idclient = client1.Idclient,
                Quantitepaniercomposition = 1

            };
            await _context.Detailpaniercompositions.AddAsync(d1);
            await _context.SaveChangesAsync();
            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);
        }

        [TestMethod()]
        public async Task PostDetailpaniercompositionTest_Normal()
        {
            DetailpaniercompositionDTO d2 = new DetailpaniercompositionDTO()
            {
                Idcomposition = 2,
                Idclient = client1.Idclient,
                Quantitepaniercomposition = 1
            };
            var result = await _controller.PostDetailPanierComposition(d2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpaniercomposition?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Detailpaniercomposition valeur = (Detailpaniercomposition)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(d2.Quantitepaniercomposition, valeur.Quantitepaniercomposition, "détail panier composition égales");
            try { _context.Detailpaniercompositions.Remove(valeur); } catch { }
        }



        [TestMethod()]
        public async Task PutDetailPanierCompositionTest_Normal()
        {
            DetailpaniercompositionDTO d3 = new DetailpaniercompositionDTO()
            {
                Idcomposition = 1,
                Idclient = client1.Idclient,
                Quantitepaniercomposition = 1
            };
            var result = await _controller.PutDetailPanoerComposition(d1.Idcomposition, d1.Idclient, d3);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpaniercomposition?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Detailpaniercomposition valeur = (Detailpaniercomposition)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(d3.Quantitepaniercomposition, valeur.Quantitepaniercomposition, "détail panier composition égales (titulaire)");
            Assert.AreEqual(d1.Idcomposition, valeur.Idcomposition, "Cartes bancaires non-modifiées (id)");
        }

        [TestMethod()]
        public async Task PutDetailPanierCompositionTest_Innégal()
        {
            DetailpaniercompositionDTO d4 = new DetailpaniercompositionDTO()
            {
                Idcomposition = 1,
                Idclient = client1.Idclient,
                Quantitepaniercomposition = 1
            };
            var result = await _controller.PutDetailPanoerComposition(-1, -1, d4);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpaniercomposition?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Résultat pas BadRequest");
        }

        [TestMethod()]
        public async Task PutDetailpaniercompositionTest_Introuvable()
        {
            DetailpaniercompositionDTO d5 = new DetailpaniercompositionDTO()
            {
                Idcomposition = -1,
                Idclient = -1,
                Quantitepaniercomposition = 1
            };
            var result = await _controller.PutDetailPanoerComposition(-1, -1, d5);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpaniercomposition?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }

        [TestMethod()]
        public async Task DeleteDetailpaniercompositionTest_Normal()
        {

            Detailpaniercomposition d6 = new Detailpaniercomposition()
            {
                Idcomposition = 3,
                Idclient = client1.Idclient,
                Quantitepaniercomposition = 1
            };

            await _context.Detailpaniercompositions.AddAsync(d6);
            await _context.SaveChangesAsync();
            var result = await _controller.DeleteDetailPanierComposition(d6.Idcomposition, d6.Idclient);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteDetailPanierCompositionTest_Introuvable()
        {
            var result = await _controller.DeleteDetailPanierComposition(0, 0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }




        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Detailpaniercompositions.Remove(d1);
            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }

    }
}