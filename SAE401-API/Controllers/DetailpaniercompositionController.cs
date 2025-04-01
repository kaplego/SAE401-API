using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System.Security.Claims;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailpaniercompositionController : ControllerBase
    {
        private readonly IDetailPanierCompositionRepository<Detailpaniercomposition> dataRepository;

        // Le constructeur doit uniquement accepter l'interface IDetailPanierRepository<Detailpanier>
        public DetailpaniercompositionController(IDetailPanierCompositionRepository<Detailpaniercomposition> datarepo)
        {
            dataRepository = datarepo;
        }


        [HttpPut("{idcomposition}/{idclient}")]
        [Authorize()]
        public async Task<IActionResult> PutDetailPanoerComposition(int idcomposition, int idclient, DetailpaniercompositionDTO detailpaniercompositionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérification de l'intégrité des paramètres
            if (idcomposition != detailpaniercompositionDTO.Idcomposition  || idclient != detailpaniercompositionDTO.Idclient)
            {
                return BadRequest("Les paramètres ne correspondent pas.");
            }

            // Récupérer l'entité existante
            var detailpaniercompositionToUpdate = await dataRepository.GetDetailPanierCompositionByIdAsync(idcomposition, idclient);

            if (detailpaniercompositionToUpdate.Value == null)
            {
                return NotFound();
            }

            // Convertir le DTO en une instance de Detailpanier
            var updatedDetailpaniercomposition = new Detailpaniercomposition
            {
                Idcomposition = detailpaniercompositionDTO.Idcomposition,
                Idclient = detailpaniercompositionDTO.Idclient,
                Quantitepaniercomposition = detailpaniercompositionDTO.Quantitepaniercomposition
                // Ajoutez ici d'autres propriétés du DTO si nécessaire
            };


            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != updatedDetailpaniercomposition.Idcomposition.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            // Appeler la méthode de mise à jour dans le repository
            await dataRepository.UpdateDetailPanierCompositionAsync(detailpaniercompositionToUpdate.Value, updatedDetailpaniercomposition);

            return NoContent();
        }



        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Detailpaniercomposition>> PostDetailPanierComposition(DetailpaniercompositionDTO detailpaniercomposition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detailpaniercompositionfinal = new Detailpaniercomposition
            {
                Idcomposition = detailpaniercomposition.Idcomposition,
                Idclient = detailpaniercomposition.Idclient,
                Quantitepaniercomposition = detailpaniercomposition.Quantitepaniercomposition,
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != detailpaniercomposition.Idcomposition.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.AddDetailPanierCompositionAsync(detailpaniercompositionfinal);

            return NoContent();
        }

        [HttpDelete("{idcomposition}/{idclient}")]
        [Authorize()]
        public async Task<IActionResult> DeleteDetailPanierComposition(int idcomposition, int idclient)
        {
            var detailpaniercomposition = await dataRepository.GetDetailPanierCompositionByIdAsync(idcomposition, idclient);
            if (detailpaniercomposition.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != detailpaniercomposition.Value.Idcomposition.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.DeleteDetailPanierCompositionAsync(detailpaniercomposition.Value);
            return NoContent();
        }
    }
}
