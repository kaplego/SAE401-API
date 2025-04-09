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
    public class ValeurattributControllerTests
    {
        private _DBMilibooContext _context;
        private IValeurattributRepository<Valeurattribut> _repository;
        private ValeurattributController _controller;
        private Valeurattribut v1;


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
            _repository = new ValeurattributManager<Valeurattribut>(_context);
            _controller = new ValeurattributController(_repository);



            v1 = new Valeurattribut()
            {
                Idattribut = 1,
                Idproduit = 1,
                Valeur = "Test"
            };
            await _context.Valeurattributs.AddAsync(v1);
            await _context.SaveChangesAsync();
        }



        [TestMethod()]
        public async Task PostValeurTest_Normal()
        {
            ValeurattributDTO v2 = new ValeurattributDTO()
            {
                Idattribut = 2,
                Idproduit = 1,
                Valeur = "Test"
            };
            var result = await _controller.PostValeurattribut(v2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Valeurattribut?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Valeurattribut valeur = (Valeurattribut)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(v2.Valeur, valeur.Valeur, "valeurs égales");
            try { _context.Valeurattributs.Remove(valeur); } catch { }
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostValeurTest_Invalide()
        {
            string longComment = new string('a', 1030); // 1025 caractères 'a'


            ValeurattributDTO v3 = new ValeurattributDTO()
            {
                Idattribut = 3,
                Idproduit = 1,
                Valeur = longComment
            };
            try
            {
                var result = await _controller.PostValeurattribut(v3);
            }
            catch (DbUpdateException ex)
            {
                _context.Valeurattributs.Remove((Valeurattribut)ex.Entries.First().Entity);
                throw ex;
            }
        }

        [TestMethod()]
        public async Task PutValeurTest_Normal()
        {
            ValeurattributDTO v4 = new ValeurattributDTO()
            {
                Idattribut = 1,
                Idproduit = 1,
                Valeur = "Teste"
            };
            var result = await _controller.PutValeurattribut(v1.Idattribut, v1.Idproduit, v4);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Valeurattribut?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Valeurattribut valeur = (Valeurattribut)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(v4.Idproduit, valeur.Idproduit, "Valeurs égales (titulaire)");
            Assert.AreEqual(v1.Idattribut, valeur.Idattribut, "Valeurs non-modifiées (id)");
            Assert.AreEqual(v4.Valeur, valeur.Valeur, "valeurs égales (dateexp)");
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PutValeurTest_Invalide()
        {
            string longComment = new string('a', 1030); // 1025 caractères 'a'


            ValeurattributDTO v5 = new ValeurattributDTO()
            {
                Idattribut = 1,
                Idproduit = 1,
                Valeur = longComment
            };
            try
            {
                var result = await _controller.PutValeurattribut(v1.Idattribut, v1.Idproduit, v5);
            }
            catch (DbUpdateException ex)
            {
                Valeurattribut v = (Valeurattribut)ex.Entries.First().Entity;
                v.Valeur = "Test";
                throw ex;
            }
        }

        [TestMethod()]
        public async Task PutValeurTest_Innégal()
        {
            ValeurattributDTO v6 = new ValeurattributDTO()
            {
                Idattribut = 6,
                Idproduit = 1,
                Valeur = "Tes"
            };
            var result = await _controller.PutValeurattribut(-1, -1, v6);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Valeurattribut?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Résultat pas BadRequest");
        }


        [TestMethod()]
        public async Task PutValeurTest_Introuvable()
        {
            ValeurattributDTO v7 = new ValeurattributDTO()
            {
                Idattribut = -1,
                Idproduit = -1,
                Valeur = "Tes"
            };
            var result = await _controller.PutValeurattribut(-1, -1, v7);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Valeurattribut?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }

        [TestMethod()]
        public async Task DeleteValeurTest_Normal()
        {
            Valeurattribut v8 = new Valeurattribut()
            {
                Idattribut = 8,
                Idproduit = 1,
                Valeur = "Tes"
            };
            await _context.Valeurattributs.AddAsync(v8);
            await _context.SaveChangesAsync();
            var result = await _controller.DeleteValeurattribut(v8.Idattribut, v8.Idproduit);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteValeurTest_Introuvable()
        {
            var result = await _controller.DeleteValeurattribut(0, 0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }




        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Valeurattributs.Remove(v1);
            await _context.SaveChangesAsync();
        }


    }
}