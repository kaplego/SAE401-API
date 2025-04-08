using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class AdresseControllerTestsMoq
    {
        private Mock<IAdresseRepository<Adresse>> _repository;
        private AdresseController _controller;
        private Client client1;
        private Adresse a1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IAdresseRepository<Adresse>>();
            _controller = new AdresseController(_repository.Object);
            a1 = new Adresse()
            {
                Idadresse = 1,
                Idclient = 0,
                Idpays = 1,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            _repository.Setup(x => x.GetAdresseByIdAsync(1)).ReturnsAsync(new ActionResult<Adresse?>(a1));
            _repository.Setup(x => x.GetAdresseByIdAsync(0)).ReturnsAsync(new ActionResult<Adresse?>(value: null));
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email",
            });
        }

        [TestMethod()]
        public async Task PostAdresseTest_Normal()
        {
            AdresseDTO a2 = new AdresseDTO()
            {
                Idclient = 0,
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
            try { await _repository.Object.DeleteAdresseAsync(valeur); } catch { }
        }

        /*
        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostAdresseTest_Invalide()
        {
            AdresseDTO a2 = new AdresseDTO()
            {
                Idclient = 0,
                Idpays = 1,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = null
            };
            var result = await _controller.PostAdresse(a2);
        }
        */

        [TestMethod()]
        public async Task PutAdresseTest_Normal()
        {
            AdresseDTO a3 = new AdresseDTO()
            {
                Idadresse = a1.Idadresse,
                Idclient = 0,
                Idpays = 2,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            _repository.Setup(x => x.UpdateAdresseAsync(a1, a3)).ReturnsAsync(new Adresse()
            {
                Idadresse = (int)a3.Idadresse,
                Idclient = a3.Idclient,
                Idpays = a3.Idpays,
                Codeinsee = a3.Codeinsee,
                Iddepartement = a3.Iddepartement,
                Codepostaladresse = a3.Codepostaladresse,
                Nomrue = a3.Nomrue
            });
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
                Idclient = 0,
                Idpays = 2,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            var result = await _controller.PutAdresse(1, a3);
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
                Idadresse = 0,
                Idclient = 0,
                Idpays = 2,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            var result = await _controller.PutAdresse(0, a3);
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
                Idclient = 0,
                Idpays = 1,
                Codeinsee = "74010",
                Iddepartement = 74,
                Codepostaladresse = "74000",
                Nomrue = "rue de chez moi"
            };
            _repository.Setup(x => x.GetAdresseByIdAsync(a4.Idadresse)).ReturnsAsync(new ActionResult<Adresse?>(a4));
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
    }
}