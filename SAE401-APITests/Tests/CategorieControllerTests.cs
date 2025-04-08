using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE401_APITests.Tests
{
    [TestClass()]
    public class CategorieControllerTests
    {
        private _DBMilibooContext _context;
        private ICategorieRepository<Categorieproduit> _repository;
        private CategorieController _controller;
        private Categorieproduit ct1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            Env.Load(Path.Combine(
                Directory.GetParent(Directory.GetParent(
                Directory.GetParent(Directory.GetCurrentDirectory()
                .ToString()).ToString()).ToString()).ToString(), ".env"));
            var builder = new DbContextOptionsBuilder<_DBMilibooContext>().UseNpgsql(
                Environment.GetEnvironmentVariable("CONNECTION_STRING"))
                .EnableSensitiveDataLogging(true);
            _context = new _DBMilibooContext(builder.Options);
            _repository = new CategorieManager(_context);
            _controller = new CategorieController(_repository);

            

            ct1 = new Categorieproduit()
            {
               Idcategorie = 34,
               Nomcategorie = "Test",
               Descriptioncategorie = "Test",
               Estfiltrable = true
            };
            await _context.Categorieproduits.AddAsync(ct1);
            await _context.SaveChangesAsync();



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



        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Categorieproduits.Remove(ct1);
            await _context.SaveChangesAsync();
        }
    }
}