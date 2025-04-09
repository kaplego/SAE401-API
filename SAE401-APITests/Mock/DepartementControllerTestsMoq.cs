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
    public class DepartementControllerTestsMoq
    {
        private Mock<IDepartementRepository<Departement>> _repository;
        private DepartementController _controller;
        private Departement d1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IDepartementRepository<Departement>>();
            _controller = new DepartementController(_repository.Object);

            d1 = new Departement()
            {
                Iddepartement = 977,
                Nomdepartement = "Test"

            };

            _repository.Setup(x => x.GetAllDepartementAsync()).ReturnsAsync(new ActionResult<IEnumerable<Departement>>(new List<Departement>() { d1 }));

        }

        [TestMethod()]
        public async Task GetAllDdepartementsTest_Normal()
        {
            var departements = await _controller.GetAllDepartement();
            Assert.IsNotNull(departements, "Retour est null");
            Assert.IsInstanceOfType(departements, typeof(ActionResult<IEnumerable<Departement>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(departements.Value, typeof(IEnumerable<Departement>), "Pas des departements ");
            Assert.AreEqual(d1, departements.Value.Last(), "departements égales");
        }
    }
}