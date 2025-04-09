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
    public class ProfessionelControllerTestsMoq
    {
        private Mock<IProfessionelRepository<Professionel>> _repository;
        private ProfessionelController _controller;
        private Professionel p1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IProfessionelRepository<Professionel>>();
            _controller = new ProfessionelController(_repository.Object);
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email"
            });
            p1 = new Professionel()
            {
                Idclient = 0,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678910"
            };
            _repository.Setup(x => x.GetProfessionelByIdAsync(p1.Idclient)).ReturnsAsync(new ActionResult<Professionel?>(p1));
            _repository.Setup(x => x.GetProfessionelByIdAsync(-1)).ReturnsAsync(new ActionResult<Professionel?>((Professionel?)null));
            _controller.ControllerContext = JwtManager.CreateControllerContext(new Client()
            {
                Idclient = 0,
                Emailclient = "email@email.email"
            });
        }

        [TestMethod()]
        public async Task PostProfessionnelTest_Normal()
        {
            ProfessionelDTO p2 = new ProfessionelDTO()
            {
                Idclient = 0,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678911"
            };
            Professionel p3 = new Professionel()
            {
                Idclient = 0,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678911"
            };
            _repository.Setup(x => x.AddProfessionelAsync(p3)).ReturnsAsync(p3);
            var result = await _controller.PostProfessionel(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Professionel?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Professionel valeur = (Professionel)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.Nomsociete, valeur.Nomsociete, "professionnels égales");
        }

        [TestMethod()]
        public async Task PutProfessionnelTest_Normal()
        {

            ProfessionelDTO p2 = new ProfessionelDTO()
            {
                Idclient = 0,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678914"
            };
            Professionel p3 = new Professionel()
            {
                Idclient = 0,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678914"
            };
            _repository.Setup(x => x.UpdateProfessionelAsync(p1, p2)).ReturnsAsync(p3);
            var result = await _controller.PutProfessionel(p1.Idclient, p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Professionel?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Professionel valeur = (Professionel)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p3.Nomsociete, valeur.Nomsociete, "professionels égales (titulaire)");
            Assert.AreEqual(p1.Idclient, valeur.Idclient, "professionnels non-modifiées (id)");
            Assert.AreEqual(p3.Numtva, valeur.Numtva, "professionnels égales (dateexp)");
        }

        [TestMethod()]
        public async Task PutProfessionnelTest_Innégal()
        {

            ProfessionelDTO p5 = new ProfessionelDTO()
            {
                Idclient = 0,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678915"
            };
            var result = await _controller.PutProfessionel(-1, p5);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Professionel?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Résultat pas BadRequest");

        }


        [TestMethod()]
        public async Task PutProfessionnelTest_Introuvable()
        {
            ProfessionelDTO p6 = new ProfessionelDTO()
            {
                Idclient = -1,
                Idactivitepro = 1,
                Nomsociete = "Test",
                Numtva = "12345678916"
            };
            var result = await _controller.PutProfessionel(-1, p6);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Professionel?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }


    }
}