using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class CommandeControllerTestsMoq
    {
        private Mock<ICommandeRepository<Commande>> _repository;
        private CommandeController _controller;
        private Commande cd1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<ICommandeRepository<Commande>>();
            _controller = new CommandeController(_repository.Object);
            cd1 = new Commande()
            {
                Idcommande = 1,
                Idclient = 0,
                IdadresseLivr = 1,
                IdadresseFact = 1,
                Idcodepromo = 1,
                Idstatut = 1,
                Idtransporteur = 1,
                Datecommande = DateTime.UtcNow,
                Avecassurance = true,
                Aveclivraisonexpress = true,
                Instructionlivraison = "Test"
            };
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email"
            });
            _repository.Setup(x => x.GetCommandeByIdAsync(cd1.Idcommande)).ReturnsAsync(cd1);
            _repository.Setup(x => x.GetCommandeByIdAsync(0)).ReturnsAsync(value: (Commande?)null);
        }

        [TestMethod()]
        public async Task GetCommandeById_Normal()
        {
            var commande = await _controller.GetCommandeById(cd1.Idcommande);
            Assert.IsNotNull(commande, "Retour est null");
            Assert.IsInstanceOfType(commande, typeof(ActionResult<Commande>), "Pas un ActionResult");
            Assert.IsInstanceOfType(commande.Value, typeof(Commande), "Pas une coloration");
            Assert.AreEqual(cd1, commande.Value, "colorations égales");
        }

        [TestMethod()]
        public async Task GetCommandeById_Inexistant()
        {
            var commande = await _controller.GetCommandeById(0);
            Assert.IsNotNull(commande, "Retour est null");
            Assert.IsInstanceOfType(commande, typeof(ActionResult<Commande>), "Pas un ActionResult");
            Assert.IsNotNull(commande.Result, "Erreur est null");
            Assert.IsInstanceOfType(commande.Result, typeof(NotFoundResult), "Pas un NotFound");
            Assert.IsNull(commande.Value, "Valeur pas null");
        }


        [TestMethod()]
        public async Task PostCommandeTest_Normal()
        {
            CommandeDTO cd2 = new CommandeDTO()
            {
                Idcommande = 1,
                Idclient = 0,
                IdadresseLivr = 1,
                IdadresseFact = 1,
                Idcodepromo = 1,
                Idstatut = 1,
                Idtransporteur = 1,
                Datecommande = DateTime.UtcNow,
                Avecassurance = true,
                Aveclivraisonexpress = true,
                Instructionlivraison = "Test"
            };
            Commande cd3 = new Commande()
            {
                Idcommande = (int)cd2.Idcommande,
                Idclient = cd2.Idclient,
                IdadresseLivr = cd2.IdadresseLivr,
                IdadresseFact = cd2.IdadresseFact,
                Idcodepromo = cd2.Idcodepromo,
                Idstatut = cd2.Idstatut,
                Idtransporteur = cd2.Idtransporteur,
                Datecommande = cd2.Datecommande,
                Avecassurance = cd2.Avecassurance,
                Aveclivraisonexpress = cd2.Aveclivraisonexpress,
                Instructionlivraison = cd2.Instructionlivraison
            };
            _repository.Setup(x => x.AddCommandeAsync(cd3)).ReturnsAsync(cd3);
            var result = await _controller.PostCommande(cd2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commande?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Commande valeur = (Commande)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(cd2.IdadresseLivr, valeur.IdadresseLivr, "commandes égales");
        }
    }
}