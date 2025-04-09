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
    public class PhotocolorationControllerTestsMoq
    {
        private Mock<IPhotocolorationRepository<Photocoloration>> _repository;
        private PhotocolorationController _controller;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IPhotocolorationRepository<Photocoloration>>();
            _controller = new PhotocolorationController(_repository.Object);

        }


        [TestMethod()]
        public async Task PostPhotoColorationTest_Normal()
        {
            PhotocolorationDTO c2 = new PhotocolorationDTO()
            {
                Idproduit = 2,
                Idcouleur = 5,
                Idphoto = 1,
            };
            Photocoloration c3 = new Photocoloration()
            {
                Idproduit = 2,
                Idcouleur = 5,
                Idphoto = 1,
            };
            _repository.Setup(x => x.AddPhotocolorationAsync(c3)).ReturnsAsync(c3);
            var result = await _controller.PostPhotocoloration(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Photocoloration?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Photocoloration valeur = (Photocoloration)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c2.Idphoto, valeur.Idphoto, "photo coloration égales");
        }



    }
}