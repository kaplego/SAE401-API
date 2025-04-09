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
    public class ProfessionelControllerTests
    {
        private _DBMilibooContext _context;
        private IProfessionelRepository<Professionel> _repository;
        private ProfessionelController _controller;
        private Client client1;
        private Client client2;
        private Professionel p1;

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
            _repository = new ProfessionelManager(_context);
            _controller = new ProfessionelController(_repository);
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




            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);



        }

        [TestMethod()]
        public async Task PostProfessionnelTest_Normal()
        {
            ProfessionelDTO p2 = new ProfessionelDTO()
            {
                Idclient = client1.Idclient,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678911"
            };


            var result = await _controller.PostProfessionel(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Professionel?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Professionel valeur = (Professionel)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.Nomsociete, valeur.Nomsociete, "professionnels égales");
            try { _context.Professionels.Remove(valeur); } catch { }
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostProfessionnelTest_Invalide()
        {
            ProfessionelDTO p3 = new ProfessionelDTO()
            {
                Idclient = client1.Idclient,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "123456789123"
            };
            try
            {
                var result = await _controller.PostProfessionel(p3);
            }
            catch (DbUpdateException ex)
            {
                _context.Professionels.Remove((Professionel)ex.Entries.First().Entity);
                throw ex;
            }
        }


        [TestMethod()]
        public async Task PutProfessionnelTest_Normal()
        {

            Professionel p1 = new Professionel()
            {
                Idclient = client1.Idclient,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678910"
            };

            await _context.Professionels.AddAsync(p1);
            await _context.SaveChangesAsync();



            ProfessionelDTO p3 = new ProfessionelDTO()
            {
                Idclient = client1.Idclient,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678914"
            };
            var result = await _controller.PutProfessionel(p1.Idclient, p3);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Professionel?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Professionel valeur = (Professionel)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p3.Nomsociete, valeur.Nomsociete, "professionels égales (titulaire)");
            Assert.AreEqual(p1.Idclient, valeur.Idclient, "professionnels non-modifiées (id)");
            Assert.AreEqual(p3.Numtva, valeur.Numtva, "professionnels égales (dateexp)");
            _context.Professionels.Remove(p1);
            await _context.SaveChangesAsync();
        }


        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PutProfessionnelsTest_Invalide()
        {
            Professionel p1 = new Professionel()
            {
                Idclient = client1.Idclient,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678910"
            };

            await _context.Professionels.AddAsync(p1);
            await _context.SaveChangesAsync();



            ProfessionelDTO p4 = new ProfessionelDTO()
            {
                Idclient = client1.Idclient,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "123456789145"
            };
            try
            {
                var result = await _controller.PutProfessionel(p1.Idclient, p4);
            }
            catch (DbUpdateException ex)
            {
                _context.Professionels.Remove((Professionel)ex.Entries.First().Entity);
                throw ex;
            }
            _context.Professionels.Remove(p1);
            await _context.SaveChangesAsync();
        }


        [TestMethod()]
        public async Task PutProfessionnelTest_Innégal()
        {


            ProfessionelDTO p5 = new ProfessionelDTO()
            {
                Idclient = client1.Idclient,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678915"
            };
            var result = await _controller.PutProfessionel(-1, p5);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Professionel?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Résultat pas BadRequest");

        }


        [TestMethod()]
        public async Task PutProfessionnelTest_Introuvable()
        {
            ProfessionelDTO p6 = new ProfessionelDTO()
            {
                Idclient = -1,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678916"
            };
            var result = await _controller.PutProfessionel(-1, p6);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Professionel?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }

        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }



    }
}