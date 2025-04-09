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
    public class ClientControllerTestsMoq
    {
        private Mock<IClientRepository<Client>> _repository;
        private ClientController _controller;
        private Client c1;


        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IClientRepository<Client>>();
            _controller = new ClientController(_repository.Object);

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
            _repository.Setup(x => x.GetClientByIdAsync(c1.Idclient)).ReturnsAsync(c1);
            _controller.ControllerContext = JwtManager.CreateControllerContext(c1);
        }

        [TestMethod()]
        public async Task GetClientByLoginTest_Normal()
        {
            ClientController.Login login = new ClientController.Login() { email = "client@email.domain", password = "mdp" };
            _repository.Setup(x => x.GetClientByLoginAsync(login.email, login.password)).ReturnsAsync(c1);
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
            _repository.Setup(x => x.GetClientByLoginAsync(login.email, login.password)).ReturnsAsync(value: null);
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
            _repository.Setup(x => x.GetClientByLoginAsync(login.email, login.password)).ReturnsAsync(value: null);
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
            _repository.Setup(x => x.GetClientByIdAsync(-1)).ReturnsAsync(value: (Client?)null);
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
                Idclient = -1,
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
            Client c3 = new Client()
            {
                Idclient = (int)c2.Idclient,
                Nomclient = c2.Nomclient,
                Prenomclient = c2.Prenomclient,
                Emailclient = c2.Emailclient,
                Telportableclient = c2.Telportableclient,
                Datecreationcompte = c2.Datecreationcompte,
                Hashmdp = c2.Hashmdp,
                Pointfideliteclient = c2.Pointfideliteclient,
                Newslettermiliboo = c2.Newslettermiliboo,
                Newsletterpartenaires = c2.Newsletterpartenaires
            };
            _repository.Setup(x => x.AddClientAsync(c3)).ReturnsAsync(c3);
            var result = await _controller.PostClient(c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Client valeur = (Client)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.IsInstanceOfType(valeur, typeof(Client), "Pas un client");
            Assert.AreEqual(c2.Prenomclient, valeur.Prenomclient, "clients égaux");
        }


        [TestMethod()]
        public async Task PutClientTest_Normal()
        {
            ClientDTO c2 = new ClientDTO()
            {
                Idclient = c1.Idclient,
                Nomclient = "NOM",
                Prenomclient = "Prenom" + DateTime.UtcNow.ToString(),
                Emailclient = "client@email.domain",
                Telportableclient = "33123456789",
                Datecreationcompte = DateTime.UtcNow,
                Hashmdp = "nouv",
                Pointfideliteclient = 0,
                Newslettermiliboo = true,
                Newsletterpartenaires = true
            };
            Client c3 = new Client()
            {
                Idclient = (int)c2.Idclient,
                Nomclient = c2.Nomclient,
                Prenomclient = c2.Prenomclient,
                Emailclient = c2.Emailclient,
                Telportableclient = c2.Telportableclient,
                Datecreationcompte = c2.Datecreationcompte,
                Hashmdp = c2.Hashmdp,
                Pointfideliteclient = c2.Pointfideliteclient,
                Newslettermiliboo = c2.Newslettermiliboo,
                Newsletterpartenaires = c2.Newsletterpartenaires
            };
            _repository.Setup(x => x.UpdateClientAsync(c1, c2)).ReturnsAsync(c3);
            var result = await _controller.PutClient(c1.Idclient, c2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Client valeur = (Client)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.IsInstanceOfType(valeur, typeof(Client), "Pas un client");
            Assert.AreEqual(c2.Hashmdp, valeur.Hashmdp, "MDP pas modifié");
            Assert.AreEqual(c1.Idclient, valeur.Idclient, "Client non-modifiés (id)");
        }

        [TestMethod()]
        public async Task PutClientTest_Innégal()
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
        public async Task PutClientTest_Introuvable()
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
            _repository.Setup(x => x.GetClientByIdAsync(-1)).ReturnsAsync(value: (Client?)null);
            var result = await _controller.PutClient(-1, c5);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(ForbidResult), "Résultat pas Forbid");
        }
    }
}