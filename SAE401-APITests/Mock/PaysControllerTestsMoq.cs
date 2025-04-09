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
    public class PaysControllerTestsMoq
    {
        private Mock<IPaysRepository<Pay>> _repository;
        private PaysController _controller;
        private Pay p1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            
            _repository = new Mock<IPaysRepository<Pay>>();
            _controller = new PaysController(_repository.Object);


            p1 = new Pay()
            {
                Idpays = 7,
                Nompays = "Test"
            };

            _repository.Setup(x => x.GetAllPaysAsync()).ReturnsAsync(new ActionResult<IEnumerable<Pay>>(new List<Pay>() { p1 }));

        }

        [TestMethod()]
        public async Task GetAllPaysTest_Normal()
        {
            var pays = await _controller.GetAllPays();
            Assert.IsNotNull(pays, "Retour est null");
            Assert.IsInstanceOfType(pays, typeof(ActionResult<IEnumerable<Pay>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(pays.Value, typeof(IEnumerable<Pay>), "Pas des pays ");
            Assert.AreEqual(p1, pays.Value.Last(), "pays égales");
        }
    }
}