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
using System.Text;
using System.Threading.Tasks;

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class DetailpaniercompositionControllerTestsMoq
    {
        private Mock<IDetailPanierCompositionRepository<Detailpaniercomposition>> _repository;
        private DetailpaniercompositionController _controller;
        private Detailpaniercomposition d1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IDetailPanierCompositionRepository<Detailpaniercomposition>>();
            _controller = new DetailpaniercompositionController(_repository.Object);
            d1 = new Detailpaniercomposition()
            {
                Idcomposition = 1,
                Idclient = 0,
                Quantitepaniercomposition = 1

            };
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email"
            });
            _repository.Setup(x => x.GetDetailPanierCompositionByIdAsync(d1.Idcomposition, 0)).ReturnsAsync(d1);
            _repository.Setup(x => x.GetDetailPanierCompositionByIdAsync(0, 0)).ReturnsAsync(value: (Detailpaniercomposition?)null);
        }


        [TestMethod()]
        public async Task PostDetailpaniercompositionTest_Normal()
        {
            DetailpaniercompositionDTO d2 = new DetailpaniercompositionDTO()
            {
                Idcomposition = 2,
                Idclient = 0,
                Quantitepaniercomposition = 1
            };
            Detailpaniercomposition d3 = new Detailpaniercomposition()
            {
                Idcomposition = 2,
                Idclient = 0,
                Quantitepaniercomposition = 1
            };
            _repository.Setup(x => x.AddDetailPanierCompositionAsync(d3)).ReturnsAsync(d3);
            var result = await _controller.PostDetailPanierComposition(d2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpaniercomposition?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Detailpaniercomposition valeur = (Detailpaniercomposition)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(d2.Quantitepaniercomposition, valeur.Quantitepaniercomposition, "détail panier composition égales");
        }



        [TestMethod()]
        public async Task PutDetailPanierCompositionTest_Normal()
        {
            DetailpaniercompositionDTO d2 = new DetailpaniercompositionDTO()
            {
                Idcomposition = 1,
                Idclient = 0,
                Quantitepaniercomposition = 1
            };
            Detailpaniercomposition d3 = new Detailpaniercomposition()
            {
                Idcomposition = 1,
                Idclient = 0,
                Quantitepaniercomposition = 1
            };
            _repository.Setup(x => x.UpdateDetailPanierCompositionAsync(d1, d3)).ReturnsAsync(d3);
            var result = await _controller.PutDetailPanierComposition(d1.Idcomposition, d1.Idclient, d2);
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
                Idcomposition = 0,
                Idclient = 0,
                Quantitepaniercomposition = 1
            };
            var result = await _controller.PutDetailPanierComposition(1, 0, d4);
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
                Idcomposition = 0,
                Idclient = 0,
                Quantitepaniercomposition = 1
            };
            var result = await _controller.PutDetailPanierComposition(0, 0, d5);
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
                Idclient = 0,
                Quantitepaniercomposition = 1
            };
            _repository.Setup(x => x.GetDetailPanierCompositionByIdAsync(d6.Idcomposition, d6.Idclient)).ReturnsAsync(d6);
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

    }
}