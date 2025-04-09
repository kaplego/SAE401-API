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
    public class PhotoControllerTests
    {
        private _DBMilibooContext _context;
        private IPhotoRepository<Photo> _repository;
        private PhotoController _controller;
        private Photo p1;


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
            _repository = new PhotoManager(_context);
            _controller = new PhotoController(_repository);



            p1 = new Photo()
            {
                Sourcephoto = "Test",
                Descriptionphoto = "Test"
            };
            await _context.Photos.AddAsync(p1);
            await _context.SaveChangesAsync();
        }


        [TestMethod()]
        public async Task PostPhotoTest_Normal()
        {
            PhotoDTO p2 = new PhotoDTO()
            {
                Sourcephoto = "Test",
                Descriptionphoto = "Test"
            };

            var result = await _controller.PostPhoto(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Photo?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Photo valeur = (Photo)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.Descriptionphoto, valeur.Descriptionphoto, "photo égales");
            try { _context.Photos.Remove(valeur); } catch { }
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostPhotoTest_Invalide()
        {
            // Générer un commentaire de plus de 1024 caractères
            string longComment = new string('a', 1030); // 1025 caractères 'a'

            PhotoDTO p3 = new PhotoDTO()
            {
                Sourcephoto = "Test",
                Descriptionphoto = longComment
            };

            try
            {
                var result = await _controller.PostPhoto(p3);

            }
            catch (DbUpdateException ex)
            {
                _context.Photos.Remove((Photo)ex.Entries.First().Entity);
                throw ex;
            }
        }

        [TestMethod()]
        public async Task DeletPhotoTest_Normal()
        {

            Photo p4 = new Photo()
            {
                Sourcephoto = "Test",
                Descriptionphoto = "Test"
            };
            await _context.Photos.AddAsync(p4);
            await _context.SaveChangesAsync();
            var result = await _controller.DeletePhoto(p4.Idphoto);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeletePhotoTest_Introuvable()
        {
            var result = await _controller.DeletePhoto(0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }




        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Photos.Remove(p1);
            await _context.SaveChangesAsync();
        }

    }
}