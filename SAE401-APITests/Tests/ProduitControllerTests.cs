using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Controllers;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_APITests.Tests
{
    [TestClass()]
    public class ProduitControllerTests
    {
        private _DBMilibooContext _context;
        private IProduitRepository<Produit> _repository;
        private ProduitController _controller;
        private Produit p1;
        private Regroupementproduit r1;
        private Detailregroupement d1;
        private Couleur col1;
        private Coloration cl1;
        private Typeproduit t1;
        private Categorieproduit cat1;



        [TestInitialize]
        public async Task TestInitialize()
        {
            Env.Load(Path.Combine(
                Directory.GetParent(Directory.GetParent(
                Directory.GetParent(Directory.GetCurrentDirectory()
                .ToString()).ToString()).ToString()).ToString(), ".env"));
            var builder = new DbContextOptionsBuilder<_DBMilibooContext>().UseNpgsql(
                Environment.GetEnvironmentVariable("CONNECTION_STRING"))
                .EnableSensitiveDataLogging(true);
            _context = new _DBMilibooContext(builder.Options);
            _repository = new ProduitManager(_context);
            _controller = new ProduitController(_repository);



            cat1 = new Categorieproduit()
            {
                Idcategorie = -1,
                Nomcategorie = "Test",
                Descriptioncategorie = "Test",
                Estfiltrable = true

            };
            await _context.Categorieproduits.AddAsync(cat1);
            await _context.SaveChangesAsync();

            t1 = new Typeproduit()
            {
                Idtypeproduit = -1,
                Idcategorie = cat1.Idcategorie,
                Nomtypeproduit = "Test"

            };
            await _context.Typeproduits.AddAsync(t1);
            await _context.SaveChangesAsync();

            p1 = new Produit()
            {
                Idtypeproduit = t1.Idtypeproduit,
                Idpays = 1,
                Nomproduit = "Test très très précis sur les produits omg",
                Notice = "Test",
                Aspecttechnique = "Test",
                Delailivraison = 1,
                Coutlivraison = 1,
                Nbpaiementmax = 1
            };
            await _context.Produits.AddAsync(p1);
            await _context.SaveChangesAsync();


            col1 = new Couleur()
            {
                Idcouleur = 30,
                Nomcouleur = "Test",
                Rgbcouleur = "ffffff"

            };
            await _context.Couleurs.AddAsync(col1);
            await _context.SaveChangesAsync();

            cl1 = new Coloration()
            {
                Idproduit = p1.Idproduit,
                Idcouleur = col1.Idcouleur,
                Quantitestock = 1

            };
            await _context.Colorations.AddAsync(cl1);
            await _context.SaveChangesAsync();

            r1 = new Regroupementproduit()
            {
                Idregroupement = 4,
                Nomregroupement = "Test"
            };
            await _context.Regroupementproduits.AddAsync(r1);
            await _context.SaveChangesAsync();

            d1 = new Detailregroupement()
            {
                Idproduit = p1.Idproduit,
                Idcouleur = col1.Idcouleur,
                Idregroupement = r1.Idregroupement


            };
            await _context.Detailregroupements.AddAsync(d1);
            await _context.SaveChangesAsync();
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
            var produits = await _controller.GetAllProduitByRecherche("Test très très précis sur les produits omg");
            Assert.IsNotNull(produits, "Retour est null");
            Assert.IsInstanceOfType(produits, typeof(ActionResult<IEnumerable<Produit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(produits.Value, typeof(IEnumerable<Produit>), "Pas des produits ");
            Assert.AreEqual(p1, produits.Value.Last(), "produits égales");
        }

        [TestMethod()]
        public async Task GetAllProduitsByRegroupementsTest_Normal()
        {
            var produits = await _controller.GetAllProduitByRegroupement(4);
            Assert.IsNotNull(produits, "Retour est null");
            Assert.IsInstanceOfType(produits, typeof(ActionResult<IEnumerable<Produit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(produits.Value, typeof(IEnumerable<Produit>), "Pas des produits ");
            Assert.AreEqual(p1, produits.Value.Last(), "produits égales");
        }

        [TestMethod()]
        public async Task GetAllProduitsByCategiorieTest_Normal()
        {
            var produits = await _controller.GetAllProduitByCategorie(-1);
            Assert.IsNotNull(produits, "Retour est null");
            Assert.IsInstanceOfType(produits, typeof(ActionResult<IEnumerable<Produit>>), "Pas un ActionResult");
            Assert.IsInstanceOfType(produits.Value, typeof(IEnumerable<Produit>), "Pas des produits ");
            Assert.AreEqual(p1, produits.Value.Last(), "produits égales");
        }

        [TestMethod()]
        public async Task GetAllProduitsByTypeTest_Normal()
        {
            var produits = await _controller.GetAllProduitByType(-1);
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
                Idtypeproduit = t1.Idtypeproduit,
                Idpays = 1,
                Nomproduit = "Produit2",
                Delailivraison = 1,
                Coutlivraison = 1,
                Nbpaiementmax = 1
            };
            var result = await _controller.PostProduit(p2);
            Assert.IsNotNull(result, "Retour est null");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produit?>), "Pas un ActionResult");
            Assert.IsNotNull(result.Result, "Résultat est null");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Résultat pas OK");
            Produit valeur = (Produit)((ObjectResult)result.Result).Value;
            Assert.IsNotNull(valeur, "Valeur est null");
            Assert.AreEqual(p2.Nomproduit, valeur.Nomproduit, "produits égales");
            try { _context.Produits.Remove(valeur); } catch { }
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PostProduitTest_Invalide()
        {
            ProduitDTO p3 = new ProduitDTO()
            {
                Idtypeproduit = t1.Idtypeproduit,
                Idpays = 1,
                Nomproduit = "Produit3",
                Delailivraison = 1,
                Coutlivraison = 100000000000000,
                Nbpaiementmax = 1
            };
            try
            {
                var result = await _controller.PostProduit(p3);
            }
            catch (DbUpdateException ex)
            {
                _context.Produits.Remove((Produit)ex.Entries.First().Entity);
                throw ex;
            }
        }


        [TestMethod()]
        public async Task PutProduitTest_Normal()
        {
            ProduitDTO p4 = new ProduitDTO()
            {
                Idproduit = p1.Idproduit,
                Idtypeproduit = t1.Idtypeproduit,
                Idpays = 1,
                Nomproduit = "Produit4",
                Delailivraison = 1,
                Coutlivraison = 1,
                Nbpaiementmax = 10
            };
            var result = await _controller.PutProduit(p1.Idproduit, p4);
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
        [ExpectedException(typeof(DbUpdateException))]
        public async Task PutProduitTest_Invalide()
        {
            ProduitDTO p5 = new ProduitDTO()
            {
                Idproduit = p1.Idproduit,
                Idtypeproduit = t1.Idtypeproduit,
                Idpays = 1,
                Nomproduit = "Produit4",
                Delailivraison = 1,
                Coutlivraison = 100000000000000,
                Nbpaiementmax = 10
            };
            try
            {
                var result = await _controller.PutProduit(p1.Idproduit, p5);
            }
            catch (DbUpdateException ex)
            {
                Produit p = (Produit)ex.Entries.First().Entity;
                p.Coutlivraison = 1;
                throw ex;
            }
        }


        [TestMethod()]
        public async Task PutProduitTest_Innégal()
        {
            ProduitDTO p6 = new ProduitDTO()
            {
                Idproduit = 0,
                Idtypeproduit = t1.Idtypeproduit,
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
                Idtypeproduit = t1.Idtypeproduit,
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






        [TestCleanup()]
        public async Task TestCleanup()
        {
            await _context.SaveChangesAsync();
            _context.Detailregroupements.Remove(d1);
            _context.Regroupementproduits.Remove(r1);

            _context.Colorations.Remove(cl1);
            _context.Couleurs.Remove(col1);


            _context.Produits.Remove(p1);



            _context.Typeproduits.Remove(t1);
            _context.Categorieproduits.Remove(cat1);



            await _context.SaveChangesAsync();
        }


    }
}