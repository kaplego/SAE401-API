using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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


        /// <summary>
        /// Modifie une relation DtPnCp
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idcomposition">L'IDcomposition à modifier</param>
        /// <param name="idclient">L'IDclient à modifier</param>
        /// <param name="detailpaniercomposition">La relation mise à jour</param>
        /// <response code="200">La relation à été modifiée</response>
        /// <response code="400">La relation n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">La relation n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [HttpPut("{idcomposition}/{idclient}")]
        [Authorize()]
        public async Task<ActionResult<Detailpaniercomposition?>> PutDetailPanierComposition(int idcomposition, int idclient, DetailpaniercompositionDTO detailpaniercomposition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérification de l'intégrité des paramètres
            if (idcomposition != detailpaniercomposition.Idcomposition || idclient != detailpaniercomposition.Idclient)
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
                Idcomposition = detailpaniercomposition.Idcomposition,
                Idclient = detailpaniercomposition.Idclient,
                Quantitepaniercomposition = detailpaniercomposition.Quantitepaniercomposition
                // Ajoutez ici d'autres propriétés du DTO si nécessaire
            };


            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != updatedDetailpaniercomposition.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            // Appeler la méthode de mise à jour dans le repository
            await dataRepository.UpdateDetailPanierCompositionAsync(detailpaniercompositionToUpdate.Value, updatedDetailpaniercomposition);

            return Ok(detailpaniercompositionToUpdate.Value);
        }


        /// <summary>
        /// Créé une relation DtPnCp
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="detailpaniercomposition">La relation à ajouter</param>
        /// <response code="200">La relation à été créée</response>
        /// <response code="400">La relation n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Detailpaniercomposition?>> PostDetailPanierComposition(DetailpaniercompositionDTO detailpaniercomposition)
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
            if (identity == null || identity.FindFirst("id").Value != detailpaniercomposition.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.AddDetailPanierCompositionAsync(detailpaniercompositionfinal);

            return Ok(detailpaniercompositionfinal);
        }


        /// <summary>
        /// Supprime une relation DtPnCp
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idcomposition">L'IDcomposition à supprimer</param>
        /// <param name="idclient">L'IDclient à supprimer</param>
        /// <response code="200">La relation à été supprimée</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">La relation n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
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
            if (identity == null || identity.FindFirst("id").Value != detailpaniercomposition.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.DeleteDetailPanierCompositionAsync(detailpaniercomposition.Value);
            return Ok();
        }
    }
}
