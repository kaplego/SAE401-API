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
    public class PaiementControllerTestsMoq
    {
        private Mock<IPaiementRepository<Paiement>> _repository;
        private PaiementController _controller;
        private Paiement p1;
        private Commande c1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            
            _repository = new Mock<IPaiementRepository<Paiement>>();
            _controller = new PaiementController(_repository.Object);
            p1 = new Paiement()
            {
                Idcartebancaire = 0,
                Idcommande = 0,
                Idtypepaiement = 1,
                Datepaiement = DateTime.UtcNow,
                Montantpaiement = 10,
                Indicepaiement = "Test"
            };
            c1 = new Commande
            {
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

            _repository.Setup(x => x.GetCommandeByIdAsync(c1.Idcommande)).ReturnsAsync(new ActionResult<Commande>(c1));
        }


        [TestMethod()]
        public async Task PostPaiementTest_Normal()
        {
            PaiementDTO p2 = new PaiementDTO()
            {
                Idcartebancaire = 23,
                Idcommande = c1.Idcommande,
                Idtypepaiement = 1,
                Datepaiement = DateTime.UtcNow,
                Montantpaiement = 10,
                Indicepaiement = "Test"
            };
            Paiement p3 = new Paiement()
            {
                Idcartebancaire = p2.Idcartebancaire,
                Idcommande = p2.Idcommande,
                Idtypepaiement = p2.Idtypepaiement,
                Datepaiement = p2.Datepaiement,
                Montantpaiement = p2.Montantpaiement,
                Indicepaiement = p2.Indicepaiement,
            };
            _repository.Setup(x => x.AddPaiementAsync(p3)).ReturnsAsync(p3);
            var result = await _controller.PostPaiement(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Paiement?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Paiement valeur = (Paiement)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.Indicepaiement, valeur.Indicepaiement, "paiements égales");
        }


    }
}