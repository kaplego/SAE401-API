using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class ColorationControllerTestsMoq
    {
        private Mock<IColorationRepository<Coloration>> _repository;
        private ColorationController _controller;
        private Coloration c1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IColorationRepository<Coloration>>();
            _controller = new ColorationController(_repository.Object);

            c1 = new Coloration()
            {
                Idproduit = 1,
                Idcouleur = 1,
                Prixvente = 1,
                Prixsolde = 1,
                Quantitestock = 1,
                Descriptioncoloration = "Test",
                Estvisible = true
            };
            _repository.Setup(x => x.GetColorationByIdAsync(c1.Idproduit, c1.Idcouleur)).ReturnsAsync(c1);
        }


        [TestMethod()]
        public async Task GetColorationById_Normal()
        {
            var coloration = await _controller.GetColorationByIdAsync(c1.Idproduit,c1.Idcouleur);
            Assert.IsNotNull(coloration, "Retour est null");
            Assert.IsInstanceOfType(coloration, typeof(ActionResult<Coloration>), "Pas un ActionResult");
            Assert.IsInstanceOfType(coloration.Value, typeof(Coloration), "Pas une coloration");
            Assert.AreEqual(c1, coloration.Value, "colorations égales");
        }

        [TestMethod()]
        public async Task GetColorationById_Inexistant()
        {
            _repository.Setup(x => x.GetColorationByIdAsync(0, 0)).ReturnsAsync(value: (Coloration?)null);
            var coloration = await _controller.GetColorationByIdAsync(0, 0);
            Assert.IsNotNull(coloration, "Retour est null");
            Assert.IsInstanceOfType(coloration, typeof(ActionResult<Coloration>), "Pas un ActionResult");
            Assert.IsNotNull(coloration.Result, "Erreur est null");
            Assert.IsInstanceOfType(coloration.Result, typeof(NotFoundResult), "Pas un NotFound");
            Assert.IsNull(coloration.Value, "Valeur pas null");
        }

        [TestMethod()]
        public async Task PostColorationTest_Normal()
        {
            ColorationDTO c2 = new ColorationDTO()
            {
                Idproduit = 2,
                Idcouleur = 2,
                Prixvente = 1,
                Prixsolde = 1,
                Quantitestock = 1,
                Descriptioncoloration = "Test",
                Estvisible = true
            };
            Coloration c3 = new Coloration()
            {
                Idproduit = c2.Idproduit,
                Idcouleur = c2.Idcouleur,
                Prixvente = c2.Prixvente,
                Prixsolde = c2.Prixsolde,
                Quantitestock = c2.Quantitestock,
                Descriptioncoloration = c2.Descriptioncoloration,
                Estvisible = c2.Estvisible
            };
            _repository.Setup(x => x.AddColorationAsync(c3)).ReturnsAsync(c3);
            var result = await _controller.PostColoration(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Coloration?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Coloration valeur = (Coloration)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c2.Quantitestock, valeur.Quantitestock, "colorations égales");
        }


        [TestMethod()]
        public async Task PutColorationTest_Normal()
        {
            ColorationDTO c2 = new ColorationDTO()
            {
                Idproduit = 1,
                Idcouleur = 1,
                Prixvente = 1,
                Prixsolde = 10,
                Quantitestock = 1,
                Descriptioncoloration = "Test",
                Estvisible = true
            };
            Coloration c3 = new Coloration()
            {
                Idproduit = c2.Idproduit,
                Idcouleur = c2.Idcouleur,
                Prixvente = c2.Prixvente,
                Prixsolde = c2.Prixsolde,
                Quantitestock = c2.Quantitestock,
                Descriptioncoloration = c2.Descriptioncoloration,
                Estvisible = c2.Estvisible
            };
            _repository.Setup(x => x.UpdateColorationAsync(c1, c3)).ReturnsAsync(c3);
            var result = await _controller.PutColoration(c1.Idproduit,c1.Idcouleur, c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Coloration?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Coloration valeur = (Coloration)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c3.Quantitestock, valeur.Quantitestock, "Cartes bancaires égales (titulaire)");
            Assert.AreEqual(c1.Prixsolde, valeur.Prixsolde, "Cartes bancaires non-modifiées (id)");
            Assert.AreEqual(c3.Descriptioncoloration, valeur.Descriptioncoloration, "Cartes bancaires égales (dateexp)");
        }

        [TestMethod()]
        public async Task PutColorationTest_Innégal()
        {
            ColorationDTO c4 = new ColorationDTO()
            {
                Idproduit = 4,
                Idcouleur = 4,
                Prixvente = 1,
                Prixsolde = 10,
                Quantitestock = 1,
                Descriptioncoloration = "Test",
                Estvisible = true
            };
            var result = await _controller.PutColoration(c1.Idproduit,c1.Idcouleur, c4);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Coloration?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Résultat pas BadRequest");
        }

        [TestMethod()]
        public async Task PutAdresseTest_Introuvable()
        {
            ColorationDTO c5 = new ColorationDTO()
            {
                Idproduit = -1,
                Idcouleur = -1,
                Prixvente = 1,
                Prixsolde = 10,
                Quantitestock = 1,
                Descriptioncoloration = "Test",
                Estvisible = true
            };
            _repository.Setup(x => x.GetColorationByIdAsync(-1, -1)).ReturnsAsync(value: (Coloration?)null);
            var result = await _controller.PutColoration(-1,-1, c5);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Coloration?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }

    }
}