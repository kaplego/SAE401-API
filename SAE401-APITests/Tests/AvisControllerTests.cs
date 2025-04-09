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
    public class AvisControllerTests
    {
        private _DBMilibooContext _context;
        private IAvisRepository<Avisproduit> _repository;
        private AvisController _controller;
        private Client client1;
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
            _repository = new AvisManager(_context);
            _controller = new AvisController(_repository);
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
            a1 = new Avisproduit()
            {
                Idproduit = 1,
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
        public async Task PostAvisTest_Normal()
        {
            AvisproduitDTO a2 = new AvisproduitDTO()
            {
                Idproduit = 2,
                Idclient = client1.Idclient,
                Noteavis = 4,
                Dateavis = DateTime.UtcNow,
                Commentaireavis = "Test"

            };
            var result = await _controller.PostAvis(a2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Avisproduit?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Avisproduit valeur = (Avisproduit)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.IsInstanceOfType(valeur, typeof(Avisproduit), "Pas un avis");
            Assert.AreEqual(a2.Commentaireavis, valeur.Commentaireavis, "avis égaux");
            try { _context.Avisproduits.Remove(valeur); } catch { }
        }


        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostAvisTest_Invalide()
        {
            // Générer un commentaire de plus de 1024 caractères
            string longComment = new string('a', 1030); // 1025 caractères 'a'

            AvisproduitDTO a5 = new AvisproduitDTO()
            {
                Idproduit = 6,
                Idclient = client1.Idclient,
                Noteavis = 4,
                Dateavis = DateTime.UtcNow,
                Commentaireavis = longComment
            };

            try
            {
                var result = await _controller.PostAvis(a5);

            }
            catch (DbUpdateException ex)
            {
                _context.Avisproduits.Remove((Avisproduit)ex.Entries.First().Entity);
                throw ex;
            }
        }




        [TestMethod()]
        public async Task DeleteAvisTest_Normal()
        {
            Avisproduit a3 = new Avisproduit()
            {
                Idproduit = 3,
                Idclient = client1.Idclient,
                Noteavis = 4,
                Dateavis = DateTime.UtcNow,
                Commentaireavis = "Test"
            };
            await _context.Avisproduits.AddAsync(a3);
            await _context.SaveChangesAsync();
            var result = await _controller.DeleteAvis(a3.Idavis);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteAvisTest_Introuvable()
        {
            var result = await _controller.DeleteAvis(0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }


        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Avisproduits.RemoveRange(client1.AvisNavigation);
            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }
    }
}