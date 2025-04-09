using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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
    public class DetailcommandeControllerTestsMoq
    {
        private Mock<IDetailcommandeRepository<Detailcommande>> _repository;
        private DetailcommandeController _controller;
        private Detailcommande detail1;
        private Commande cmd1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IDetailcommandeRepository<Detailcommande>>();
            _controller = new DetailcommandeController(_repository.Object);
            cmd1 = new Commande()
            {
                Idclient = 0,
                IdadresseLivr = 1,
                IdadresseFact = 1,
                Idstatut = 1,
                Idtransporteur = 1,
                Avecassurance = true,
                Aveclivraisonexpress = true
            };
            detail1 = new Detailcommande()
            {
                Idproduit = 1,
                Idcouleur = 7,
                Idcommande = cmd1.Idcommande,
                Quantitecommande = 1
            };
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email"
            });
            _repository.Setup(x => x.GetCommandeByIdAsync(cmd1.Idcommande)).ReturnsAsync(cmd1);
        }



        [TestMethod()]
        public async Task PostDetailcommandeTest_Normal()
        {
            DetailcommandeDTO c2 = new DetailcommandeDTO()
            {
                Idproduit = 1,
                Idcouleur = 5,
                Idcommande = cmd1.Idcommande,
                Quantitecommande = 1
            };
            Detailcommande c3 = new Detailcommande()
            {
                Idproduit = 1,
                Idcouleur = 5,
                Idcommande = cmd1.Idcommande,
                Quantitecommande = 1
            };
            _repository.Setup(x => x.AddDetailcommandeAsync(c3)).ReturnsAsync(c3);
            var result = await _controller.PostDetailcommande(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Detailcommande?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Detailcommande valeur = (Detailcommande)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c2.Quantitecommande, valeur.Quantitecommande, "detail commandes égales");
        }
    }
}