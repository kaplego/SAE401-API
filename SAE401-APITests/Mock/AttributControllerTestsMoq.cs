using Microsoft.AspNetCore.Mvc;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class AttributControllerTestsMoq
    {
        private Mock<IAttributRepository<Attributproduit>> _repository;
        private AttributController _controller;
        private Attributproduit at1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IAttributRepository<Attributproduit>>();
            _controller = new AttributController(_repository.Object);

            at1 = new Attributproduit()
            {
                Idattribut = 47,
                Idtypeproduit = 34,
                Nomattribut = "Test"
            };

            _repository.Setup(x => x.GetAllAttributByTypeAsync(0)).ReturnsAsync(new ActionResult<IEnumerable<Attributproduit>>(value: new List<Attributproduit>()));
            _repository.Setup(x => x.GetAllAttributByTypeAsync(1)).ReturnsAsync(new ActionResult<IEnumerable<Attributproduit>>(value: new List<Attributproduit>() { at1 }));

        }

        [TestMethod()]
        public async Task GetAllAttributsByTypeProduitTest_Normal()
        {
            var attributs = await _controller.GetAllAttributByType(1);
            Assert.IsNotNull(attributs, "Retour est null");
            Assert.IsInstanceOfType(attributs, typeof(ActionResult<IEnumerable<Attributproduit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(attributs.Value, typeof(IEnumerable<Attributproduit>), "Pas des attributs ");
            Assert.AreEqual(at1, attributs.Value.First(), "attributs égales");
        }

        [TestMethod()]
        public async Task GetAllAttributsByTypeProduitTest_Innexistant()
        {
            var attributs = await _controller.GetAllAttributByType(0);
            Assert.IsNotNull(attributs, "Retour est null");
            Assert.IsInstanceOfType(attributs, typeof(ActionResult<IEnumerable<Attributproduit>>), "Pas un ActionResult");

        }

    }
}