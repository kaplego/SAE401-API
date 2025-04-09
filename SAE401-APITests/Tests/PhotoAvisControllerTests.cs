using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Tests
{
    [TestClass()]
    public class PhotoAvisControllerTests
    {
        private _DBMilibooContext _context;
        private IPhotoAvisRepository<Photoavi> _repository;
        private PhotoAvisController _controller;
        private Client client1;
        private Photoavi p1;
        private Avisproduit a1;

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
            _repository = new PhotoAvisManager<Photoavi>(_context);
            _controller = new PhotoAvisController(_repository);
            client1 = new Client()
            {
                Nomclient = "NOM",
                Prenomclient = "Prenom" + DateTime.UtcNow.ToString(),
                Emailclient = "email@email.email",
                Telportableclient = "33123456789",
                Datecreationcompte = DateTime.UtcNow,
                Hashmdp = "mdp",
                Pointfideliteclient = 0,
                Newslettermiliboo = true,
                Newsletterpartenaires = true
            };
            await _context.Clients.AddAsync(client1);
            await _context.SaveChangesAsync();


            a1 = new Avisproduit
            {
                Idproduit = 2,
                Idclient = client1.Idclient,
                Noteavis = 4,
                Dateavis = DateTime.UtcNow,
                Commentaireavis = "Test"


            };
            await _context.Avisproduits.AddAsync(a1);
            await _context.SaveChangesAsync();

            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);
        }


        [TestMethod()]
        public async Task PostPhotoAvisTest_Normal()
        {
            PhotoaviDTO p2 = new PhotoaviDTO()
            {
                Idavis = a1.Idavis,
                Idphoto = 1
            };

            var result = await _controller.PostPhotoAvis(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Photoavi?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Photoavi valeur = (Photoavi)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.Idphoto, valeur.Idphoto, "pphoto avis égales");
            try { _context.Photoavis.Remove(valeur); } catch { }
        }



        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Avisproduits.Remove(a1);

            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }


    }
}