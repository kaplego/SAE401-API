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
    public class PhotoControllerTestsMoq
    {
        private Mock<IPhotoRepository<Photo>> _repository;
        private PhotoController _controller;
        private Photo p1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IPhotoRepository<Photo>>();
            _controller = new PhotoController(_repository.Object);

            p1 = new Photo()
            {
                Idphoto = 1,
                Sourcephoto = "Test",
                Descriptionphoto = "Test"
            };

            _repository.Setup(x => x.GetPhotoByIdAsync(p1.Idphoto)).ReturnsAsync(p1);
            _repository.Setup(x => x.GetPhotoByIdAsync(0)).ReturnsAsync((Photo?)null);
        }


        [TestMethod()]
        public async Task PostPhotoTest_Normal()
        {
            PhotoDTO p2 = new PhotoDTO()
            {
                Sourcephoto = "Test",
                Descriptionphoto = "Test"
            };
            Photo p3 = new Photo()
            {
                Sourcephoto = "Test",
                Descriptionphoto = "Test"
            };
            _repository.Setup(x => x.AddPhotoAsync(p3)).ReturnsAsync(p3);
            var result = await _controller.PostPhoto(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Photo?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Photo valeur = (Photo)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.Descriptionphoto, valeur.Descriptionphoto, "photo égales");
        }


        [TestMethod()]
        public async Task DeletPhotoTest_Normal()
        {
            var result = await _controller.DeletePhoto(p1.Idphoto);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteAvisTest_Introuvable()
        {
            var result = await _controller.DeletePhoto(0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }

    }
}