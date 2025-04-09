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
    public class DetailPanierControllerTestsMoq
    {
        private Mock<IDetailPanierRepository<Detailpanier>> _repository;
        private DetailPanierController _controller;
        private Detailpanier d1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IDetailPanierRepository<Detailpanier>>();
            _controller = new DetailPanierController(_repository.Object);
            d1 = new Detailpanier()
            {
                Idproduit = 1,
                Idcouleur = 7,
                Idclient = 0,
                Quantitepanier = 1

            };
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email"
            });
            _repository.Setup(x => x.GetDetailPanierByIdAsync(d1.Idproduit, d1.Idcouleur, d1.Idclient)).ReturnsAsync(d1);
            _repository.Setup(x => x.GetDetailPanierByIdAsync(0, 0, 0)).ReturnsAsync(value: (Detailpanier?)null);

        }



        [TestMethod()]
        public async Task PostDetailpanierTest_Normal()
        {
            DetailpanierDTO d2 = new DetailpanierDTO()
            {
                Idproduit = 1,
                Idcouleur = 8,
                Idclient = 0,
                Quantitepanier = 1
            };
            Detailpanier d3 = new Detailpanier()
            {
                Idproduit = 1,
                Idcouleur = 8,
                Idclient = 0,
                Quantitepanier = 1
            };
            _repository.Setup(x => x.AddDetailPanierAsync(d3)).ReturnsAsync(d3);
            var result = await _controller.PostDetailPanier(d2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpanier?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Detailpanier valeur = (Detailpanier)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(d2.Quantitepanier, valeur.Quantitepanier, "détail panier composition égales");
        }

        [TestMethod()]
        public async Task PutDetailPanierTest_Normal()
        {
            DetailpanierDTO d2 = new DetailpanierDTO()
            {
                Idproduit = d1.Idproduit,
                Idcouleur = d1.Idcouleur,
                Idclient = d1.Idclient,
                Quantitepanier = 2
            };
            Detailpanier d3 = new Detailpanier()
            {
                Idproduit = d2.Idproduit,
                Idcouleur = d2.Idcouleur,
                Idclient = d2.Idclient,
                Quantitepanier = d2.Quantitepanier
            };
            _repository.Setup(x => x.UpdateDetailPanierAsync(d1, d2)).ReturnsAsync(d3);
            var result = await _controller.PutDetailPanier(d1.Idproduit, d1.Idcouleur, d1.Idclient, d2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpanier?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Detailpanier valeur = (Detailpanier)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(d3.Quantitepanier, valeur.Quantitepanier, "détail panier  égales (quantité)");
            Assert.AreEqual(d1.Idcouleur, valeur.Idcouleur, "détail panier non-modifiées (id)");

        }


        [TestMethod()]
        public async Task PutDetailPanierTest_Innégal()
        {
            DetailpanierDTO d4 = new DetailpanierDTO()
            {
                Idproduit = d1.Idproduit,
                Idcouleur = d1.Idcouleur,
                Idclient = 0,
                Quantitepanier = 2

            };
            var result = await _controller.PutDetailPanier(-1, -1, -1, d4);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpanier?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Résultat pas BadRequest");
        }


        [TestMethod()]
        public async Task PutDetailpanierTest_Introuvable()
        {
            DetailpanierDTO d5 = new DetailpanierDTO()
            {
                Idproduit = 0,
                Idcouleur = 0,
                Idclient = 0,
                Quantitepanier = 2

            };
            var result = await _controller.PutDetailPanier(0, 0, 0, d5);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailpanier?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }

        [TestMethod()]
        public async Task DeleteDetailpanierTest_Normal()
        {

            Detailpanier d6 = new Detailpanier()
            {
                Idproduit = 1,
                Idcouleur = 5,
                Idclient = 0,
                Quantitepanier = 1

            };
            _repository.Setup(x => x.GetDetailPanierByIdAsync(d6.Idproduit, d6.Idcouleur, d6.Idclient)).ReturnsAsync(d6);
            var result = await _controller.DeleteDetailPanier(d6.Idproduit, d6.Idcouleur, d6.Idclient);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteDetailPanierTest_Introuvable()
        {
            var result = await _controller.DeleteDetailPanier(0, 0, 0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }
    }
}