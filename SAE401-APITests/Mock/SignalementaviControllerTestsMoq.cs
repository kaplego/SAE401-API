using Microsoft.AspNetCore.Mvc;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class SignalementaviControllerTestsMoq
    {
        private Mock<ISignalementaviRepository<Signalementavi>> _repository;
        private SignalementaviController _controller;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<ISignalementaviRepository<Signalementavi>>();
            _controller = new SignalementaviController(_repository.Object);

        }



        [TestMethod()]
        public async Task PostSignalementTest_Normal()
        {

            SignalementaviDTO s1 = new SignalementaviDTO()
            {
                Idavis = 4,
                Idtypesignalement = 1,
                Emailsignalement = "Test",
                Datesignalement = DateTime.UtcNow,
                Contenusignalement = "Test"
            };
            Signalementavi s2 = new Signalementavi()
            {
                Idavis = 4,
                Idtypesignalement = 1,
                Emailsignalement = "Test",
                Datesignalement = DateTime.UtcNow,
                Contenusignalement = "Test"
            };
            _repository.Setup(x => x.AddSignalementaviAsync(s2)).ReturnsAsync(s2);
            var result = await _controller.PostSignalementavi(s1);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Signalementavi?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Signalementavi valeur = (Signalementavi)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(s1.Contenusignalement, valeur.Contenusignalement, "signalement égales");
        }
    }
}