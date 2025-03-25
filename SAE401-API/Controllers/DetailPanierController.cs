using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailPanierController : ControllerBase
    {
        private readonly IDetailPanierRepository<Detailpanier> dataRepository;

        // Le constructeur doit uniquement accepter l'interface IDetailPanierRepository<Detailpanier>
        public DetailPanierController(IDetailPanierRepository<Detailpanier> datarepo)
        {
            dataRepository = datarepo;
        }

        [HttpGet("{idproduit}/{idcouleur}/{idclient}")]
        public async Task<ActionResult<Detailpanier>> GetDetailPanierByIdAsync(int idproduit, int idcouleur, int idclient)
        {
            var detailpanier = await dataRepository.GetDetailPanierByIdAsync(idproduit, idcouleur, idclient);

            if (detailpanier.Value == null)
            {
                return NotFound();
            }

            return detailpanier;
        }

        [HttpPut("{idproduit}/{idcouleur}/{idclient}")]
        public async Task<IActionResult> PutDetailProduit(int idproduit, int idcouleur, int idclient, DetailpanierDTO detailpanierDTO)
        {
            // Vérification de l'intégrité des paramètres
            if (idproduit != detailpanierDTO.Idproduit || idcouleur != detailpanierDTO.Idcouleur || idclient != detailpanierDTO.Idclient)
            {
                return BadRequest("Les paramètres ne correspondent pas.");
            }

            // Récupérer l'entité existante
            var produitToUpdate = await dataRepository.GetDetailPanierByIdAsync(idproduit, idcouleur, idclient);

            if (produitToUpdate.Value == null)
            {
                return NotFound();
            }

            // Convertir le DTO en une instance de Detailpanier
            var updatedDetailpanier = new Detailpanier
            {
                Idproduit = detailpanierDTO.Idproduit,
                Idcouleur = detailpanierDTO.Idcouleur,
                Idclient = detailpanierDTO.Idclient,
                Quantitepanier = detailpanierDTO.Quantitepanier
                // Ajoutez ici d'autres propriétés du DTO si nécessaire
            };

            // Appeler la méthode de mise à jour dans le repository
            await dataRepository.UpdateDetailPanierAsync(produitToUpdate.Value, updatedDetailpanier);

            return NoContent();
        }



        [HttpPost]
        public async Task<ActionResult<Detailpanier>> PostDetailPanier(DetailpanierDTO detailpanier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detailpanierfinal = new Detailpanier
            {
                Idproduit = detailpanier.Idproduit,
                Idcouleur = detailpanier.Idcouleur,
                Idclient = detailpanier.Idclient,
                Quantitepanier = detailpanier.Quantitepanier
            };

            await dataRepository.AddDetailPanierAsync(detailpanierfinal);

            return NoContent();
        }

        [HttpDelete("{idproduit}/{idcouleur}/{idclient}")]
        public async Task<IActionResult> DeleteDetailPanier(int idproduit, int idcouleur, int idclient)
        {
            var detailpanier = await dataRepository.GetDetailPanierByIdAsync(idproduit, idcouleur, idclient);
            if (detailpanier.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteDetailPanierAsync(detailpanier.Value);
            return NoContent();
        }
    }
}
