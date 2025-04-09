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
    public class SignalementaviControllerTests
    {
        private _DBMilibooContext _context;
        private ISignalementaviRepository<Signalementavi> _repository;
        private SignalementaviController _controller;
        private Signalementavi s1;


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
            _repository = new SignalementaviManager<Signalementavi>(_context);
            _controller = new SignalementaviController(_repository);



            s1 = new Signalementavi()
            {
                Idavis = 3,
                Idtypesignalement = 1,
                Emailsignalement = "Test",
                Datesignalement = DateTime.UtcNow,
                Contenusignalement = "Test"
            };
            await _context.Signalementavis.AddAsync(s1);
            await _context.SaveChangesAsync();
        }



        [TestMethod()]
        public async Task PostSignalementTest_Normal()
        {

            SignalementaviDTO s2 = new SignalementaviDTO()
            {
                Idavis = 4,
                Idtypesignalement = 1,
                Emailsignalement = "Test",
                Datesignalement = DateTime.UtcNow,
                Contenusignalement = "Test"
            };


            var result = await _controller.PostSignalementavi(s2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Signalementavi?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Signalementavi valeur = (Signalementavi)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(s2.Contenusignalement, valeur.Contenusignalement, "signalement égales");
            try { _context.Signalementavis.Remove(valeur); } catch { }
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostSignalementTest_Invalide()
        {
            // Générer un commentaire de plus de 1024 caractères
            string longComment = new string('a', 1030); // 1025 caractères 'a'

            SignalementaviDTO s3 = new SignalementaviDTO()
            {
                Idavis = 5,
                Idtypesignalement = 1,
                Emailsignalement = longComment,
                Datesignalement = DateTime.UtcNow,
                Contenusignalement = "Test"
            };

            try
            {
                var result = await _controller.PostSignalementavi(s3);

            }
            catch (DbUpdateException ex)
            {
                _context.Signalementavis.Remove((Signalementavi)ex.Entries.First().Entity);
                throw ex;
            }
        }


        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Signalementavis.Remove(s1);
            await _context.SaveChangesAsync();
        }
    }
}