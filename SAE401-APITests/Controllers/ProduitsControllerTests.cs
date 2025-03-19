using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataManager;
using SAE401_API.Controllers;
using SAE401_API.Models.EntityFramework;
using Sprache;

namespace ProduitControllerTests
{
    [TestClass]
    public class ProduitControllerTests
    {
        private ProduitsController controller;
        private _DBMilibooContext context;
        private IDataRepository<Produit> dataRepository;
        public ProduitControllerTests()
        {
            var builder = new DbContextOptionsBuilder<_DBMilibooContext>().UseNpgsql("Server=51.83.36.122;Port=5432;Uid=maglou;Password=zxADxL;Database=sae401_td2_miliboo;SearchPath=sae401_td2_miliboo");
            context = new _DBMilibooContext(builder.Options);
            dataRepository = new ProduitManager(context);
            controller = new ProduitsController(dataRepository);
        }
        [TestMethod()]
        public async void GetProduitTest()
        {

            // Arrange
            var controller = new ProduitsController(dataRepository);
            // Act
            var produitTest =  new Produit { Idproduit = 999999999, Nomproduit = "Produit 1", Idtypeproduit = 2, Idpays = 3, Delailivraison = 5, Coutlivraison = 15.99m, Nbpaiementmax = 3 };
            var postProduitTest = controller.PostProduit(produitTest);
            var result = controller.GetProduit(999999999).Result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produit>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsNotNull(result, "Erreur est pas null"); //Test de l'erreur
            Assert.IsInstanceOfType(result.Value, typeof(Produit), "Pas un Produit "); // Test du type du contenu (valeur) du retour
            Assert.AreEqual(produitTest, (Produit?)result.Value, "Produits pas identiques"); //Test du produit récupéré
            var deleteProduitTest = await controller.DeleteProduit(produitTest.Idproduit);
        }



    }
}
