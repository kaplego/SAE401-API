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
    public class HistoriqueconsultationControllerTestsMoq
    {
        private Mock<IHistoriqueconsultationRepository<Historiqueconsultation>> _repository;
        private HistoriqueconsultationController _controller;
        private Historiqueconsultation h1;



        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IHistoriqueconsultationRepository<Historiqueconsultation>>();
            _controller = new HistoriqueconsultationController(_repository.Object);
            h1 = new Historiqueconsultation()
            {
                Idproduit = 1,
                Idclient = 0,
                Dateconsultation = DateTime.UtcNow
            };
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email"
            });
            _repository.Setup(x => x.GetHistoriqueconsultationByIdAsync(1, 0)).ReturnsAsync(h1);
            _repository.Setup(x => x.GetHistoriqueconsultationByIdAsync(0, 0)).ReturnsAsync((Historiqueconsultation?)null);

        }


        [TestMethod()]
        public async Task PostHistoriqueTest_Normal()
        {
            HistoriqueconsultationDTO h2 = new HistoriqueconsultationDTO()
            {
                Idproduit = 2,
                Idclient = 0,
                Dateconsultation = DateTime.UtcNow
            };
            Historiqueconsultation h3 = new Historiqueconsultation()
            {
                Idproduit = h2.Idproduit,
                Idclient = h2.Idclient,
                Dateconsultation = h2.Dateconsultation
            };
            _repository.Setup(x => x.AddHistoriqueconsultationAsync(h3)).ReturnsAsync(h3);
            var result = await _controller.PostHistoriqueconsultation(h2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Historiqueconsultation?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Historiqueconsultation valeur = (Historiqueconsultation)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(h2.Dateconsultation, valeur.Dateconsultation, "historique de consultation égales");
        }

        [TestMethod()]
        public async Task DeleteHistoriqueTest_Normal()
        {

            Historiqueconsultation h2 = new Historiqueconsultation()
            {
                Idproduit = 3,
                Idclient = 0,
                Dateconsultation = DateTime.UtcNow
            };
            _repository.Setup(x => x.GetHistoriqueconsultationByIdAsync(h2.Idproduit, h2.Idclient)).ReturnsAsync(h2);
            var result = await _controller.DeleteHistoriqueconsultation(h2.Idproduit, h2.Idclient);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteHistoriqueTest_Introuvable()
        {
            var result = await _controller.DeleteHistoriqueconsultation(0, 0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }
    }
}