using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.DTO;
using SAE401_API.Models.Repository;
using SAE401_API.Models.DataMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using DotNetEnv;
using Newtonsoft.Json.Linq;
using Sprache;

namespace SAE401_API.Controllers.Tests
{
    [TestClass()]
    public class CartebancaireControllerTestsMoq
    {

       

        [TestMethod]
        public async Task GetAllCarteBancaireByIdClientExistantAvecMoq()
        {
        Client client = new Client()
        {
            Idclient = 1,
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

                // Arrange
        Cartebancaire cartebancaire = new Cartebancaire
        {
            Idclient = 1,
            Dateenregistement = DateTime.UtcNow,
            Titulairecartebancaire = "Nom",
            Numcartebancaire = "4444333322221111",
            Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
        };


        // Créer le mock pour le repository
        var mockRepository = new Mock<ICartebancaireRepository<Cartebancaire>>();

        // Configurer le mock pour qu'il retourne un ActionResult contenant la liste de cartes bancaires
        mockRepository.Setup(x => x.GetClientByIdAsync(1))
                      .ReturnsAsync(new ActionResult<Client>(client ));  // Retourne un ActionResult

        mockRepository.Setup(x => x.GetAllCartebancaireByClientAsync(1))
                        .ReturnsAsync(new ActionResult<IEnumerable<Cartebancaire>>(new List<Cartebancaire>() { cartebancaire}));  // Retourne un ActionResult

        // Créer le contrôleur
        var cartebancaireController = new CartebancaireController(mockRepository.Object);


        cartebancaireController.ControllerContext = JwtManager.CreateControllerContext(client);

        // Act
        var actionResult = await cartebancaireController.GetAllCartebancaireByClient(client.Idclient);

        


        // Assert
        Assert.IsNotNull(actionResult, "Retour est null");
        Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<Cartebancaire>>), "Pas un ActionResult");
        Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<Cartebancaire>), "Pas des cartes bancaires");
        Assert.AreEqual(cartebancaire, actionResult.Value.First(), "Cartes bancaires égales");
        }

        [TestMethod]
        public async Task GetAllCarteBancaireByIdClientInexistantAvecMoq()
        {
            Client client = new Client()
            {
                Idclient = 1,
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

            // Arrange
            Cartebancaire cartebancaire = new Cartebancaire
            {
                Idclient = 1,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Nom",
                Numcartebancaire = "4444333322221111",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
            };


            // Créer le mock pour le repository
            var mockRepository = new Mock<ICartebancaireRepository<Cartebancaire>>();

            // Configurer le mock pour qu'il retourne un ActionResult contenant la liste de cartes bancaires
            mockRepository.Setup(x => x.GetClientByIdAsync(1))
                          .ReturnsAsync(new ActionResult<Client>(client));  // Retourne un ActionResult

            mockRepository.Setup(x => x.GetAllCartebancaireByClientAsync(1))
                            .ReturnsAsync(new ActionResult<IEnumerable<Cartebancaire>>(new NotFoundResult()));  // Retourne un ActionResult

            // Créer le contrôleur
            var cartebancaireController = new CartebancaireController(mockRepository.Object);


            cartebancaireController.ControllerContext = JwtManager.CreateControllerContext(client);

            // Act
            var actionResult = await cartebancaireController.GetAllCartebancaireByClient(client.Idclient);




            Assert.IsNotNull(actionResult, "Retour est null");
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<Cartebancaire>>), "Pas un ActionResult");
            Assert.IsNotNull(actionResult.Result, "Erreur est null");
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Pas un NotFound");
            Assert.IsNull(actionResult.Value, "Valeur pas null");
        }



    }
}