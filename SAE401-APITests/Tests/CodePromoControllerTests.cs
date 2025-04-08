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
    public class CodePromoControllerTests
    {
        private _DBMilibooContext _context;
        private ICodePromoRepository<Codepromo> _repository;
        private CodePromoController _controller;
        private Codepromo cp1;


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
            _repository = new CodePromoManager(_context);
            _controller = new CodePromoController(_repository);



            cp1 = new Codepromo()
            {
                Idcodepromo = 17,
                Idclient = 1,
                Nomcodepromo = "Test",
                Valeurreduction = 1,
                Estvalide = true
            };
            await _context.Codepromos.AddAsync(cp1);
            await _context.SaveChangesAsync();



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



        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Codepromos.Remove(cp1);
            await _context.SaveChangesAsync();
        }
    }
}