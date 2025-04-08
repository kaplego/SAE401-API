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
using System.Text;
using System.Threading.Tasks;

namespace SAE401_APITests.Tests
{
    [TestClass()]
    public class ClientControllerTests
    {
        private _DBMilibooContext _context;
        private IClientRepository<Client> _repository;
        private ClientController _controller;
        private Client c1;


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
            _repository = new ClientManager(_context);
            _controller = new ClientController(_repository);

            c1 = new Client()
            {
                Nomclient = "NOM",
                Prenomclient = "Prenom" + DateTime.UtcNow.ToString(),
                Emailclient = "client@email.domain",
                Telportableclient = "33123456789",
                Datecreationcompte = DateTime.UtcNow,
                Hashmdp = "$2y$10$jqMsA9Suk5UBrJvkN4QRme6IHcnaZ4RLb89E5pXFHtYhtKNYIny9.", // mdp
                Pointfideliteclient = 0,
                Newslettermiliboo = true,
                Newsletterpartenaires = true
            };

            await _context.Clients.AddAsync(c1);
            await _context.SaveChangesAsync();

            _controller.ControllerContext = JwtManager.CreateControllerContext(c1);
        }

        [TestMethod()]
        public async Task GetClientByLoginTest_Normal()
        {
            ClientController.Login login = new ClientController.Login() { email = "client@email.domain", password = "mdp"};
            var result = await _controller.GetClientByLogin(login);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(IActionResult), "Pas un ActionResult");
            var res = (ActionResult)result;
            Assert.IsNotNull(res, "Résultat est null");
            Assert.IsInstanceOfType(res, typeof(OkObjectResult), "Résultat pas OK");
            dynamic valeur = ((ObjectResult)res).Value as dynamic;
            Assert.IsNotNull(valeur, "Valeur est null");
            string token = valeur.GetType().GetProperty("token").GetValue(valeur, null);
            Client client = valeur.GetType().GetProperty("client").GetValue(valeur, null);
            Assert.IsNotNull(token, "Token est null");
            Assert.IsNotNull(client, "Client est null");
            Assert.AreEqual(c1.Prenomclient, client.Prenomclient, "Client Égaux");
        }

        [TestMethod()]
        public async Task GetClientByLoginTest_MailInvalide()
        {
            ClientController.Login login = new ClientController.Login() { email = "client@email.invalide", password = "mdp" };
            var result = await _controller.GetClientByLogin(login);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(IActionResult), "Pas un ActionResult");
            var res = (ActionResult)result;
            Assert.IsNotNull(res, "Résultat est null");
            Assert.IsInstanceOfType(res, typeof(ForbidResult), "Résultat pas OK");
        }

        [TestMethod()]
        public async Task GetClientByLoginTest_MdpInvalide()
        {
            ClientController.Login login = new ClientController.Login() { email = "client@email.domain", password = "NOPE" };
            var result = await _controller.GetClientByLogin(login);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(IActionResult), "Pas un ActionResult");
            var res = (ActionResult)result;
            Assert.IsNotNull(res, "Résultat est null");
            Assert.IsInstanceOfType(res, typeof(ForbidResult), "Résultat pas OK");
        }


        [TestMethod()]
        public async Task GetClientByIdTest_Normal()
        {
            var result = await _controller.GetClientById(c1.Idclient);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client?>), "Pas un ActionResult");
            Assert.IsNull(result.Result, "Résultat est pas null");
            Assert.IsNotNull(result.Value, "Valeur est null");
            Assert.IsInstanceOfType(result.Value, typeof(Client), "Pas un Client");
            Assert.AreEqual(c1, result.Value, "Client égaux");
        }

        [TestMethod()]
        public async Task GetClientByIdTest_Innexistant()
        {
            var result = await _controller.GetClientById(-1);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(ForbidResult), "Pas un Forbid");
            Assert.IsNull(result.Value, "Valeur est pas null");
        }


        [TestMethod()]
        public async Task PostClientTest_Normal()
        {
            ClientDTO c2 = new ClientDTO()
            {
                Nomclient = "NOM2",
                Prenomclient = "Prenom" + DateTime.UtcNow.ToString(),
                Emailclient = "email@email.email",
                Telportableclient = "33123456789",
                Datecreationcompte = DateTime.UtcNow,
                Hashmdp = "mdp",
                Pointfideliteclient = 0,
                Newslettermiliboo = true,
                Newsletterpartenaires = true

            };
            var result = await _controller.PostClient(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Client valeur = (Client)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.IsInstanceOfType(valeur, typeof(Client), "Pas un client");
            Assert.AreEqual(c2.Prenomclient, valeur.Prenomclient, "clients égaux");
            try { _context.Clients.Remove(valeur); } catch { }
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostClientTest_Invalide()
        {
            ClientDTO c3 = new ClientDTO()
            {
                Nomclient = "NOM3",
                Prenomclient = "Prenom" + DateTime.UtcNow.ToString(),
                Emailclient = "email@email.email",
                Telportableclient = "331234567890",
                Datecreationcompte = DateTime.UtcNow,
                Hashmdp = "mdp",
                Pointfideliteclient = 0,
                Newslettermiliboo = true,
                Newsletterpartenaires = true

            };

            try
            {
                var result = await _controller.PostClient(c3);
            }
            catch (DbUpdateException ex)
            {
                _context.Clients.Remove((Client)ex.Entries.First().Entity);
                throw ex;
            }
        }


        [TestMethod()]
        public async Task PutClientTest_Normal()
        {
            ClientDTO c4 = new ClientDTO()
            {
                Idclient = c1.Idclient,
                Nomclient = "NOM4",
                Prenomclient = "Prenom" + DateTime.UtcNow.ToString(),
                Emailclient = "email@email.email",
                Telportableclient = "33123456789",
                Datecreationcompte = DateTime.UtcNow,
                Hashmdp = "nouv",
                Pointfideliteclient = 0,
                Newslettermiliboo = true,
                Newsletterpartenaires = true

            };
            var result = await _controller.PutClient(c1.Idclient, c4);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Client valeur = (Client)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.IsInstanceOfType(valeur, typeof(Client), "Pas un client");
            Assert.AreEqual(c4.Hashmdp, valeur.Hashmdp, "MDP égaux");
            Assert.AreEqual(c1.Idclient, valeur.Idclient, "Client non-modifiés (id)");
        }

        [TestMethod()]
        public async Task PutAdresseTest_Innégal()
        {
            ClientDTO c5 = new ClientDTO()
            {
                Idclient = -1,
                Nomclient = "NOM4",
                Prenomclient = "Prenom" + DateTime.UtcNow.ToString(),
                Emailclient = "email@email.email",
                Telportableclient = "33123456789",
                Datecreationcompte = DateTime.UtcNow,
                Hashmdp = "mdp",
                Pointfideliteclient = 0,
                Newslettermiliboo = true,
                Newsletterpartenaires = true

            };
            var result = await _controller.PutClient(c1.Idclient, c5);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Résultat pas BadRequest");
        }

        [TestMethod()]
        public async Task PutAdresseTest_Introuvable()
        {
            ClientDTO c5 = new ClientDTO()
            {
                Idclient = -1,
                Nomclient = "NOM4",
                Prenomclient = "Prenom" + DateTime.UtcNow.ToString(),
                Emailclient = "email@email.email",
                Telportableclient = "33123456789",
                Datecreationcompte = DateTime.UtcNow,
                Hashmdp = "mdp",
                Pointfideliteclient = 0,
                Newslettermiliboo = true,
                Newsletterpartenaires = true

            };
            var result = await _controller.PutClient(-1, c5);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(ForbidResult), "Résultat pas Forbid");
        }


        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Clients.Remove(c1);
            await _context.SaveChangesAsync();
        }
    }
}