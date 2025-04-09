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
    public class ProduitsimilaireControllerTestsMoq
    {
        private Mock<IProduitsimilaireRepository<Produitsimilaire>> _repository;
        private ProduitsimilaireController _controller;
        private Produitsimilaire p1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IProduitsimilaireRepository<Produitsimilaire>>();
            _controller = new ProduitsimilaireController(_repository.Object);


            p1 = new Produitsimilaire()
            {
                IdproduitRef = 1,
                IdproduitSim = 4
            };

            _repository.Setup(x => x.GetProduitsimilaireByIdAsync(p1.IdproduitRef, p1.IdproduitSim)).ReturnsAsync(p1);
            _repository.Setup(x => x.GetProduitsimilaireByIdAsync(0, 0)).ReturnsAsync((Produitsimilaire?)null);
        }

        [TestMethod()]
        public async Task PostProduitsimilaireTest_Normal()
        {

            ProduitsimilaireDTO p2 = new ProduitsimilaireDTO()
            {
                IdproduitRef = 2,
                IdproduitSim = 4
            };
            Produitsimilaire p3 = new Produitsimilaire()
            {
                IdproduitRef = 2,
                IdproduitSim = 4
            };
            _repository.Setup(x => x.AddProduitsimilaireAsync(p3)).ReturnsAsync(p3);
            var result = await _controller.PostProduitsimilaire(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produitsimilaire?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Produitsimilaire valeur = (Produitsimilaire)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.IdproduitSim, valeur.IdproduitSim, "produit similaire égales");
        }

        [TestMethod()]
        public async Task DeletProduitsimilaireTest_Normal()
        {

            var result = await _controller.DeleteProduitsimilaire(p1.IdproduitRef, p1.IdproduitSim);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteProduitTest_Introuvable()
        {
            var result = await _controller.DeleteProduitsimilaire(0, 0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }

    }
}