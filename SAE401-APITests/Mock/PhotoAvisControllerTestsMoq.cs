using Microsoft.AspNetCore.Mvc;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class PhotoAvisControllerTestsMoq
    {
        private Mock<IPhotoAvisRepository<Photoavi>> _repository;
        private PhotoAvisController _controller;
        private Photoavi p1;
        private Avisproduit a1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IPhotoAvisRepository<Photoavi>>();
            _controller = new PhotoAvisController(_repository.Object);

            a1 = new Avisproduit
            {
                Idavis = 0,
                Idproduit = 2,
                Idclient = 0,
                Noteavis = 4,
                Dateavis = DateTime.UtcNow,
                Commentaireavis = "Test"
            };

            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email"
            });

            _repository.Setup(x => x.GetAvisByIdAsync(0)).ReturnsAsync(a1);
        }


        [TestMethod()]
        public async Task PostPhotoAvisTest_Normal()
        {
            PhotoaviDTO p2 = new PhotoaviDTO()
            {
                Idavis = a1.Idavis,
                Idphoto = 1
            };
            Photoavi p3 = new Photoavi()
            {
                Idavis = a1.Idavis,
                Idphoto = 1
            };
            _repository.Setup(x => x.AddPhotoAvisAsync(p3)).ReturnsAsync(p3);
            var result = await _controller.PostPhotoAvis(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Photoavi?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Photoavi valeur = (Photoavi)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.Idphoto, valeur.Idphoto, "pphoto avis égales");
        }

    }
}