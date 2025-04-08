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
    public class CategorieControllerTestsMoq
    {
        private Mock<ICategorieRepository<Categorieproduit>> _repository;
        private CategorieController _controller;
        private Categorieproduit ct1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<ICategorieRepository<Categorieproduit>>();
            _controller = new CategorieController(_repository.Object);

        

            ct1 = new Categorieproduit()
            {
               Idcategorie = 34,
               Nomcategorie = "Test",
               Descriptioncategorie = "Test",
               Estfiltrable = true
            };
            _repository.Setup(x => x.GetAllCategorieAsync()).ReturnsAsync(new ActionResult<IEnumerable<Categorieproduit>>(new List<Categorieproduit>() { ct1 }));

        }

        [TestMethod()]
        public async Task GetAllCategoriesTest_Normal()
        {
            var categories = await _controller.GetAllCategorie();
            Assert.IsNotNull(categories, "Retour est null");
            Assert.IsInstanceOfType(categories, typeof(ActionResult<IEnumerable<Categorieproduit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(categories.Value, typeof(IEnumerable<Categorieproduit>), "Pas des categories ");
            Assert.AreEqual(ct1, categories.Value.Last(), "categories égales");
        }
    }
}