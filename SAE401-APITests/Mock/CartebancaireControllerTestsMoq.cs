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
using Npgsql;
using NuGet.Protocol;

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class CartebancaireControllerTestsMoq
    {
        private Mock<ICartebancaireRepository<Cartebancaire>> _repository;
        private CartebancaireController _controller;
        private Cartebancaire cb1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<ICartebancaireRepository<Cartebancaire>>();
            _controller = new CartebancaireController(_repository.Object);
            cb1 = new Cartebancaire()
            {
                Idcartebancaire = 1,
                Idclient = 0,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Nom1",
                Numcartebancaire = "1111222233334444",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1),
            };
            _repository.Setup(x => x.GetCartebancaireByIdAsync(1)).ReturnsAsync(new ActionResult<Cartebancaire?>(cb1));
            _repository.Setup(x => x.GetCartebancaireByIdAsync(0)).ReturnsAsync(new ActionResult<Cartebancaire?>(value: null));
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email",
            });
        }

        [TestMethod()]
        public async Task GetAllCartebancaireByClientTest_Normal()
        {
            _repository.Setup(x => x.GetAllCartebancaireByClientAsync(0)).ReturnsAsync(new ActionResult<IEnumerable<Cartebancaire>>(new List<Cartebancaire>() { cb1 }));
            var carteBancaires = await _controller.GetAllCartebancaireByClient(0);
            Assert.IsNotNull(carteBancaires, "Retour est null");
            Assert.IsInstanceOfType(carteBancaires, typeof(ActionResult<IEnumerable<Cartebancaire>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(carteBancaires.Value, typeof(IEnumerable<Cartebancaire>), "Pas des cartes bancaires");
            Assert.AreEqual(cb1, carteBancaires.Value.First(), "Cartes bancaires égales");
        }
        
        [TestMethod()]
        public async Task PostCartebancaireTest_Normal()
        {
            CartebancaireDTO cb2 = new CartebancaireDTO()
            {
                Idcartebancaire = 0,
                Idclient = 0,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Nom2",
                Numcartebancaire = "4444333322221111",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
            };
            Cartebancaire cb3 = new Cartebancaire()
            {
                Idcartebancaire = (int)cb2.Idcartebancaire,
                Idclient = cb2.Idclient,
                Dateenregistement = cb2.Dateenregistement,
                Titulairecartebancaire = cb2.Titulairecartebancaire,
                Numcartebancaire = cb2.Numcartebancaire,
                Dateexpirationcarte = cb2.Dateexpirationcarte
            };
            _repository.Setup(x => x.AddCartebancaireAsync(cb3)).ReturnsAsync(cb3);
            var result = await _controller.PostCartebancaire(cb2);

            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Cartebancaire?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Cartebancaire valeur = (Cartebancaire)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(cb2.Numcartebancaire, valeur.Numcartebancaire, "Cartes bancaires égales");
        }

        [TestMethod()]
        public async Task PutCartebancaireTest_Normal()
        {
            CartebancaireDTO cb2 = new CartebancaireDTO()
            {
                Idcartebancaire = cb1.Idcartebancaire,
                Idclient = 0,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Nom2",
                Numcartebancaire = "4444333322221111",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
            };
            Cartebancaire cb3 = new Cartebancaire()
            {
                Idcartebancaire = (int)cb2.Idcartebancaire,
                Idclient = cb2.Idclient,
                Dateenregistement = cb2.Dateenregistement,
                Titulairecartebancaire = cb2.Titulairecartebancaire,
                Numcartebancaire = cb2.Numcartebancaire,
                Dateexpirationcarte = cb2.Dateexpirationcarte
            };
            _repository.Setup(x => x.UpdateCartebancaireAsync(cb1, cb2)).ReturnsAsync(cb3);
            var result = await _controller.PutCartebancaire(cb1.Idcartebancaire, cb2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Cartebancaire?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Cartebancaire valeur = (Cartebancaire)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(cb3.Titulairecartebancaire, valeur.Titulairecartebancaire, "Cartes bancaires égales (titulaire)");
            Assert.AreEqual(cb1.Idcartebancaire, valeur.Idcartebancaire, "Cartes bancaires non-modifiées (id)");
            Assert.AreEqual(cb3.Dateexpirationcarte, valeur.Dateexpirationcarte, "Cartes bancaires égales (dateexp)");
        }


        [TestMethod()]
        public async Task PutCartebancaireTest_Innégal()
        {
            var result = await _controller.PutCartebancaire(-1, new CartebancaireDTO() { Idcartebancaire = 0 });
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Cartebancaire?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Résultat pas BadRequest");
        }

        [TestMethod()]
        public async Task PutCartebancaireTest_Introuvable()
        {
            CartebancaireDTO cb3 = new CartebancaireDTO()
            {
                Idcartebancaire = 0,
                Idclient = cb1.Idclient,
                Dateenregistement = cb1.Dateenregistement,
                Titulairecartebancaire = "Test",
                Numcartebancaire = "1111222233334444",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(10)
            };
            var result = await _controller.PutCartebancaire(0, cb3);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Cartebancaire?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }


        [TestMethod()]
        public async Task DeleteCartebancaireTest_Normal()
        {
            var result = await _controller.DeleteCartebancaire(cb1.Idcartebancaire);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteCartebancaireTest_Introuvable()
        {
            var result = await _controller.DeleteCartebancaire(0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }
        
    }
}