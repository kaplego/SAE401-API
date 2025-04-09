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
    public class VilleControllerTestsMoq
    {
        private Mock<IVilleRepository<Ville>> _repository;
        private VilleController _controller;
        private Ville v1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IVilleRepository<Ville>>();
            _controller = new VilleController(_repository.Object);

            v1 = new Ville()
            {
                Codeinsee = "99183",
                Nomville = "Test"

            };

            _repository.Setup(x => x.GetAllVilleAsync()).ReturnsAsync(new ActionResult<IEnumerable<Ville>>(new List<Ville>() { v1 }));

        }



        [TestMethod()]
        public async Task GetAllVillesTest_Normal()
        {
            var villes = await _controller.GetAllVille();
            Assert.IsNotNull(villes, "Retour est null");
            Assert.IsInstanceOfType(villes, typeof(ActionResult<IEnumerable<Ville>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(villes.Value, typeof(IEnumerable<Ville>), "Pas des villes ");
            Assert.AreEqual(v1, villes.Value.Last(), "villes égales");
        }
    }

}