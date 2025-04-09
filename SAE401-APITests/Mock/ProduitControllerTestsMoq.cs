using Microsoft.AspNetCore.Mvc;
using Moq;
using SAE401_API.Controllers;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Mock
{
    [TestClass()]
    public class ProduitControllerTestsMoq
    {
        private Mock<IProduitRepository<Produit>> _repository;
        private ProduitController _controller;
        private Produit p1;



        [TestInitialize]
        public async Task TestInitialize()
        {
            _repository = new Mock<IProduitRepository<Produit>>();
            _controller = new ProduitController(_repository.Object);


            p1 = new Produit()
            {
                Idtypeproduit = 1,
                Idpays = 1,
                Nomproduit = "Test très très précis sur les produits omg",
                Notice = "Test",
                Aspecttechnique = "Test",
                Delailivraison = 1,
                Coutlivraison = 1,
                Nbpaiementmax = 1
            };

            _repository.Setup(x => x.GetAllProduitAsync()).ReturnsAsync(new ActionResult<IEnumerable<Produit>>(new List<Produit>() { p1 }));
            _repository.Setup(x => x.GetAllProduitByCategorieAsync(1)).ReturnsAsync(new ActionResult<IEnumerable<Produit>>(new List<Produit>() { p1 }));
            _repository.Setup(x => x.GetAllProduitByRechercheAsync("Test", 2)).ReturnsAsync(new ActionResult<IEnumerable<Produit>>(new List<Produit>() { p1 }));
            _repository.Setup(x => x.GetAllProduitByRegroupementAsync(1)).ReturnsAsync(new ActionResult<IEnumerable<Produit>>(new List<Produit>() { p1 }));
            _repository.Setup(x => x.GetAllProduitByTypeAsync(1)).ReturnsAsync(new ActionResult<IEnumerable<Produit>>(new List<Produit>() { p1 }));
            _repository.Setup(x => x.GetProduitByIdAsync(p1.Idproduit)).ReturnsAsync(p1);
            _repository.Setup(x => x.GetProduitByIdAsync(-1)).ReturnsAsync((Produit?)null);
        }


        [TestMethod()]
        public async Task GetAllProduitsTest_Normal()
        {
            var produits = await _controller.GetAllProduit();
            Assert.IsNotNull(produits, "Retour est null");
            Assert.IsInstanceOfType(produits, typeof(ActionResult<IEnumerable<Produit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(produits.Value, typeof(IEnumerable<Produit>), "Pas des produits ");
            Assert.AreEqual(p1, produits.Value.Last(), "produits égales");
        }

        [TestMethod()]
        public async Task GetAllProduitsByRechercheTest_Normal()
        {
            var produits = await _controller.GetAllProduitByRecherche("Test");
            Assert.IsNotNull(produits, "Retour est null");
            Assert.IsInstanceOfType(produits, typeof(ActionResult<IEnumerable<Produit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(produits.Value, typeof(IEnumerable<Produit>), "Pas des produits ");
            Assert.AreEqual(p1, produits.Value.Last(), "produits égales");
        }

        [TestMethod()]
        public async Task GetAllProduitsByRegroupementsTest_Normal()
        {
            var produits = await _controller.GetAllProduitByRegroupement(1);
            Assert.IsNotNull(produits, "Retour est null");
            Assert.IsInstanceOfType(produits, typeof(ActionResult<IEnumerable<Produit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(produits.Value, typeof(IEnumerable<Produit>), "Pas des produits ");
            Assert.AreEqual(p1, produits.Value.Last(), "produits égales");
        }

        [TestMethod()]
        public async Task GetAllProduitsByCategiorieTest_Normal()
        {
            var produits = await _controller.GetAllProduitByCategorie(1);
            Assert.IsNotNull(produits, "Retour est null");
            Assert.IsInstanceOfType(produits, typeof(ActionResult<IEnumerable<Produit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(produits.Value, typeof(IEnumerable<Produit>), "Pas des produits ");
            Assert.AreEqual(p1, produits.Value.Last(), "produits égales");
        }

        [TestMethod()]
        public async Task GetAllProduitsByTypeTest_Normal()
        {
            var produits = await _controller.GetAllProduitByType(1);
            Assert.IsNotNull(produits, "Retour est null");
            Assert.IsInstanceOfType(produits, typeof(ActionResult<IEnumerable<Produit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(produits.Value, typeof(IEnumerable<Produit>), "Pas des produits ");
            Assert.AreEqual(p1, produits.Value.Last(), "produits égales");
        }

        [TestMethod()]
        public async Task GetProduitByIdTest_Normal()
        {
            var result = await _controller.GetProduitById(p1.Idproduit);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produit?>), "Pas un ActionResult");
            Assert.IsNull(result.Result, "Résultat est pas null");
            Assert.IsNotNull(result.Value, "Valeur est null");
            Assert.IsInstanceOfType(result.Value, typeof(Produit), "Pas un Produit");
            Assert.AreEqual(p1, result.Value, "Client égaux");
        }

        [TestMethod()]
        public async Task GetProduitByIdTest_Innexistant()
        {
            var result = await _controller.GetProduitById(-1);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produit?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFound");
            Assert.IsNull(result.Value, "Valeur est pas null");
        }

        [TestMethod()]
        public async Task PostProduitTest_Normal()
        {
            ProduitDTO p2 = new ProduitDTO()
            {
                Idtypeproduit = 1,
                Idpays = 1,
                Nomproduit = "Produit2",
                Delailivraison = 1,
                Coutlivraison = 1,
                Nbpaiementmax = 1
            };
            Produit p3 = new Produit()
            {
                Idtypeproduit = 1,
                Idpays = 1,
                Nomproduit = "Produit2",
                Delailivraison = 1,
                Coutlivraison = 1,
                Nbpaiementmax = 1
            };
            _repository.Setup(x => x.AddProduitAsync(p3)).ReturnsAsync(p3);
            var result = await _controller.PostProduit(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produit?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Produit valeur = (Produit)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.Nomproduit, valeur.Nomproduit, "produits égales");
        }


        [TestMethod()]
        public async Task PutProduitTest_Normal()
        {
            ProduitDTO p3 = new ProduitDTO()
            {
                Idproduit = p1.Idproduit,
                Idtypeproduit = 1,
                Idpays = 1,
                Nomproduit = "Produit4",
                Delailivraison = 1,
                Coutlivraison = 1,
                Nbpaiementmax = 10
            };
            Produit p4 = new Produit()
            {
                Idproduit = p1.Idproduit,
                Idtypeproduit = 1,
                Idpays = 1,
                Nomproduit = "Produit4",
                Delailivraison = 1,
                Coutlivraison = 1,
                Nbpaiementmax = 10
            };
            _repository.Setup(x => x.UpdateProduitAsync(p1, p3)).ReturnsAsync(p4);
            var result = await _controller.PutProduit(p1.Idproduit, p3);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produit?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Produit valeur = (Produit)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p4.Coutlivraison, valeur.Coutlivraison, "Cartes bancaires égales (titulaire)");
            Assert.AreEqual(p1.Idproduit, valeur.Idproduit, "Cartes bancaires non-modifiées (id)");
            Assert.AreEqual(p4.Nbpaiementmax, valeur.Nbpaiementmax, "Cartes bancaires égales (dateexp)");
        }

        [TestMethod()]
        public async Task PutProduitTest_Innégal()
        {
            ProduitDTO p6 = new ProduitDTO()
            {
                Idproduit = 0,
                Idtypeproduit = 1,
                Idpays = 1,
                Nomproduit = "Produit4",
                Delailivraison = 1,
                Coutlivraison = 1,
                Nbpaiementmax = 10
            };
            var result = await _controller.PutProduit(-1, p6);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produit?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Résultat pas BadRequest");
        }


        [TestMethod()]
        public async Task PutProduitTest_Introuvable()
        {
            ProduitDTO p7 = new ProduitDTO()
            {
                Idproduit = -1,
                Idtypeproduit = 1,
                Idpays = 1,
                Nomproduit = "Produit4",
                Delailivraison = 1,
                Coutlivraison = 1,
                Nbpaiementmax = 10
            };
            var result = await _controller.PutProduit(-1, p7);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produit?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Résultat pas NotFound");
        }

    }
}