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

namespace SAE401_API.Controllers.Mock
{
    [TestClass()]
    public class AimeControllerTestsMoq
    {
        private Mock<IAimeRepository<Aime>> _repository;
        private AimeController _controller;
        private Client client1;
        private Aime a1;

        [TestInitialize]
        public async Task TestInitialize()
        {

            _repository = new Mock<IAimeRepository<Aime>>();
            _controller = new AimeController(_repository.Object);
            a1 = new Aime()
            {
                Idclient = 1,
                Idproduit = 1

            };
            _repository.Setup(x => x.GetAimeByIdAsync(1,1)).ReturnsAsync(new ActionResult<Aime?>(a1));
            _repository.Setup(x => x.GetAimeByIdAsync(0,0)).ReturnsAsync(new ActionResult<Aime?>(value: null));
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email",
            });
        }

        [TestMethod()]
        public async Task PostAimeTest_Normal()
        {
            AimeDTO a2 = new AimeDTO()
            {
                
                Idclient = 0,
                Idproduit = 1
                
            };

            Aime a22 = new Aime()
            {
                
                Idclient = a2.Idclient,
                Idproduit = a2.Idproduit
            };

            _repository.Setup(x => x.AddAimeAsync(a22)).ReturnsAsync(new Aime()
            {

                Idclient = a2.Idclient,
                Idproduit = a2.Idproduit
            });

            var result = await _controller.PostAime(a2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Aime?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Aime valeur = (Aime)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(a2.Idproduit, valeur.Idproduit, "Cartes bancaires égales");
            try { await _repository.Object.DeleteAimeAsync(valeur); } catch { }
        }

        [TestMethod()]
        public async Task DeleteAimeTest_Normal()
        {
            Aime a3 = new Aime()
            {
                Idclient = 0,
                Idproduit = 1
            };
            _repository.Setup(x => x.GetAimeByIdAsync(a3.Idclient,a3.Idproduit)).ReturnsAsync(new ActionResult<Aime?>(a3));
            _repository.Setup(x => x.DeleteAimeAsync(a3)).Returns(Task.CompletedTask);


            var result = await _controller.DeleteAime(a3.Idclient,a3.Idproduit);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteAimeTest_Introuvable()
        {
            var result = await _controller.DeleteAime(0,0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }


        

    }
}