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
    public class TypePaiementControllerTestsMoq
    {
        private Mock<ITypePaiementRepository<Typepaiement>> _repository;
        private TypePaiementController _controller;
        private Typepaiement t1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<ITypePaiementRepository<Typepaiement>>();
            _controller = new TypePaiementController(_repository.Object);

            t1 = new Typepaiement()
            {
                Idtypepaiement = 4,
                Nomtypepaiement = "Test"

            };

            _repository.Setup(x => x.GetAllTypePaiementAsync()).ReturnsAsync(new ActionResult<IEnumerable<Typepaiement>>(new List<Typepaiement>() { t1 }));
        }


        [TestMethod()]
        public async Task GetAllTypePaiementsTest_Normal()
        {
            var typepaiements = await _controller.GetAllTypePaiement();
            Assert.IsNotNull(typepaiements, "Retour est null");
            Assert.IsInstanceOfType(typepaiements, typeof(ActionResult<IEnumerable<Typepaiement>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(typepaiements.Value, typeof(IEnumerable<Typepaiement>), "Pas des type paiements ");
            Assert.AreEqual(t1, typepaiements.Value.Last(), "type paiements égales");
        }

    }
}