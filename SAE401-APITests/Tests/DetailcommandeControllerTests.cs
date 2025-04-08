using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
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
    public class DetailcommandeControllerTests
    {
        private _DBMilibooContext _context;
        private IDetailcommandeRepository<Detailcommande> _repository;
        private DetailcommandeController _controller;
        private Detailcommande detail1;


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
            _repository = new DetailcommandeManager<Detailcommande>(_context);
            _controller = new DetailcommandeController(_repository);
            detail1 = new Detailcommande()
            {
                Idproduit = 1,
                Idcouleur = 7,
                Idcommande = 1,
                Quantitecommande = 1
            };
            await _context.Detailcommandes.AddAsync(detail1);
            await _context.SaveChangesAsync();

        }



        [TestMethod()]
        public async Task PostDetailcommandeTest_Normal()
        {
            DetailcommandeDTO c2 = new DetailcommandeDTO()
            {
                Idproduit = 1,
                Idcouleur = 7,
                Idcommande = 2,
                Quantitecommande = 1
            };
            var result = await _controller.PostDetailcommande(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailcommande?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Detailcommande valeur = (Detailcommande)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c2.Quantitecommande, valeur.Quantitecommande, "detail commandes égales");
            try { _context.Detailcommandes.Remove(valeur); } catch { }
        }






        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Detailcommandes.Remove(detail1);
            await _context.SaveChangesAsync();
        }
    }
}