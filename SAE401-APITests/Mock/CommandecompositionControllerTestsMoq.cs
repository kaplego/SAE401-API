using Microsoft.AspNetCore.Mvc;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class CommandecompositionControllerTestsMoq
    {
        private Mock<ICommandecompositionRepository<Commandecomposition>> _repository;
        private CommandecompositionController _controller;
        private Commandecomposition commandecompo1;
        private Commande cmd1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<ICommandecompositionRepository<Commandecomposition>>();
            _controller = new CommandecompositionController(_repository.Object);
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
            commandecompo1 = new Commandecomposition()
            {
                Idcomposition = 1,
                Idcommande = cmd1.Idcommande,
                Quantitecompositioncommande = 1
            };
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email"
            });
            _repository.Setup(x => x.GetCommandeByIdAsync(cmd1.Idcommande)).ReturnsAsync(cmd1);
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
            Commandecomposition c3 = new Commandecomposition()
            {
                Idcomposition = c2.Idcomposition,
                Idcommande = c2.Idcommande,
                Quantitecompositioncommande = c2.Quantitecompositioncommande
            };
            _repository.Setup(x => x.AddCommandecompositionAsync(c3)).ReturnsAsync(c3);
            var result = await _controller.PostCommandecomposition(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commandecomposition?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Commandecomposition valeur = (Commandecomposition)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(c2.Quantitecompositioncommande, valeur.Quantitecompositioncommande, "commande compositions égales");
        }

    }
}