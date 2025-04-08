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
    public class AvisControllerTestsMoq
    {
        private Mock<IAvisRepository<Avisproduit>> _repository;
        private AvisController _controller;
        private Avisproduit a1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IAvisRepository<Avisproduit>>();
            _controller = new AvisController(_repository.Object);
            a1 = new Avisproduit()
            {
                Idavis = 1,
                Idproduit = 1,
                Idclient = 0,
                Noteavis = 4,
                Dateavis = DateTime.UtcNow,
                Commentaireavis = "Test"

            };
            _repository.Setup(x => x.GetAvisByIdAsync(1)).ReturnsAsync(new ActionResult<Avisproduit?>(a1));
            _repository.Setup(x => x.GetAvisByIdAsync(-1)).ReturnsAsync(new ActionResult<Avisproduit?>(value: null));
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email.email.email"
            });
        }

        [TestMethod()]
        public async Task PostAvisTest_Normal()
        {
            AvisproduitDTO a2 = new AvisproduitDTO()
            {
                Idavis = 0,
                Idproduit = 2,
                Idclient = 0,
                Noteavis = 4,
                Dateavis = DateTime.UtcNow,
                Commentaireavis = "Test"

            };
            Avisproduit a3 = new Avisproduit()
            {
                Idavis = 0,
                Idproduit = 2,
                Idclient = 0,
                Noteavis = 4,
                Dateavis = DateTime.UtcNow,
                Commentaireavis = "Test"

            };
            _repository.Setup(x => x.AddAvisAsync(a3)).ReturnsAsync(a3);
            var result = await _controller.PostAvis(a2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Avisproduit?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Avisproduit valeur = (Avisproduit)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(a2.Commentaireavis, valeur.Commentaireavis, "avis égales");
        }

        [TestMethod()]
        public async Task DeleteAvisTest_Normal()
        {
            var result = await _controller.DeleteAvis(a1.Idavis);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteAvisTest_Introuvable()
        {
            var result = await _controller.DeleteAvis(-1);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }
    }
}