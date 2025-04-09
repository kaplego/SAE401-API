using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Tests
{
    [TestClass()]
    public class PhotocolorationControllerTests
    {
        private _DBMilibooContext _context;
        private IPhotocolorationRepository<Photocoloration> _repository;
        private PhotocolorationController _controller;
        private Photocoloration c1;


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
            _repository = new PhotocolorationManager<Photocoloration>(_context);
            _controller = new PhotocolorationController(_repository);



            c1 = new Photocoloration()
            {
                Idproduit = 2,
                Idcouleur = 4,
                Idphoto = 1,
            };
            await _context.Photocolorations.AddAsync(c1);
            await _context.SaveChangesAsync();
        }


        [TestMethod()]
        public async Task PostPhotoColorationTest_Normal()
        {
            PhotocolorationDTO c2 = new PhotocolorationDTO()
            {
                Idproduit = 2,
                Idcouleur = 5,
                Idphoto = 1,
            };

            var result = await _controller.PostPhotocoloration(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Photocoloration?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Photocoloration valeur = (Photocoloration)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c2.Idphoto, valeur.Idphoto, "photo coloration égales");
            try { _context.Photocolorations.Remove(valeur); } catch { }
        }





        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Photocolorations.Remove(c1);
            await _context.SaveChangesAsync();
        }


    }
}