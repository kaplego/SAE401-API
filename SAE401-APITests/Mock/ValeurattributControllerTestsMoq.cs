using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class ValeurattributControllerTestsMoq
    {
        private Mock<IValeurattributRepository<Valeurattribut>> _repository;
        private ValeurattributController _controller;
        private Valeurattribut v1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IValeurattributRepository<Valeurattribut>>();
            _controller = new ValeurattributController(_repository.Object);

            v1 = new Valeurattribut()
            {
                Idattribut = 1,
                Idproduit = 1,
                Valeur = "Test"
            };

            _repository.Setup(x => x.GetValeurattributByIdAsync(v1.Idattribut, v1.Idproduit)).ReturnsAsync(v1);
            _repository.Setup(x => x.GetValeurattributByIdAsync(-1, -1)).ReturnsAsync(new ActionResult<Valeurattribut?>((Valeurattribut?)null));
        }



        [TestMethod()]
        public async Task PostValeurTest_Normal()
        {
            ValeurattributDTO v2 = new ValeurattributDTO()
            {
                Idattribut = 2,
                Idproduit = 1,
                Valeur = "Test"
            };
            Valeurattribut v3 = new Valeurattribut()
            {
                Idattribut = 2,
                Idproduit = 1,
                Valeur = "Test"
            };
            _repository.Setup(x => x.AddValeurattributAsync(v3)).ReturnsAsync(v3);
            var result = await _controller.PostValeurattribut(v2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Valeurattribut?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Valeurattribut valeur = (Valeurattribut)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(v2.Valeur, valeur.Valeur, "valeurs égales");
        }


        [TestMethod()]
        public async Task PutValeurTest_Normal()
        {
            ValeurattributDTO v4 = new ValeurattributDTO()
            {
                Idattribut = 1,
                Idproduit = 1,
                Valeur = "Teste"
            };
            Valeurattribut v5 = new Valeurattribut()
            {
                Idattribut = 1,
                Idproduit = 1,
                Valeur = "Teste"
            };
            _repository.Setup(x => x.UpdateValeurattributAsync(v1, v4)).ReturnsAsync(v5);
            var result = await _controller.PutValeurattribut(v1.Idattribut, v1.Idproduit, v4);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Valeurattribut?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Valeurattribut valeur = (Valeurattribut)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(v4.Idproduit, valeur.Idproduit, "Valeurs égales (titulaire)");
            Assert.AreEqual(v1.Idattribut, valeur.Idattribut, "Valeurs non-modifiées (id)");
            Assert.AreEqual(v4.Valeur, valeur.Valeur, "valeurs égales (dateexp)");
        }

        [TestMethod()]
        public async Task PutValeurTest_Innégal()
        {
            ValeurattributDTO v6 = new ValeurattributDTO()
            {
                Idattribut = 6,
                Idproduit = 1,
                Valeur = "Tes"
            };
            var result = await _controller.PutValeurattribut(-1, -1, v6);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Valeurattribut?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Résultat pas BadRequest");
        }


        [TestMethod()]
        public async Task PutValeurTest_Introuvable()
        {
            ValeurattributDTO v7 = new ValeurattributDTO()
            {
                Idattribut = -1,
                Idproduit = -1,
                Valeur = "Tes"
            };
            var result = await _controller.PutValeurattribut(-1, -1, v7);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Valeurattribut?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }

        [TestMethod()]
        public async Task DeleteValeurTest_Normal()
        {
            Valeurattribut v8 = new Valeurattribut()
            {
                Idattribut = 8,
                Idproduit = 1,
                Valeur = "Tes"
            };
            _repository.Setup(x => x.GetValeurattributByIdAsync(v8.Idattribut, v8.Idproduit)).ReturnsAsync(v8);
            var result = await _controller.DeleteValeurattribut(v8.Idattribut, v8.Idproduit);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteValeurTest_Introuvable()
        {
            var result = await _controller.DeleteValeurattribut(-1, -1);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }

    }
}