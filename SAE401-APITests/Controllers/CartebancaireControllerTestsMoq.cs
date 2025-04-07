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
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;

namespace SAE401_API.Controllers.Tests
{
    [TestClass()]
    public class CartebancaireControllerTestsMoq
    {

       

        [TestMethod]
        public async Task TestGetAllCarteBancaireByIdClientExistantAvecMoq()
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
        public async Task TestGetAllCarteBancaireByIdClientInexistantAvecMoq()
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


        [TestMethod]
        public async Task TestDeleteCarteBancaireExistantAvecMoq()
        {
            // Arrange

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


            Cartebancaire cartebancaire = new Cartebancaire
            {
                Idcartebancaire = 1,
                Idclient = 1,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Nom",
                Numcartebancaire = "4444333322221111",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
            };
            var mockRepository = new Mock<ICartebancaireRepository<Cartebancaire>>();

            mockRepository.Setup(x => x.DeleteCartebancaireAsync(cartebancaire))
                            .Returns(Task.CompletedTask);

            mockRepository.Setup(x => x.GetCartebancaireByIdAsync(1))
                            .ReturnsAsync(new ActionResult<Cartebancaire>(cartebancaire));  // Retourne un ActionResult

            var cartebancaireController = new CartebancaireController(mockRepository.Object);

            cartebancaireController.ControllerContext = JwtManager.CreateControllerContext(client);


            // Act
            var actionResult = await cartebancaireController.DeleteCartebancaire(cartebancaire.Idcartebancaire);
            // Assert
            Assert.IsNotNull(actionResult, "Retour est null");
            Assert.IsInstanceOfType(actionResult, typeof(OkResult), "Pas un OkResult");
        }

        [TestMethod]
        public async Task TestPostCarteBancaireExistantAvecMoq()
        {
            // Arrange

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


            Cartebancaire cartebancaire = new Cartebancaire
            {
                Idcartebancaire = 1,
                Idclient = 1,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Nom",
                Numcartebancaire = "4444333322221111",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
            };

            CartebancaireDTO cartebancaire2 = new CartebancaireDTO
            {
                Idcartebancaire = 1,
                Idclient = 1,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Nom",
                Numcartebancaire = "4444333322221111",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
            };
            var mockRepository = new Mock<ICartebancaireRepository<Cartebancaire>>();

            mockRepository.Setup(x => x.AddCartebancaireAsync(cartebancaire))
                            .ReturnsAsync(cartebancaire);

         

            var cartebancaireController = new CartebancaireController(mockRepository.Object);

            cartebancaireController.ControllerContext = JwtManager.CreateControllerContext(client);


            // Act
            var actionResult = await cartebancaireController.PostCartebancaire(cartebancaire2);



            // Assert
            Assert.IsNotNull(actionResult, "Retour est null");
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Cartebancaire?>), "Pas un ActionResult");
            Assert.IsNotNull(actionResult.Result, "Résultat est null");
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult), "Résultat pas OK");
            Cartebancaire valeur = (Cartebancaire)((ObjectResult)actionResult.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(cartebancaire.Numcartebancaire, valeur.Numcartebancaire, "Cartes bancaires égales");
        }



        [TestMethod]
        public async Task TestPutCarteBancaireExistantAvecMoq()
        {
            // Arrange

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


            Cartebancaire cartebancaire = new Cartebancaire
            {
                Idcartebancaire = 1,
                Idclient = 1,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Nom",
                Numcartebancaire = "4444333322221111",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
            };

            Cartebancaire cartebancaire2 = new Cartebancaire
            {
                Idcartebancaire = 1,
                Idclient = 1,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Test",
                Numcartebancaire = "4444333322221111",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
            };


            CartebancaireDTO cartebancaire3 = new CartebancaireDTO
            {
                Idcartebancaire = 1,
                Idclient = 1,
                Dateenregistement = DateTime.UtcNow,
                Titulairecartebancaire = "Test",
                Numcartebancaire = "4444333322221111",
                Dateexpirationcarte = DateTime.UtcNow.AddDays(1)
            };

            var mockRepository = new Mock<ICartebancaireRepository<Cartebancaire>>();

            mockRepository.Setup(x => x.UpdateCartebancaireAsync(cartebancaire,cartebancaire2))
                            .ReturnsAsync(cartebancaire2);

            mockRepository.Setup(x => x.GetCartebancaireByIdAsync(1))
                            .ReturnsAsync(new ActionResult<Cartebancaire>(cartebancaire));  // Retourne un ActionResult

            var cartebancaireController = new CartebancaireController(mockRepository.Object);

            cartebancaireController.ControllerContext = JwtManager.CreateControllerContext(client);


            // Act
            var actionResult = await cartebancaireController.PutCartebancaire(cartebancaire.Idcartebancaire,cartebancaire3);
            
            
            // Assert
            Assert.IsNotNull(actionResult, "Retour est null");
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Cartebancaire?>), "Pas un ActionResult");
            Assert.IsNotNull(actionResult.Result, "Résultat est null");
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult), "Résultat pas OK");
           

            Cartebancaire valeur = (Cartebancaire)((ObjectResult)actionResult.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(cartebancaire2.Titulairecartebancaire, valeur.Titulairecartebancaire, "Cartes bancaires égales (num)");
            Assert.AreEqual(cartebancaire.Idcartebancaire, valeur.Idcartebancaire, "Cartes bancaires non-modifiées (id)");
            Assert.AreEqual(cartebancaire2.Dateexpirationcarte, valeur.Dateexpirationcarte, "Cartes bancaires égales (dateexp)");
        }
    }
}