using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
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
    public class RegroupementControllerTestsMoq
    {
        private Mock<IRegroupementRepository<Regroupementproduit>> _repository;
        private RegroupementController _controller;
        private Regroupementproduit r1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IRegroupementRepository<Regroupementproduit>>();
            _controller = new RegroupementController(_repository.Object);

            r1 = new Regroupementproduit()
            {
                Idregroupement = 5,
                Nomregroupement = "Test"
            };

            _repository.Setup(x => x.GetAllRegroupementAsync()).ReturnsAsync(
                new ActionResult<IEnumerable<Regroupementproduit>>(new List<Regroupementproduit>() { r1 }));
        }

        [TestMethod()]
        public async Task GetAllRegroupementsTest_Normal()
        {
            var regroupements = await _controller.GetAllRegroupement();
            Assert.IsNotNull(regroupements, "Retour est null");
            Assert.IsInstanceOfType(regroupements, typeof(ActionResult<IEnumerable<Regroupementproduit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(regroupements.Value, typeof(IEnumerable<Regroupementproduit>), "Pas des regroupements ");
            Assert.AreEqual(r1, regroupements.Value.Last(), "regrouepements égales");
        }

    }
}