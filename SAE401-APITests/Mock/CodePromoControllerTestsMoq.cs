using Microsoft.AspNetCore.Mvc;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class CodePromoControllerTestsMoq
    {
        private Mock<ICodePromoRepository<Codepromo>> _repository;
        private CodePromoController _controller;
        private Codepromo cp1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<ICodePromoRepository<Codepromo>>();
            _controller = new CodePromoController(_repository.Object);

            cp1 = new Codepromo()
            {
                Idcodepromo = 17,
                Idclient = 1,
                Nomcodepromo = "Test",
                Valeurreduction = 1,
                Estvalide = true
            };

            _repository.Setup(x => x.GetAllCodePromoAsync()).ReturnsAsync(new ActionResult<IEnumerable<Codepromo>>(new List<Codepromo>() { cp1 }));

        }

        [TestMethod()]
        public async Task GetAllCodePromosTest_Normal()
        {
            var codes = await _controller.GetAllCodePromo();
            Assert.IsNotNull(codes, "Retour est null");
            Assert.IsInstanceOfType(codes, typeof(ActionResult<IEnumerable<Codepromo>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(codes.Value, typeof(IEnumerable<Codepromo>), "Pas des codes promos ");
            Assert.AreEqual(cp1, codes.Value.Last(), "categories égales");
        }
    }
}