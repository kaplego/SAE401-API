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
    public class TransporteurControllerTestsMoq
    {
        private Mock<ITransporteurRepository<Transporteur>> _repository;
        private TransporteurController _controller;
        private Transporteur t1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<ITransporteurRepository<Transporteur>>();
            _controller = new TransporteurController(_repository.Object);

            t1 = new Transporteur()
            {
                Idtransporteur = 6,
                Nomtransporteur = "Test"
            };

            _repository.Setup(x => x.GetAllTransporteurAsync()).ReturnsAsync(new ActionResult<IEnumerable<Transporteur>>(new List<Transporteur>() { t1 }));
        }


        [TestMethod()]
        public async Task GetAllTransporteursTest_Normal()
        {
            var transporteurs = await _controller.GetAllTransporteur();
            Assert.IsNotNull(transporteurs, "Retour est null");
            Assert.IsInstanceOfType(transporteurs, typeof(ActionResult<IEnumerable<Transporteur>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(transporteurs.Value, typeof(IEnumerable<Transporteur>), "Pas des transporteurs ");
            Assert.AreEqual(t1, transporteurs.Value.Last(), "transporteurs égales");
        }

    }
}