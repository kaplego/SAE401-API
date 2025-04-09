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
    public class CommandecompositionControllerTests
    {
        private _DBMilibooContext _context;
        private ICommandecompositionRepository<Commandecomposition> _repository;
        private CommandecompositionController _controller;
        private Client client1;
        private Commande cmd1;
        private Commandecomposition commandecompo1;


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
            _repository = new CommandecompositionManager<Commandecomposition>(_context);
            _controller = new CommandecompositionController(_repository);
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
            cmd1 = new Commande()
            {
                Idclient = client1.Idclient,
                IdadresseLivr = 1,
                IdadresseFact = 1,
                Idstatut = 1,
                Idtransporteur = 1,
                Avecassurance = true,
                Aveclivraisonexpress = true
            };
            await _context.Commandes.AddAsync(cmd1);
            await _context.SaveChangesAsync();
            commandecompo1 = new Commandecomposition()
            {
                Idcomposition = 1,
                Idcommande = cmd1.Idcommande,
                Quantitecompositioncommande = 1
            };
            await _context.Commandecompositions.AddAsync(commandecompo1);
            await _context.SaveChangesAsync();
            _controller.ControllerContext = JwtManager.CreateControllerContext(client1);
        }



        [TestMethod()]
        public async Task PostCommandecompositionTest_Normal()
        {
            CommandecompositionDTO c2 = new CommandecompositionDTO()
            {
                Idcomposition = 2,
                Idcommande = cmd1.Idcommande,
                Quantitecompositioncommande = 1
            };
            var result = await _controller.PostCommandecomposition(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commandecomposition?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Commandecomposition valeur = (Commandecomposition)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c2.Quantitecompositioncommande, valeur.Quantitecompositioncommande, "commande compositions égales");
            try { _context.Commandecompositions.Remove(valeur); } catch { }
        }






        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Commandecompositions.Remove(commandecompo1);
            _context.Commandes.Remove(cmd1);
            _context.Clients.Remove(client1);
            await _context.SaveChangesAsync();
        }


    }
}