using Microsoft.AspNetCore.Mvc;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SAE401_API.Controllers.Tests
{
    [TestClass()]
    public class CommandeControllerTests
    {
        private Mock<ICommandeRepository<Commande>> _mockCommandeRepository;
        private CommandeController _controller;

        [TestInitialize()]
        public void Initialize()
        {
            _mockCommandeRepository = new Mock<ICommandeRepository<Commande>>();
            _controller = new CommandeController(_mockCommandeRepository.Object);
        }

        [TestMethod()]
        public void CommandeControllerTest()
        {
            // Arrange : Aucun besoin d'arrangement spécifique pour ce test.
            // Le constructeur de CommandeController est testé indirectement.

            // Act : Instanciation de l'objet dans Initialize()

            // Assert : Aucune assertion n'est nécessaire dans ce test car nous ne faisons rien ici.
            Assert.IsNotNull(_controller);
        }

        [TestMethod()]
        public async Task PostCommandeTest_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var commandeDTO = new CommandeDTO
            {
                Idcommande = 1,
                Idclient = 123,
                IdadresseLivr = 456,
                IdadresseFact = 789,
                Idcodepromo = 1011,
                Idstatut = 1,
                Idtransporteur = 999,
                Datecommande = DateTime.Now,
                Avecassurance = true,
                Aveclivraisonexpress = false,
                Instructionlivraison = "Livrer à l'adresse principale"
            };

            _mockCommandeRepository
                .Setup(repo => repo.AddCommandeAsync(It.IsAny<Commande>()))
                .Returns(Task.CompletedTask); // On simule l'ajout réussi d'une commande

            // Act
            var result = await _controller.PostCommande(commandeDTO);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetCommandeById", createdResult.ActionName);
            Assert.AreEqual(1, createdResult.RouteValues["idcommande"]);
        }

        [TestMethod()]
        public async Task PostCommandeTest_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Idcommande", "Required");

            var commandeDTO = new CommandeDTO
            {
                Idcommande = 0, // Model invalide ici
                Idclient = 123,
                IdadresseLivr = 456,
                IdadresseFact = 789,
                Idcodepromo = 1011,
                Idstatut = 1,
                Idtransporteur = 999,
                Datecommande = DateTime.Now,
                Avecassurance = true,
                Aveclivraisonexpress = false,
                Instructionlivraison = "Livrer à l'adresse principale"
            };

            // Act
            var result = await _controller.PostCommande(commandeDTO);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }

        [TestMethod()]
        public async Task GetCommandeByIdTest_ValidId_ReturnsCommande()
        {
            // Arrange
            var commande = new Commande
            {
                Idcommande = 1,
                Idclient = 123,
                IdadresseLivr = 456,
                IdadresseFact = 789,
                Idcodepromo = 1011,
                Idstatut = 1,
                Idtransporteur = 999,
                Datecommande = DateTime.Now,
                Avecassurance = true,
                Aveclivraisonexpress = false,
                Instructionlivraison = "Livrer à l'adresse principale"
            };

            _mockCommandeRepository
                .Setup(repo => repo.GetCommandeByIdAsync(1))
                .ReturnsAsync(new ActionResult<Commande>(commande));

            // Act
            var result = await _controller.GetCommandeById(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(commande, okResult.Value);
        }

        [TestMethod()]
        public async Task GetCommandeByIdTest_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockCommandeRepository
                .Setup(repo => repo.GetCommandeByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new ActionResult<Commande>(null));

            // Act
            var result = await _controller.GetCommandeById(999);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
        }
    }
}
