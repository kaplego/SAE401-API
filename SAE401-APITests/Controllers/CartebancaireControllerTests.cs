using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.DTO;
using SAE401_API.Models.Repository;
using SAE401_API.Models.DataMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using DotNetEnv;
using Newtonsoft.Json.Linq;

namespace SAE401_API.Controllers.Tests
{
    [TestClass()]
    public class CartebancaireControllerTests
    {
        private _DBMilibooContext _context;
        private ICartebancaireRepository<Cartebancaire> _repository;
        private CartebancaireController _controller;
        private Client client1;
        private Cartebancaire cb1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            Env.Load(Path.Combine(
                Directory.GetParent(Directory.GetParent(
                Directory.GetParent(Directory.GetCurrentDirectory()
                .ToString()).ToString()).ToString()).ToString(),".env"));
            var builder = new DbContextOptionsBuilder<_DBMilibooContext>().UseNpgsql(
                Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            _context = new _DBMilibooContext(builder.Options);
            _repository = new CartebancaireManager<Cartebancaire>(_context);
            _controller = new CartebancaireController(_repository);
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
            cb1 = new Cartebancaire()
            {
                Idclient = client1.Idclient,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Nom1",
                Numcartebancaire = "1111222233334444",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1),
            };
            await _context.Cartebancaires.AddAsync(cb1);
            await _context.SaveChangesAsync();
            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);
        }

        [TestMethod()]
        public async Task GetAllCartebancaireByClientTest_ClientInnexistant()
        {
            var carteBancaires = await _controller.GetAllCartebancaireByClient(0);
            Assert.IsNotNull(carteBancaires, "Retour est null");
            Assert.IsInstanceOfType(carteBancaires, typeof(ActionResult<IEnumerable<Cartebancaire>>), "Pas un ActionResult");
            Assert.IsNotNull(carteBancaires.Result, "Erreur est null");
            Assert.IsInstanceOfType(carteBancaires.Result, typeof(NotFoundResult), "Pas un NotFound");
            Assert.IsNull(carteBancaires.Value, "Valeur pas null");
        }

        [TestMethod()]
        public async Task GetAllCartebancaireByClientTest_Normal()
        {
            var carteBancaires = await _controller.GetAllCartebancaireByClient(client1.Idclient);
            Assert.IsNotNull(carteBancaires, "Retour est null");
            Assert.IsInstanceOfType(carteBancaires, typeof(ActionResult<IEnumerable<Cartebancaire>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(carteBancaires.Value, typeof(IEnumerable<Cartebancaire>), "Pas des cartes bancaires");
            Assert.AreEqual(cb1, carteBancaires.Value.First(), "Cartes bancaires égales");
        }

        [TestMethod()]
        public async Task PostCartebancaireTest()
        {
            CartebancaireDTO cb2 = new CartebancaireDTO()
            {
                Idclient = client1.Idclient,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Nom2",
                Numcartebancaire = "4444333322221111",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
            };
            var result = await _controller.PostCartebancaire(cb2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Cartebancaire?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Cartebancaire valeur = (Cartebancaire)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(cb2.Numcartebancaire, valeur.Numcartebancaire, "Cartes bancaires égales");
            try { _context.Cartebancaires.Remove(valeur); } catch { }
        }

        [TestMethod()]
        public async Task PutCartebancaireTest()
        {
            CartebancaireDTO cb3 = new CartebancaireDTO()
            {
                Idcartebancaire = cb1.Idcartebancaire,
                Idclient = cb1.Idclient,
                Dateenregistement = cb1.Dateenregistement,
                Titulairecartebancaire = "Test",
                Numcartebancaire = cb1.Numcartebancaire,
                Dateexpirationcarte = DateTime.UtcNow.AddDays(10)
            };
            var result = await _controller.PutCartebancaire(cb1.Idcartebancaire, cb3);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Cartebancaire?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Cartebancaire valeur = (Cartebancaire)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(cb3.Titulairecartebancaire, valeur.Titulairecartebancaire, "Cartes bancaires égales (num)");
            Assert.AreEqual(cb1.Idcartebancaire, valeur.Idcartebancaire, "Cartes bancaires non-modifiées (id)");
            Assert.AreEqual(cb3.Dateexpirationcarte, valeur.Dateexpirationcarte, "Cartes bancaires égales (dateexp)");
        }

        [TestMethod()]
        public async Task DeleteCartebancaireTest()
        {
            Cartebancaire cb4 = new Cartebancaire()
            {
                Idclient = cb1.Idclient,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Obama",
                Numcartebancaire = "1111000011110000",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(10)
            };
            await _context.Cartebancaires.AddAsync(cb4);
            await _context.SaveChangesAsync();
            var result = await _controller.DeleteCartebancaire(cb4.Idcartebancaire);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestCleanup()]
        public async Task TestCleanup()
        {
            _context.Cartebancaires.Remove(cb1);
            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }
    }
}