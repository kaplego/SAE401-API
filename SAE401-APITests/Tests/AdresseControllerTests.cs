using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Protocol.Core.Types;
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

namespace SAE401_API.Controllers.Tests
{
    [TestClass()]
    public class AdresseControllerTests
    {
        private _DBMilibooContext _context;
        private IAdresseRepository<Adresse> _repository;
        private AdresseController _controller;
        private Client client1;
        private Adresse a1;

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
            _repository = new AdresseManager<Adresse>(_context);
            _controller = new AdresseController(_repository);
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
            a1 = new Adresse()
            {
                Idclient = client1.Idclient,
                Idpays = 1,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            await _context.Adresses.AddAsync(a1);
            await _context.SaveChangesAsync();
            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);
        }

        [TestMethod()]
        public async Task PostAdresseTest_Normal()
        {
            AdresseDTO a2 = new AdresseDTO()
            {
                Idclient = client1.Idclient,
                Idpays = 1,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            var result = await _controller.PostAdresse(a2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Adresse valeur = (Adresse)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(a2.Nomrue, valeur.Nomrue, "Cartes bancaires égales");
            try { _context.Adresses.Remove(valeur); } catch { }
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostAdresseTest_Invalide()
        {
            AdresseDTO a2 = new AdresseDTO()
            {
                Idclient = client1.Idclient,
                Idpays = 1,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = null
            };
            try
            {
                var result = await _controller.PostAdresse(a2);
            }
            catch (DbUpdateException ex)
            {
                try { _context.Adresses.RemoveRange(client1.AdressesNavigation.Where(x => x.Nomrue == null)); }
                catch { throw ex; }
                throw ex;
            }
        }

        [TestMethod()]
        public async Task PutAdresseTest_Normal()
        {
            AdresseDTO a3 = new AdresseDTO()
            {
                Idadresse = a1.Idadresse,
                Idclient = client1.Idclient,
                Idpays = 2,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            var result = await _controller.PutAdresse(a1.Idadresse, a3);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Adresse valeur = (Adresse)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(a3.Idpays, valeur.Idpays, "Adresses égales (pays)");
            Assert.AreEqual(a1.Idadresse, valeur.Idadresse, "Adresses non-modifiées (id)");
        }

        [TestMethod()]
        public async Task PutAdresseTest_Innégal()
        {
            AdresseDTO a3 = new AdresseDTO()
            {
                Idadresse = 0,
                Idclient = client1.Idclient,
                Idpays = 2,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            var result = await _controller.PutAdresse(-1, a3);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Résultat pas BadRequest");
        }

        [TestMethod()]
        public async Task PutAdresseTest_Introuvable()
        {
            AdresseDTO a3 = new AdresseDTO()
            {
                Idadresse = -1,
                Idclient = client1.Idclient,
                Idpays = 2,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            var result = await _controller.PutAdresse(-1, a3);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }


        [TestMethod()]
        public async Task DeleteAdresseTest_Normal()
        {
            Adresse a4 = new Adresse()
            {
                Idclient = client1.Idclient,
                Idpays = 1,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            await _context.Adresses.AddAsync(a4);
            await _context.SaveChangesAsync();
            var result = await _controller.DeleteAdresse(a4.Idadresse);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod()]
        public async Task DeleteAdresseTest_Introuvable()
        {
            var result = await _controller.DeleteAdresse(0);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }

        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Adresses.RemoveRange(client1.AdressesNavigation);
            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }
    }
}