using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace SAE401_APITests.Tests
{
    [TestClass()]
    public class ColorationControllerTests
    {

        private _DBMilibooContext _context;
        private IColorationRepository<Coloration> _repository;
        private ColorationController _controller;
        private Coloration c1;
        private Produit p1;

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
            _repository = new ColorationManager<Coloration>(_context);
            _controller = new ColorationController(_repository);
            
            c1 = new Coloration()
            {
                Idproduit = 1,
                Idcouleur = 1,
                Prixvente = 1,
                Prixsolde = 1,
                Quantitestock = 1,
                Descriptioncoloration = "Test",
                Estvisible = true
            };
            await _context.Colorations.AddAsync(c1);
            await _context.SaveChangesAsync();

            p1 = new Produit()
            {
                Idtypeproduit = 1,
                Idpays = 1,
                Nomproduit = "Test",


            };
            await _context.Produits.AddAsync(p1);
            await _context.SaveChangesAsync();

        }


        [TestMethod()]
        public async Task GetColorationById_Normal()
        {
            var coloration = await _controller.GetColorationByIdAsync(c1.Idproduit,c1.Idcouleur);
            Assert.IsNotNull(coloration, "Retour est null");
            Assert.IsInstanceOfType(coloration, typeof(ActionResult<Coloration>), "Pas un ActionResult");
            Assert.IsInstanceOfType(coloration.Value, typeof(Coloration), "Pas une coloration");
            Assert.AreEqual(c1, coloration.Value, "colorations égales");
        }

        [TestMethod()]
        public async Task GetColorationById_Inexistant()
        {
            var coloration = await _controller.GetColorationByIdAsync(0, 0);
            Assert.IsNotNull(coloration, "Retour est null");
            Assert.IsInstanceOfType(coloration, typeof(ActionResult<Coloration>), "Pas un ActionResult");
            Assert.IsNotNull(coloration.Result, "Erreur est null");
            Assert.IsInstanceOfType(coloration.Result, typeof(NotFoundResult), "Pas un NotFound");
            Assert.IsNull(coloration.Value, "Valeur pas null");
        }

        [TestMethod()]
        public async Task PostColorationTest_Normal()
        {
            ColorationDTO c2 = new ColorationDTO()
            {
                Idproduit = 2,
                Idcouleur = 2,
                Prixvente = 1,
                Prixsolde = 1,
                Quantitestock = 1,
                Descriptioncoloration = "Test",
                Estvisible = true
            };
            var result = await _controller.PostColoration(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Coloration?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Coloration valeur = (Coloration)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c2.Quantitestock, valeur.Quantitestock, "colorations égales");
            try { _context.Colorations.Remove(valeur); } catch { }
        }



        


        [TestMethod()]
        public async Task PutColorationTest_Normal()
        {
            ColorationDTO c3 = new ColorationDTO()
            {
                Idproduit = 1,
                Idcouleur = 1,
                Prixvente = 1,
                Prixsolde = 10,
                Quantitestock = 1,
                Descriptioncoloration = "Test",
                Estvisible = true
            };
            var result = await _controller.PutColoration(c1.Idproduit,c1.Idcouleur, c3);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Coloration?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Coloration valeur = (Coloration)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c3.Quantitestock, valeur.Quantitestock, "Cartes bancaires égales (titulaire)");
            Assert.AreEqual(c1.Prixsolde, valeur.Prixsolde, "Cartes bancaires non-modifiées (id)");
            Assert.AreEqual(c3.Descriptioncoloration, valeur.Descriptioncoloration, "Cartes bancaires égales (dateexp)");
        }

        [TestMethod()]
        public async Task PutColorationTest_Innégal()
        {
            ColorationDTO c4 = new ColorationDTO()
            {
                Idproduit = 4,
                Idcouleur = 4,
                Prixvente = 1,
                Prixsolde = 10,
                Quantitestock = 1,
                Descriptioncoloration = "Test",
                Estvisible = true
            };
            var result = await _controller.PutColoration(c1.Idproduit,c1.Idcouleur, c4);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Coloration?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Résultat pas BadRequest");
        }

        [TestMethod()]
        public async Task PutAdresseTest_Introuvable()
        {
            ColorationDTO c5 = new ColorationDTO()
            {
                Idproduit = -1,
                Idcouleur = -1,
                Prixvente = 1,
                Prixsolde = 10,
                Quantitestock = 1,
                Descriptioncoloration = "Test",
                Estvisible = true
            };
            var result = await _controller.PutColoration(-1,-1, c5);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Coloration?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostColorationTest_Invalide()
        {
            string longComment = new string('a', 2050);

            ColorationDTO c5 = new ColorationDTO()
            {
                Idproduit = p1.Idproduit,
                Idcouleur = 5,
                Prixvente = 1,
                Prixsolde = 1,
                Quantitestock = 1,
                Descriptioncoloration = longComment,
                Estvisible = true
            };
            try
            {
                var result = await _controller.PostColoration(c5);
            }
            catch (DbUpdateException ex)
            {
                try { _context.Colorations.RemoveRange(p1.ColorationsNavigation.Where(x => x.Descriptioncoloration == longComment)); }
                catch { throw ex; }
                throw ex;
            }
        }


        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Colorations.Remove(c1);
            _context.Produits.Remove(p1);

            await _context.SaveChangesAsync();
        }

    }
}